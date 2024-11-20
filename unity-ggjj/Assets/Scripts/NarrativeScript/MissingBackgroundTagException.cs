using System;
using System.Linq;
using Ink.Runtime;

public class MissingBackgroundTagException : Exception
{
    public MissingBackgroundTagException(Story story) : base(CreateExceptionMessage(story)) { }

    private static string CreateExceptionMessage(Story story)
    {
        var message = 
            $"Could not find background tag near '{story.currentText.Replace("\n", "")}'\n" +
            $"'#Move' choices also require a background tag: '#{InvestigationState.BACKGROUND_TAG_KEY}:[SceneAssetName]'\n";
        
        var elements = story.state.callStack.elements.AsEnumerable().Reverse();
        
        foreach (var element in elements)
        {
            message += $"In container {element.currentPointer.path.head.name} at choice index {element.currentPointer.path.tail.tail.head.name}\n";
        }

        return message;
    }
}