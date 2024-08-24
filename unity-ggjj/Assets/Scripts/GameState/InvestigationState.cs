using System;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using UnityEngine;

public class InvestigationState : MonoBehaviour, IInvestigationState
{
    public enum ChoiceType
    {
        Talk,
        Move
    }

    [SerializeField] private MenuOpener InvestigationMainMenuOpener;
    [SerializeField] private ChoiceMenu InvestigationTalkMenu;
    [SerializeField] private ChoiceMenu InvestigationMoveMenu;
    
    private readonly List<string> _examinedChoices = new();
    private readonly List<string> _unlockedTalkChoices = new();
    private readonly List<string> _unlockedMoveChoices = new();
    private List<Choice> _moveOptions;
    private List<Choice> _talkOptions;

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

    // ReSharper disable Unity.PerformanceAnalysis
    public void OpenWithChoices(List<Choice> talkOptions, List<Choice> moveOptions)
    {
        InvestigationMainMenuOpener.OpenMenu();
        _talkOptions = talkOptions;
        _moveOptions = moveOptions;
    }

    public void Clear()
    {
        _examinedChoices.Clear();
    }

    public void OpenTalkMenu()
    {
        InvestigationMainMenuOpener.CloseMenu();
        InvestigationTalkMenu.Initialise(_talkOptions);
    }

    public void OpenMoveMenu()
    {
        InvestigationMainMenuOpener.CloseMenu();
        InvestigationMoveMenu.transform.parent.gameObject.SetActive(true);
        InvestigationMoveMenu.Initialise(_moveOptions);
    }
}