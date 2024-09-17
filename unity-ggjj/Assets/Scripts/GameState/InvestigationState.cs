using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using Input;
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
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private InputModule _gameInputModule;
    [SerializeField] private InputModule _investigationInputModule;
    
    [Header("Mouse cursor")]
    [SerializeField] private Texture2D examinationNoEvidence;
    [SerializeField] private Texture2D examinationNewEvidence;
    [SerializeField] private Texture2D examinationKnownEvidence;
    
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
        Cursor.SetCursor(examinationNoEvidence, Vector2.zero, CursorMode.Auto);
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
        
        var ray = Camera.main.ScreenPointToRay(_lastCursorPosition);
        var hit = Physics2D.Raycast(ray.origin, Vector3.forward, Mathf.Infinity, LayerMask.GetMask("Detail"));
        
        Gizmos.color = hit.transform != null ? Color.green : Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * 100000);
        Gizmos.DrawWireSphere(hit.point, 0.1f);
    }

    public void OnCursorSelect(InputAction.CallbackContext context)
    {
        if (_hoveredDetail == null)
        {
            return;
        }
        
        _inputManager.SetInput(_gameInputModule);
        
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        _narrativeGameState.ActorController.SetVisibility(true, null);
        _narrativeGameState.NarrativeScriptPlayerComponent.NarrativeScriptPlayer.StartSubStory(new NarrativeScript(_hoveredDetail.NarrativeScriptToPlay));
        _narrativeGameState.NarrativeScriptPlayerComponent.NarrativeScriptPlayer.ActiveNarrativeScriptPlayer.OnNarrativeScriptComplete += () =>
        {
            _narrativeGameState.AppearingDialogueController.TextBoxHidden = true;
            _examinedDetails.Add(_hoveredDetail.NarrativeScriptToPlay.name);
            _hoveredDetail.AttemptPickUp();
        }; 
    }

    Vector2 _lastCursorPosition = Vector2.zero;
    private Detail _hoveredDetail = null;
    public void OnCursorMove(InputAction.CallbackContext context)
    {
        Cursor.SetCursor(examinationNoEvidence, Vector2.zero, CursorMode.Auto);
        _hoveredDetail = null;
        _lastCursorPosition = context.ReadValue<Vector2>();
        var ray = Camera.main.ScreenPointToRay(_lastCursorPosition);
        var hit = Physics2D.Raycast(ray.origin, Vector3.forward, Mathf.Infinity, LayerMask.GetMask("Detail"));
        if (!hit)
        {
            return;
        }
        
        _hoveredDetail = hit.transform.GetComponent<Detail>();
        
        Cursor.SetCursor(examinationNewEvidence, Vector2.zero, CursorMode.Auto);
        
        if (_examinedDetails.Contains(_hoveredDetail.NarrativeScriptToPlay.name))
        {
            Cursor.SetCursor(examinationKnownEvidence, Vector2.zero, CursorMode.Auto);
        }
    }
    
    public void QuitExamination()
    {
        if (!_isExamining)
        {
            return;
        }
        
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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