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
            $"Move tag is missing a valid corresponding background tag in the format '#{InvestigationState.BACKGROUND_TAG_KEY}:[background name]'\n";
        
        var elements = story.state.callStack.elements.AsEnumerable().Reverse();
        
        foreach (var element in elements)
        {
            message += $"In container {element.currentPointer.path.head.name} at choice index {element.currentPointer.path.tail.tail.head.name}\n";
        }

        return message;
    }
}