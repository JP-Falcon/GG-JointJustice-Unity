using System;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using TextDecoder.Parser;
using Object = Ink.Runtime.Object;
using UnityEngine;

[Serializable]
public class NarrativeScript : INarrativeScript
{
    [field: Tooltip("Drag an Ink narrative script here.")]
    [field: SerializeField] public TextAsset Script { get; private set; }

    private ObjectStorage _objectStorage = new ObjectStorage();

    public string Name => Script.name;
    public Story Story { get; private set; }
    public IObjectStorage ObjectStorage => _objectStorage;

    /// <summary>
    /// Initialise values on construction.
    /// </summary>
    /// <param name="script">An Ink narrative script</param>
    /// <param name="actionDecoder">An optional action decoder, used for testing</param>
    public NarrativeScript(TextAsset script, IActionDecoder actionDecoder = null)
    {
        Script = script;
        Initialize(actionDecoder);
    }

    /// <summary>
    /// Initializes script values that cannot be set in the Unity inspector
    /// and begins script reading and object preloading.
    /// </summary>
    /// <param name="actionDecoder">An optional action decoder, used for testing</param>
    public void Initialize(IActionDecoder actionDecoder = null)
    {
        if (Script == null)
        {
            throw new NullReferenceException("Could not initialize narrative script. Script field is null.");
        }
        _objectStorage = new ObjectStorage();
        Story = new Story(Script.text);
        ReadScript(Story, actionDecoder ?? new ObjectPreloader(_objectStorage));
    }

    /// <summary>
    /// Creates an action decoder and assigns an ObjectPreloader to its controller properties.
    /// Gets all lines from an Ink story, extracts all of the action lines (using a
    /// hash set to remove duplicate lines). Calls the ActionDecoder's OnNewActionLine method
    /// for each action extracted. ActionDecoder then calls the actions
    /// on the ObjectPreloader to preload any required assets.
    /// </summary>
    /// <param name="story">The Ink story to read</param>
    /// <param name="actionDecoder">An optional action decoder, used for testing</param>
    private void ReadScript(Story story, IActionDecoder actionDecoder)
    {
        var lines = ReadContent(story);

        var actions = lines.Where(line => line[0] == '&').Distinct().ToList();
        foreach (var action in actions)
        {
            try
            {
                actionDecoder.InvokeMatchingMethod(action);
            }
            catch (MethodNotFoundScriptParsingException)
            {
                // these types of exceptions are fine, as only actions
                // with resources need to be handled by the ObjectPreloader
            }
        }
    }

    /// <summary>
    /// Reads the content of an Ink file.
    /// Traverses an Ink story, exploring all dialogue paths
    /// Stores visited paths in a hash set to avoid re-visiting them
    /// </summary>
    /// <param name="content">The Ink container content to read</param>
    /// <param name="lines">A list to add read lines to</param>
    /// <param name="story">The story containing the "content" container</param>
    public static List<string> ReadContent(Story originalStory)
    {
        var story = new Story(originalStory.ToJson());
        var originalState = story.state;
        var lines = new List<string>();
        var visitedPaths = new HashSet<string>();

        ExploreNode();

        void ExploreNode()
        {
            while (story.canContinue)
            {
                var content = story.Continue();
                lines.Add(content);
            }

            if (story.currentChoices.Count > 0)
            {
                foreach (var choice in story.currentChoices)
                {
                    var savedState = story.state.ToJson();
                    story.ChooseChoiceIndex(choice.index);

                    if (!visitedPaths.Contains(story.state.currentPathString))
                    {
                        visitedPaths.Add(story.state.currentPathString);
                        ExploreNode();
                    }

                    story.state.LoadJson(savedState);
                }
            }
        }

        story.state.LoadJson(originalState.ToJson());

        return lines;
    }

    /// <summary>
    /// Resets the state of a story
    /// </summary>
    public void Reset()
    {
        Story.ResetState();
    }

    public override string ToString()
    {
        return Script.name;
    }
}