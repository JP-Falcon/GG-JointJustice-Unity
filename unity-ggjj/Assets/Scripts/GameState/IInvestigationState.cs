using System.Collections.Generic;
using Ink.Runtime;

public interface IInvestigationState
{
    void LockChoice(string choiceId, InvestigationChoiceType type);
    void UnlockChoice(string choiceId, InvestigationChoiceType type);
    bool IsChoiceUnlocked(string choiceId, InvestigationChoiceTag investigationChoiceTag, InvestigationChoiceType type);
    void OpenWithChoices(List<Choice> talkOptions, List<Choice> moveOptions);
}