#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;

namespace Scanner;

public static class AnimationControllerParser
{
    public record Info
    {
        public string EntryPoint { get; init; }
        public bool HasIntro { get; init; }
        public bool HasTalking { get; init; }
        
        public override string ToString()
        {
            var attributes = new List<string>();
            if (HasTalking)
            {
                attributes.Add("🗣️");
            }
            if (HasIntro)
            {
                attributes.Add("🎦");
            }
            
            return $"{EntryPoint} ({string.Join(", ", attributes)})";
        }
    }

    private static Info ToInfo(IEnumerable<string> states)
    {
        var stateList = states.ToList();
        return stateList.Count switch
        {
            1 => new Info()
            {
                EntryPoint = stateList.First(), 
                HasIntro = true, HasTalking = false
            },
            2 => new Info()
            {
                EntryPoint = stateList.First(), 
                HasIntro = !stateList.Any(state => state.Contains("Talking")), 
                HasTalking = stateList.Any(state => state.Contains("Talking"))
            },
            3 => new Info()
            {
                EntryPoint = stateList.First(),
                HasIntro = true,
                HasTalking = stateList.Any(state => state.Contains("Talking"))
            },
            _ => throw new NotSupportedException("Only 1, 2 or 3 states are supported")
        };
    }
    
    #region Unity types
    // these types are used to deserialize the YAML file and cause warnings
    // ReSharper disable MemberCanBePrivate.Global
    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public record UnityUnused
    {
    }
    public record UnityAnimatorState
    {
        public record Content
        {
            public record Motion
            {
                public string? guid { get; init; }
            }
            public string m_Name { get; init; }
            public Motion m_Motion { get; init; }
            public List<FileReference> m_Transitions { get; init; }
        }
        public Content AnimatorState { get; init; }
    }

    public record UnityAnimatorStateTransition
    {
        public record Content
        {
            public FileReference m_DstState { get; init; }
        }
        public Content AnimatorStateTransition { get; init; }
    }

    public record FileReference
    {
        public string fileID { get; init; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore InconsistentNaming
    // ReSharper restore MemberCanBePrivate.Global
    #endregion

    public static List<PathItem> ConvertToMarkdown(string absolutePathToAssetsDirectory, Dictionary<string, string> pathsByGUID, string relativeFileName)
    {
        var yamlContent = File.ReadAllText(Path.Join(absolutePathToAssetsDirectory, relativeFileName));
        
        // read the .controller YAML syntax by mapping YAML fields to C# records depending on the tag
        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .WithTagMapping("tag:unity3d.com,2011:1102", typeof(UnityAnimatorState))
            .WithTagMapping("tag:unity3d.com,2011:1101", typeof(UnityAnimatorStateTransition))
            .WithTagMapping("tag:unity3d.com,2011:1107", typeof(UnityUnused)) // AnimatorStateMachine
            .WithTagMapping("tag:unity3d.com,2011:91", typeof(UnityUnused)) // AnimatorController
            .WithTagMapping("tag:unity3d.com,2011:206", typeof(UnityUnused)) // BlendTree
            .WithTagMapping("tag:unity3d.com,2011:114", typeof(UnityUnused)) // MonoBehaviour
            .Build();
        
        // Unity's variant of YAML isn't "proper" YAML
        // https://github.com/aaubry/YamlDotNet/issues/140#issuecomment-206927072
        // to work around this, split the file by…
        var items = yamlContent.Split("---");
        // treat the first part as a header…
        var header = items[0];
        var list = items[1..]
            // that gets prepended to every item
            .Select(item => header + "---" + item)
            .ToDictionary(
                // fetch the ID of each item by taking the value after `&` in the third line
                item => item.Split("\n")[2].Split("&")[1],
                // and parse the resulting YAML into a dynamic object based on the TagMapping from above
                item => deserializer.Deserialize<dynamic>(item)
            );
            
        // we're only interested in the states and transitions, that represent the controller graph
        var animatorStates = list
            .Where(pair => pair.Value is UnityAnimatorState)
            .ToDictionary(pair => pair.Key, pair => pair.Value as UnityAnimatorState)
            .Where(pair =>
            {
                if (pair.Value?.AnimatorState.m_Motion.guid == null)
                {
                    return false;
                }
                return pathsByGUID.ContainsKey(pair.Value.AnimatorState.m_Motion.guid);
            })
            .ToDictionary(pair => pair.Key, pair => pair.Value);
        var animatorStateTransitions = list
            .Where(pair => pair.Value is UnityAnimatorStateTransition)
            .ToDictionary(pair => pair.Key, pair => pair.Value as UnityAnimatorStateTransition);
        
        // Graph traversal methods based on states representing nodes and transitions representing edges
        #region Graph traversal
        List<string> FindAllStates(string startKey)
        {
            var visited = new HashSet<string>();
            return FindAllStatesRecursive(startKey, visited);
        }

        List<string> FindAllStatesRecursive(string startKey, HashSet<string> visited)
        {
            List<string> states = [];
            if (visited.Contains(startKey))
            {
                return states;
            }
            states.Add(startKey);
            visited.Add(startKey);

            var transitions = animatorStates[startKey].AnimatorState.m_Transitions;
            foreach (var transition in transitions)
            {
                var key = transition.fileID;
                key = animatorStateTransitions[key].AnimatorStateTransition.m_DstState.fileID;
                if (!visited.Contains(key))
                {
                    states.AddRange(FindAllStatesRecursive(key, visited));
                }
            }
            return states;
        }
        #endregion
        
        // documentation should only contain states that we consider an "entry point" 
        //
        // nodes can be of three types
        //   - a talking state
        //   - a non-talking state
        //   - an intro state
        //
        // a state is an entry points if it is…
        //  - a stand-alone state
        //  - OR
        //  -   not a talking state AND
        //  -   not transitioned into by another entry point
        
        // find all states that are         
        var entryPoints = animatorStates
            // not a talking state
            .Where(pair => !pair.Value.AnimatorState.m_Name.Contains("Talking")) // AND
            // have less than 2 transitions into them
            .Where(pair => animatorStateTransitions.Count(entry => entry.Value.AnimatorStateTransition.m_DstState.fileID == pair.Key) < 2
            ).ToList();
        
        var transitionsOfEntryPoints = entryPoints
            .SelectMany(entryPoint =>
                entryPoint.Value.AnimatorState.m_Transitions.Select(transition=> animatorStateTransitions[transition.fileID].AnimatorStateTransition.m_DstState.fileID)
            );

        var info = entryPoints
            // also ignore states that are only transitioned into by other entry points
            .Where(entryPoint => !transitionsOfEntryPoints.Contains(entryPoint.Key))
            // sort them alphabetically
            .OrderBy(a=>a.Value.AnimatorState.m_Name)
            // then traverse the graph until we find a duplicate
            .Select(entryPoint =>
                FindAllStates(entryPoint.Key).Select(id=>animatorStates[id].AnimatorState.m_Name).ToList()
            )
            // then summarize the info for the documentation
            .Select(ToInfo)
            .ToList();

        return info.Select(line =>
            new PathItem()
            {
                Item = line.ToString()
            }
        ).ToList();

    }
}