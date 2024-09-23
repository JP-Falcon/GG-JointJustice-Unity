using System;
using System.Collections.Generic;
using Ink.Runtime;

public interface IChoiceMenu
{
    void Initialise(List<Choice> storyCurrentChoices, Action onBackButtonClick, Action<MenuItem> onButtonCreated);
}
