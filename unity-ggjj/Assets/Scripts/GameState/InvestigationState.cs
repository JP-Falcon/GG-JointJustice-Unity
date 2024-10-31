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
    [SerializeField] private InvestigationChoiceMenu _investigationMoveMenu;
    [SerializeField] private NarrativeGameState _narrativeGameState;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private InputModule _gameInputModule;
    [SerializeField] private InputModule _investigationInputModule;

    #region Talk + Move
    [Header("Move")]
    [SerializeField] private Texture2D _unestablishedSceneBackground;

    private List<Choice> _talkOptions;
    private List<Choice> _moveOptions;

    private readonly List<ChoiceState> _choiceStates = new();

    private ChoiceState GetOrCreateChoiceState(string choice, InvestigationChoiceType type)
    {
        var choiceState = _choiceStates.FirstOrDefault(cs => cs.Text == choice && cs.Type == type);
        if (choiceState != null)
        {
            return choiceState;
        }
        choiceState = new ChoiceState(choice, type);
        _choiceStates.Add(choiceState);
        return choiceState;
    }

    private record ChoiceState(string Text, InvestigationChoiceType Type)
    {
        public string Text { get; } = Text;
        public InvestigationChoiceType Type { get; } = Type;
        public bool Examined { get; set; }
        public bool Unlocked { get; set; }
        public bool ExplicitlyLocked { get; set; }
    }

    public void UnlockChoice(string choice, InvestigationChoiceType type)
    {
        var choiceState = GetOrCreateChoiceState(choice, type);
        choiceState.Unlocked = true;
        choiceState.ExplicitlyLocked = false;
    }

    public void LockChoice(string choice, InvestigationChoiceType type)
    {
        var choiceState = GetOrCreateChoiceState(choice, type);
        choiceState.Unlocked = false;
        choiceState.ExplicitlyLocked = true;
    }

    public bool IsChoiceUnlocked(string choice, InvestigationChoiceTag investigationChoiceTag, InvestigationChoiceType type)
    {
        var choiceState = _choiceStates.FirstOrDefault(cs => cs.Text == choice && cs.Type == type);
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
            var choiceState = _choiceStates.FirstOrDefault(cs => cs.Text == choiceTaggedInitial.text && cs.Type == InvestigationChoiceType.Talk);
            if (choiceState == null || !choiceState.Examined)
            {
                _inputManager.SetInput(_gameInputModule);
                _investigationMainMenuOpener.CloseMenu();
                _narrativeGameState.NarrativeScriptPlayerComponent.NarrativeScriptPlayer.HandleChoice(0);
                choiceState = GetOrCreateChoiceState(choiceTaggedInitial.text, InvestigationChoiceType.Talk);
                choiceState.Examined = true;
                return;
            }
        }

        _investigationMainMenuOpener.CloseMenu();
        _investigationTalkMenu.Initialise(_talkOptions, () =>
        {
            _investigationTalkMenu.DeactivateChoiceMenu();
            OpenWithChoices(_talkOptions, _moveOptions);
        }, (menuItem) =>
        {
            var choiceState = GetOrCreateChoiceState(menuItem.Text, InvestigationChoiceType.Talk);
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
        _investigationMoveMenu.ChoiceMenu.Initialise(_moveOptions, () =>
        {
            _investigationMoveMenu.ChoiceMenu.DeactivateChoiceMenu();
            _investigationMoveMenu.transform.parent.gameObject.SetActive(false);
            OpenWithChoices(_talkOptions, _moveOptions);
        }, menuItem =>
        {
            var choiceState = GetOrCreateChoiceState(menuItem.Text, InvestigationChoiceType.Move);
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
                    _investigationMoveMenu.SceneImage.sprite = Sprite.Create(_unestablishedSceneBackground, new Rect(0, 0, _unestablishedSceneBackground.width, _unestablishedSceneBackground.height), new Vector2(0.5f, 0.5f));
                    return;
                }

                var bgScene = _moveOptions
                    .First(choice => choice.text == menuItem.Text)
                    .tags
                    .First(value =>
                        value.ToLower() != "move" &&
                        value.ToLower() != "locked");

                var rootPrefab = Resources.Load<GameObject>($"BGScenes/{bgScene}");
                var sprite = rootPrefab.transform.Find("Background").GetComponent<SpriteRenderer>().sprite;
                _investigationMoveMenu.SceneImage.sprite = sprite;
            });

            if (menuItem.transform.GetSiblingIndex() == 0)
            {
                menuItem.Selectable.Select();
            }
        });
    }
    #endregion

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
}
