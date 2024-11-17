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
    public const string ID_TAG_KEY = "id";
    public const string BACKGROUND_TAG_KEY = "background";
    
    [SerializeField] private MenuOpener _investigationMainMenuOpener;
    [SerializeField] private NarrativeGameState _narrativeGameState;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private InputModule _gameInputModule;

    #region Examine
    [Header("Examine :: Mouse cursor")]
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
            var detailKey = $"{_narrativeGameState.SceneController.ActiveSceneName}_{_hoveredDetail.NarrativeScriptToPlay.name}";
            var choiceState = GetOrCreateChoiceState(detailKey, InvestigationChoiceType.Examine);
            choiceState.Examined = true;
            _hoveredDetail.AttemptPickUp();
            _narrativeGameState.ActorController.SetVisibility(true, null);
            _narrativeGameState.NarrativeScriptPlayerComponent.NarrativeScriptPlayer.GameMode = GameMode.Investigation;
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

        var detailKey = $"{_narrativeGameState.SceneController.ActiveSceneName}_{_hoveredDetail.NarrativeScriptToPlay.name}";
        var choiceState = GetOrCreateChoiceState(detailKey, InvestigationChoiceType.Examine);

        Cursor.SetCursor(_examinationNewEvidence, Vector2.zero, CursorMode.Auto);

        if (choiceState.Examined)
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
        _narrativeGameState.ActorController.SetVisibility(true, null);
        _investigationMainMenuOpener.OpenMenu();
    }
    #endregion
    
    #region Talk + Move
    [Header("Talk + Move")]
    [SerializeField] private ChoiceMenu _investigationTalkMenu;
    [SerializeField] private InvestigationChoiceMenu _investigationMoveMenu;
    [SerializeField] private Sprite _unestablishedSceneBackground;

    private List<Choice> _talkOptions;
    private List<Choice> _moveOptions;

    private readonly List<ChoiceState> _choiceStates = new();

    private ChoiceState GetOrCreateChoiceState(string choiceId, InvestigationChoiceType type)
    {
        var choiceState = _choiceStates.FirstOrDefault(cs => cs.ChoiceId == choiceId && cs.Type == type);
        if (choiceState != null)
        {
            return choiceState;
        }
        choiceState = new ChoiceState(choiceId, type);
        _choiceStates.Add(choiceState);
        return choiceState;
    }

    private record ChoiceState(string ChoiceId, InvestigationChoiceType Type)
    {
        public string ChoiceId { get; } = ChoiceId;
        public InvestigationChoiceType Type { get; } = Type;
        public bool Examined { get; set; }
        public bool Unlocked { get; set; }
        public bool ExplicitlyLocked { get; set; }
    }

    public void UnlockChoice(string choiceId, InvestigationChoiceType type)
    {
        var choiceState = GetOrCreateChoiceState(choiceId, type);
        choiceState.Unlocked = true;
        choiceState.ExplicitlyLocked = false;
    }

    public void LockChoice(string choiceId, InvestigationChoiceType type)
    {
        var choiceState = GetOrCreateChoiceState(choiceId, type);
        choiceState.Unlocked = false;
        choiceState.ExplicitlyLocked = true;
    }

    public bool IsChoiceUnlocked(string choiceId, InvestigationChoiceTag investigationChoiceTag, InvestigationChoiceType type)
    {
        var choiceState = _choiceStates.FirstOrDefault(cs => cs.ChoiceId == choiceId && cs.Type == type);
        if (choiceState == null)
        {
            return investigationChoiceTag != InvestigationChoiceTag.Locked;
        }

        return investigationChoiceTag == InvestigationChoiceTag.Locked
            ? choiceState.Unlocked
            : !choiceState.ExplicitlyLocked;
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
        var choiceTaggedInitial = _talkOptions.FirstOrDefault(choice => choice.tags != null && choice.tags.Contains("Initial"));
        if (choiceTaggedInitial != null)
        {
            var choiceState = _choiceStates.FirstOrDefault(cs => cs.ChoiceId == choiceTaggedInitial.GetTagValue(ID_TAG_KEY) && cs.Type == InvestigationChoiceType.Talk);
            if (choiceState is not { Examined: true })
            {
                _inputManager.SetInput(_gameInputModule);
                _investigationMainMenuOpener.CloseMenu();
                _narrativeGameState.NarrativeScriptPlayerComponent.NarrativeScriptPlayer.HandleChoice(0);
                choiceState = GetOrCreateChoiceState(choiceTaggedInitial.GetTagValue(ID_TAG_KEY), InvestigationChoiceType.Talk);
                choiceState.Examined = true;
                return;
            }
        }

        _investigationMainMenuOpener.CloseMenu();
        _investigationTalkMenu.Initialise(_talkOptions, () =>
        {
            _investigationTalkMenu.DeactivateChoiceMenu();
            OpenWithChoices(_talkOptions, _moveOptions);
        }, (menuItem, choice) =>
        {
            var choiceState = GetOrCreateChoiceState(choice.GetTagValue(ID_TAG_KEY), InvestigationChoiceType.Talk);
            menuItem.transform.Find("AlreadyExamined").gameObject.SetActive(choiceState.Examined);
            menuItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (menuItem.Text == ChoiceMenu.BACK_BUTTON_LABEL)
                {
                    return;
                }

                choiceState.Examined = true;
            });
        });
    }

    public void OpenMoveMenu()
    {
        _investigationMainMenuOpener.CloseMenu();
        _investigationMoveMenu.transform.parent.gameObject.SetActive(true);
        _investigationMoveMenu.ChoiceMenu.Initialise(
            _moveOptions,
            OnMoveMenuBackButtonClick,
            OnMoveMenuButtonCreated);
    }

    private void OnMoveMenuButtonCreated(MenuItem menuItem, Choice choice)
    {
        var choiceState = GetOrCreateChoiceState(choice.GetTagValue(ID_TAG_KEY), InvestigationChoiceType.Move);
        menuItem.ShouldIgnoreNextSelectEvent = false;
        menuItem.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (menuItem.Text == ChoiceMenu.BACK_BUTTON_LABEL)
            {
                return;
            }

            choiceState.Examined = true;
            _investigationMoveMenu.transform.parent.gameObject.SetActive(false);
        });
        menuItem.OnItemSelect.AddListener(() =>
        {
            if (menuItem.Text == ChoiceMenu.BACK_BUTTON_LABEL)
            {
                return;
            }

            _investigationMoveMenu.SceneImage.color = Color.white;

            if (!choiceState.Examined)
            {
                _investigationMoveMenu.SceneImage.sprite = _unestablishedSceneBackground;
                return;
            }

            var bgScene = _moveOptions
                .First(moveChoice => moveChoice.text == menuItem.Text)
                .GetTagValue(BACKGROUND_TAG_KEY);

            var rootPrefab = _narrativeGameState.ObjectStorage.GetObject<BGScene>(bgScene);
            var sprite = rootPrefab.transform.Find("Background").GetComponent<SpriteRenderer>().sprite;
            _investigationMoveMenu.SceneImage.sprite = sprite;
        });

        if (menuItem.transform.GetSiblingIndex() == 0)
        {
            menuItem.Selectable.Select();
        }
    }

    private void OnMoveMenuBackButtonClick()
    {
        _investigationMoveMenu.ChoiceMenu.DeactivateChoiceMenu();
        _investigationMoveMenu.transform.parent.gameObject.SetActive(false);
        OpenWithChoices(_talkOptions, _moveOptions);
    }

    #endregion
    
    #region Present
    [Header("Present")]
    [SerializeField] private MenuOpener _investigationEvidenceMenuOpener;

    public void OpenPresentMenu()
    {
        _investigationMainMenuOpener.CloseMenu();
        _investigationEvidenceMenuOpener.OpenMenu();
        var items = _investigationEvidenceMenuOpener.GetComponentsInChildren<EvidenceMenuItem>();
        foreach (var item in items)
        {
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                _investigationEvidenceMenuOpener.CloseMenu();
                _narrativeGameState.NarrativeScriptPlayerComponent.NarrativeScriptPlayer.PresentEvidence(item.CourtRecordObject);
                foreach (var evidenceMenuItem in items)
                {
                    evidenceMenuItem.GetComponent<Button>().onClick.RemoveAllListeners();
                }
            });
            
        }
    }
    #endregion
}
