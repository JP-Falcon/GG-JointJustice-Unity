using Ink.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using TextDecoder.Parser;
using UnityEngine;

[Serializable]
public class NarrativeScript : INarrativeScript
{
    [field: Tooltip("Drag an Ink narrative script here.")]
    [field: SerializeField] public TextAsset Script { get; private set; }

    private ObjectStorage _objectStorage = new();

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
    /// Reads an Ink story and calls methods on
    /// an ObjectPreloader in order to preload assets
    /// </summary>
    /// <param name="story">The Ink story to read</param>
    /// <param name="actionDecoder">An optional action decoder, used for testing</param>
    private void ReadScript(Story story, IActionDecoder actionDecoder)
    {
        var storyData = ReadContent(story);

        foreach (var action in storyData.DistinctActions)
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

        foreach (var tag in storyData.DistinctMoveTags)
        {
            actionDecoder.InvokeMatchingMethod($"{ActionDecoderBase.ACTION_TOKEN}SCENE:{tag}");
        }
    }

    /// <summary>
    /// Reads the content of an Ink file.
    /// Traverses an Ink story, exploring all dialogue paths
    /// Stores visited paths in a hash set to avoid re-visiting them
    /// </summary>
    /// <param name="story">The story containing the to be read</param>
    public static StoryData ReadContent(Story originalStory)
    {
        var story = new Story(originalStory.ToJson()); // Clone the story so as not to alter the state of the original story
        var originalState = story.state;
        var lines = new HashSet<string>();
        var moveTags = new HashSet<string>();
        var visitedPaths = new HashSet<string>();

        ExploreNode();

        void ExploreNode()
        {
            while (story.canContinue)
            {
                var line = story.Continue().Trim();

                if (line.Length == 0 ||
                    line[0] != '&')
                {
                    continue;
                }
                
                var lineWithoutNewLine = line.Replace("\n", "");
                lines.Add(lineWithoutNewLine);
            }

            if (story.currentChoices.Count == 0)
            {
                return;
            }

            foreach (var choice in story.currentChoices)
            {
                var savedState = story.state.ToJson();
                story.ChooseChoiceIndex(choice.index);

                if (choice.tags != null && 
                    choice.tags.Any(tag => tag == InvestigationChoiceType.Move.ToString()))
                {
                    if (!choice.HasTagValue(InvestigationState.BACKGROUND_TAG_KEY))
                    {
                        throw new MissingBackgroundTagException(story);
                    }
                    
                    moveTags.Add(choice.GetTagValue(InvestigationState.BACKGROUND_TAG_KEY));
                }

                if (visitedPaths.Add(story.state.currentPathString + story.state.callStack.elements.First().currentPointer.index))
                {
                    ExploreNode();
                }

                story.state.LoadJson(savedState);
            }
        }

        story.state.LoadJson(originalState.ToJson());

        var storyData = new StoryData(
            lines.ToList(),
            moveTags.ToList());

        return storyData;
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