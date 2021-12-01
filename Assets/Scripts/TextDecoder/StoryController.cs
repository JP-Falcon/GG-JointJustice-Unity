using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SceneLoader))]
public class StoryController : MonoBehaviour
{
    [Tooltip("List of inky dialogue scripts to be played in order")]
    [SerializeField] private List<Dialogue> _dialogueList;

    [Header("Events")]
    
    [SerializeField] private UnityEvent<Dialogue> _onNextDialogueScript;
    [SerializeField] private UnityEvent<int> _onCrossExaminationStart;

    private SceneLoader _sceneLoader;
    private int _currentStory = -1;

    /// <summary>
    /// Initializes variables
    /// </summary>
    private void Awake()
    {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    /// <summary>
    /// Starts the first dialogue script.
    /// This script should always be last in the run order in order for this to work properly.
    /// </summary>
    private void Start()
    {
        if (_dialogueList.Count == 0)
        {
            Debug.LogWarning("No narrative scripts assigned to StoryController", this);
        }
        else
        {
            RunNextDialogueScript();
        }
    }

    /// <summary>
    /// Loads the next dialogue script. If it doesn't exist, loads the next unity scene.
    /// </summary>
    public void RunNextDialogueScript()
    {
        if (_dialogueList.Count <= 0)
            return;

        _currentStory++;
        if (_currentStory >= _dialogueList.Count)
        {
            if (!_sceneLoader.Busy)
                _sceneLoader.ChangeSceneBySceneName();
        }
        else
        {
            if (_dialogueList[_currentStory].ScriptType == DialogueControllerMode.CrossExamination)
            {
                _onCrossExaminationStart.Invoke(5);
            }
            _onNextDialogueScript.Invoke(_dialogueList[_currentStory]);
        }
    }
}
