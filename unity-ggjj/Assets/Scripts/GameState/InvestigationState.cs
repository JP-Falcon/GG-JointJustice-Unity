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

    [SerializeField] private MenuOpener _investigationMainMenuOpener;
    [SerializeField] private ChoiceMenu _investigationTalkMenu;
    [SerializeField] private ChoiceMenu _investigationMoveMenu;
    [SerializeField] private NarrativeGameState _narrativeGameState;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private InputModule _gameInputModule;
    [SerializeField] private InputModule _investigationInputModule;
    
    #region Talk + Move
    public enum ChoiceType
    {
        Talk,
        Move
    }
    private List<Choice> _talkOptions;
    private List<Choice> _moveOptions;
    
    private readonly List<string> _unlockedTalkChoices = new();
    private readonly List<string> _unlockedMoveChoices = new();
    private readonly List<string> _examinedTalkChoices = new();
    private readonly List<string> _examinedMoveChoices = new();
    private readonly List<string> _examinedDetails = new();

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
    
    public void OpenTalkMenu()
    {
        _investigationMainMenuOpener.CloseMenu();
        _investigationTalkMenu.Initialise(_talkOptions);
        var selectableAndLabel = _investigationTalkMenu.GetComponentsInChildren<MenuItem>().ToList();

        foreach (var menuItem in selectableAndLabel)
        {
            menuItem.transform.Find("AlreadyExamined").gameObject.SetActive(_examinedTalkChoices.Contains(_narrativeGameState.SceneController.ActiveSceneName + "_" + menuItem.Text));
            menuItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                _examinedTalkChoices.Add(_narrativeGameState.SceneController.ActiveSceneName + "_" + menuItem.Text);
            });
        }
    }

    public void OpenMoveMenu()
    {
        _investigationMainMenuOpener.CloseMenu();
        _investigationMoveMenu.transform.parent.gameObject.SetActive(true);
        _investigationMoveMenu.Initialise(_moveOptions);

        var selectableAndLabel = _investigationMoveMenu.GetComponentsInChildren<MenuItem>().ToList();

        foreach (var menuItem in selectableAndLabel)
        {
            menuItem.transform.Find("AlreadyExamined").gameObject.SetActive(_examinedMoveChoices.Contains(_narrativeGameState.SceneController.ActiveSceneName + "_" + menuItem.Text));
            menuItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                _investigationMoveMenu.transform.parent.gameObject.SetActive(false);
            });
            menuItem.OnItemSelect.AddListener(() =>
            {
                _examinedMoveChoices.Add(_narrativeGameState.SceneController.ActiveSceneName + "_" + menuItem.Text);
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
    #endregion

    #region Examine
    [Header("Mouse cursor")]
    [SerializeField] private Texture2D _examinationNoEvidence;
    [SerializeField] private Texture2D _examinationNewEvidence;
    [SerializeField] private Texture2D _examinationKnownEvidence;

    private Vector2 _lastCursorPosition = Vector2.zero;
    private Detail _hoveredDetail;
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
            _examinedDetails.Add($"{_narrativeGameState.SceneController.ActiveSceneName}_{_hoveredDetail.NarrativeScriptToPlay.name}");
            _hoveredDetail.AttemptPickUp();
        }; 
    }

    public void OnCursorMove(InputAction.CallbackContext context)
    {
        Cursor.SetCursor(_examinationNoEvidence, Vector2.zero, CursorMode.Auto);
        _hoveredDetail = null;
        _lastCursorPosition = context.ReadValue<Vector2>();
        var ray = Camera.main!.ScreenPointToRay(_lastCursorPosition);
        var hit = Physics2D.Raycast(ray.origin, Vector3.forward, Mathf.Infinity, LayerMask.GetMask("Detail"));
        if (!hit)
        {
            return;
        }
        
        _hoveredDetail = hit.transform.GetComponent<Detail>();
        
        Cursor.SetCursor(_examinationNewEvidence, Vector2.zero, CursorMode.Auto);
        
        if (_examinedDetails.Contains($"{_narrativeGameState.SceneController.ActiveSceneName}_{_hoveredDetail.NarrativeScriptToPlay.name}"))
        {
            Cursor.SetCursor(_examinationKnownEvidence, Vector2.zero, CursorMode.Auto);
        }
    }
    
    public void OpenExaminationMenu()
    {
        Cursor.SetCursor(_examinationNoEvidence, Vector2.zero, CursorMode.Auto);
        _investigationMainMenuOpener.CloseMenu();
        _narrativeGameState.ActorController.SetVisibility(false, null);
    }

    public void QuitExamination()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        _investigationMainMenuOpener.OpenMenu();
    }
    #endregion
 
}