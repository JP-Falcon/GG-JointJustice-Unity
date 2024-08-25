using System.Collections.Generic;
using Ink.Runtime;

public interface IInvestigationState
{
    void UnlockChoice(string choice, InvestigationState.ChoiceType type);
    bool IsChoiceUnlocked(string choice, InvestigationState.ChoiceType type);
    void OpenWithChoices(List<Choice> talkOptions, List<Choice> moveOptions);
    void Clear();

    void OpenTalkMenu();
    void OpenMoveMenu();
}