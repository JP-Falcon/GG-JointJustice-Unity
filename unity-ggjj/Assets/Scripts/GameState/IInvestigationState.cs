using System.Collections.Generic;
using Ink.Runtime;

public interface IInvestigationState
{
    void LockChoice(string choice, InvestigationChoiceType type);
    void UnlockChoice(string choice, InvestigationChoiceType type);
    bool IsChoiceUnlocked(string choice, InvestigationChoiceTag investigationChoiceTag, InvestigationChoiceType type);
    void OpenWithChoices(List<Choice> talkOptions, List<Choice> moveOptions);
}