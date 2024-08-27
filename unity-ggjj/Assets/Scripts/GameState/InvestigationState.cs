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

    [SerializeField] private MenuOpener InvestigationMainMenuOpener;
    [SerializeField] private ChoiceMenu InvestigationTalkMenu;
    [SerializeField] private ChoiceMenu InvestigationMoveMenu;
    
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
        InvestigationMainMenuOpener.OpenMenu();
        _talkOptions = talkOptions;
        _moveOptions = moveOptions;
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

        var selectableAndLabel = InvestigationMoveMenu
            .GetComponentsInChildren<MenuItem>()
            .Select(menuItem => (menuItem, menuItem.GetComponentInChildren<TextMeshProUGUI>().text))
            .ToList();
        
        foreach (var (menuItem, text) in selectableAndLabel)
        {
            valueTuple.menuItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                InvestigationMoveMenu.transform.parent.gameObject.SetActive(false);
            });
            valueTuple.menuItem.OnItemSelect.AddListener(() =>
            {
                var bgScene = _moveOptions
                    .First(choice => choice.text == valueTuple.text)
                    .tags
                        .First(value => 
                            value.ToLower() != "move" &&
                            value.ToLower() != "locked");
                
                var rootPrefab = Resources.Load<GameObject>($"BGScenes/{bgScene}");
                var sprite = rootPrefab.transform.Find("Background").GetComponent<SpriteRenderer>().sprite;
                InvestigationMoveMenu.transform.parent.Find("SceneImage").GetComponent<Image>().sprite = sprite;
            });
        }
        selectableAndLabel.First().menuItem.Selectable.Select();
    }
}