using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using PlasticPipe.Tube;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] private NarrativeGameState _narrativeGameState;
    
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
        _talkOptions = talkOptions;
        _moveOptions = moveOptions;
        StartCoroutine(OpenMenuDelayed());
    }
    
    private IEnumerator OpenMenuDelayed()
    {
        yield return null;
        _investigationMainMenuOpener.OpenMenu();
    }

    private bool _isExamining;
    private readonly List<string> _examinedDetails = new();
    public void OpenExaminationMenu()
    {
        _investigationMainMenuOpener.CloseMenu();
        _narrativeGameState.ActorController.SetVisibility(false, null);
        _isExamining = true;
    }

    public void OnDrawGizmos()
    {
        if (!_isExamining)
        {
            return;
        }
        
        // send ray and attempt to hit any Polygon2D ovects
        var ray = Camera.main.ScreenPointToRay(_lastCursorPosition);
        var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Detail"));
        
        Gizmos.color = hit.transform != null ? Color.green : Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * 100000);
        Gizmos.DrawWireSphere(hit.point, 0.1f);
    }

    public void OnCursorSelect()
    {
        // TODO VM: Prevent InvestigationInput Click/Select from changing to GameInput, if click was empty
        if (!_isExamining)
        {
            return;
        }

        if (_hoveredDetail == null)
        {
            return;
        }

        _narrativeGameState.NarrativeScriptPlayerComponent.NarrativeScriptPlayer.StartSubStory(new NarrativeScript(_hoveredDetail.NarrativeScriptToPlay));
        _narrativeGameState.ActorController.SetVisibility(true, null);
        _narrativeGameState.NarrativeScriptPlayerComponent.NarrativeScriptPlayer.ActiveNarrativeScriptPlayer.OnNarrativeScriptComplete += () =>
        {
            _narrativeGameState.AppearingDialogueController.TextBoxHidden = true;
            _examinedDetails.Add(_hoveredDetail.NarrativeScriptToPlay.name);
            _hoveredDetail.AttemptPickUp();
            OpenExaminationMenu();
        }; 
    }

    Vector2 _lastCursorPosition = Vector2.zero;
    private Detail _hoveredDetail = null;
    public void OnCursorMove(InputAction.CallbackContext context)
    {
        _hoveredDetail = null;
        if (!_isExamining)
        {
            return;
        }
        
        _lastCursorPosition = context.ReadValue<Vector2>();
        var ray = Camera.main.ScreenPointToRay(_lastCursorPosition);
        var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Detail"), 0);
        if (!hit)
        {
            return;
        }
        
        _hoveredDetail = hit.transform.GetComponent<Detail>();
        if (!_hoveredDetail)
        {
            Debug.LogError("Hit object does not have a Detail component", hit.transform);
        }

        if (_examinedDetails.Contains(_hoveredDetail.NarrativeScriptToPlay.name))
        {
            // TODO VM: Change cursor
        }
    }
    
    public void QuitExamination()
    {
        if (!_isExamining)
        {
            return;
        }
        _isExamining = false;
        _investigationMainMenuOpener.OpenMenu();
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