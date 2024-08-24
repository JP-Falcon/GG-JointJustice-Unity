using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

public interface IChoiceMenu
{
    public enum Flags
    {
        None = 0,
        OpenOnCreation = 1
    }
    void Initialise(List<Choice> storyCurrentChoices, Flags flags);
}
