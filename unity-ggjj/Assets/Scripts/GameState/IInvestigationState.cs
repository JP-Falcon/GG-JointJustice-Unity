public interface IInvestigationState
{
    void AddExaminedChoice(string choice);
    bool IsChoiceExamined(string choice);
    void UnlockChoice(string choice, InvestigationState.ChoiceType type);
    bool IsChoiceUnlocked(string choice, InvestigationState.ChoiceType type);
    void Clear();
}