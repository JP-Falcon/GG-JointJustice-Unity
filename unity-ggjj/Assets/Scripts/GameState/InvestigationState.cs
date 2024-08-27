using System;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvestigationState : MonoBehaviour, IInvestigationState
{
    public enum ChoiceType
    {
        Talk,
        Move
    }

    [SerializeField] private MenuOpener _investigationMainMenuOpener;
    [SerializeField] private ChoiceMenu _investigationTalkMenu;
    [SerializeField] private ChoiceMenu _investigationMoveMenu;
    
    private readonly List<string> _unlockedTalkChoices = new();
    private readonly List<string> _unlockedMoveChoices = new();
    private List<Choice> _moveOptions;
    private List<Choice> _talkOptions;

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

    public void OpenWithChoices(List<Choice> talkOptions, List<Choice> moveOptions)
    {
        _investigationMainMenuOpener.OpenMenu();
        _talkOptions = talkOptions;
        _moveOptions = moveOptions;
    }

    public void OpenTalkMenu()
    {
        _investigationMainMenuOpener.CloseMenu();
        _investigationTalkMenu.Initialise(_talkOptions);
    }

    public void OpenMoveMenu()
    {
        _investigationMainMenuOpener.CloseMenu();
        _investigationMoveMenu.transform.parent.gameObject.SetActive(true);
        _investigationMoveMenu.Initialise(_moveOptions);

        var selectableAndLabel = _investigationMoveMenu.GetComponentsInChildren<MenuItem>().ToList();

        foreach (var menuItem in selectableAndLabel)
        {
            menuItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                _investigationMoveMenu.transform.parent.gameObject.SetActive(false);
            });
            menuItem.OnItemSelect.AddListener(() =>
            {
                var bgScene = _moveOptions
                    .First(choice => choice.text == menuItem.Text)
                    .tags
                        .First(value => 
                            value.ToLower() != "move" &&
                            value.ToLower() != "locked");
                
                var rootPrefab = Resources.Load<GameObject>($"BGScenes/{bgScene}");
                var sprite = rootPrefab.transform.Find("Background").GetComponent<SpriteRenderer>().sprite;
                _investigationMoveMenu.transform.parent.Find("SceneImage").GetComponent<Image>().sprite = sprite;
            });
        }
        selectableAndLabel.First().Selectable.Select();
    }
}