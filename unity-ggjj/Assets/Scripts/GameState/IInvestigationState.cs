using System.Collections.Generic;
using Ink.Runtime;

public interface IInvestigationState
{
    void LockChoice(string choice, ChoiceType type);
    void UnlockChoice(string choice, ChoiceType type);
    bool IsChoiceUnlocked(string choice, ChoiceTag choiceTag, ChoiceType type);
    void OpenWithChoices(List<Choice> talkOptions, List<Choice> moveOptions);

    public enum ChoiceType
    {
        Talk,
        Move
    }

    public enum ChoiceTag
    {
        None,
        Locked
    }
}