using System;
using System.Collections.Generic;
using System.Linq;

public class InvestigationState : IInvestigationState
{
    public enum ChoiceType
    {
        Talk,
        Move
    }
    
    private readonly List<string> _examinedChoices = new();
    private readonly List<string> _unlockedTalkChoices = new();
    private readonly List<string> _unlockedMoveChoices = new();
    
    public void AddExaminedChoice(string choice)
    {
        _examinedChoices.Add(choice);
    }
    
    public bool IsChoiceExamined(string choice)
    {
        return _examinedChoices.Contains(choice);
    }

    public void UnlockChoice(string choice, ChoiceType type)
    {
        switch (type)
        {
            case ChoiceType.Talk:
                _unlockedTalkChoices.Add(choice);
                break;
            case ChoiceType.Move:
                _unlockedMoveChoices.Add(choice);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public bool IsChoiceUnlocked(string choice, ChoiceType type)
    {
        return type switch
        {
            ChoiceType.Talk => _unlockedTalkChoices.Contains(choice),
            ChoiceType.Move => _unlockedMoveChoices.Contains(choice),
            _ => false
        };
    }

    public void Clear()
    {
        _examinedChoices.Clear();
    }
}