using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class that contains methods for loading scenes.
/// ChangeScene has overloads for scene index and scene name.
/// LoadScene coroutine keeps track of the progress of the scene loading.
/// Unloads the current scene after new scene has loaded.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField, Tooltip("The name of the scene to load")]
    private string _sceneName;

    [SerializeField, Tooltip("The index of the scene to load")]
    private int _sceneIndex;
    
    [SerializeField, Tooltip("Assign a transition controller here if a transition is required when changing the scene.")]
    private TransitionController _transitionController;

    [SerializeField, Tooltip("Assign a loading bar here if required")]
    private Slider _loadingBar;

    private AsyncOperation _sceneLoadOperation;

    /// <summary>
    /// Call this method when wanting to change the scene using a specific scene's index.
    /// </summary>
    /// <param name="menuNavigator">The menu navigator used to call this method. It is passed in case it needs to be disabled</param>
    public void ChangeSceneBySceneIndex()
    {
        _sceneLoadOperation = SceneManager.LoadSceneAsync(_sceneIndex, LoadSceneMode.Additive);
        Transition();
    }

    /// <summary>
    /// Call this method when wanting to change the scene using a specific scene's name.
    /// </summary>
    /// <param name="menuNavigator">The menu navigator used to call this method. It is passed in case it needs to be disabled</param>
    public void ChangeSceneBySceneName()
    {
        _sceneLoadOperation = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        Transition();
    }

    /// <summary>
    /// Check if there is a _transitionController and call its transition method.
    /// Otherwise load the next scene.
    /// </summary>
    private void Transition()
    {
        if (_transitionController != null)
        {
            if (_sceneLoadOperation != null)
            {
                _sceneLoadOperation.allowSceneActivation = false;
            }
            _transitionController.Transition();

            if (_sceneLoadOperation != null)
            {
                _sceneLoadOperation.allowSceneActivation = true;
            }
        }

        if (_loadingBar != null)
        {
            _loadingBar.gameObject.SetActive(true);
        }
        
        LoadSceneCallback();
    }

    /// <summary>
    /// Called by a transition controller to load the next scene after a transition.
    /// </summary>
    public void LoadSceneCallback()
    {
        StartCoroutine(LoadSceneCoroutine());
    }
    
    /// <summary>
    /// Loads the next scene.
    /// If a loading bar is assigned it will update it with the current progress of the async operation.
    /// </summary>
    private IEnumerator LoadSceneCoroutine()
    {
        _sceneLoadOperation.allowSceneActivation = true;
        yield return null; // Don't show loading bar if it loads in one frame
        
        while (!_sceneLoadOperation.isDone)
        {
            Debug.Log(_sceneLoadOperation.isDone);
            if (_loadingBar != null)
            {
                if (!_loadingBar.gameObject.activeInHierarchy)
                {
                    _loadingBar.gameObject.SetActive(true);
                }

                _loadingBar.value = _sceneLoadOperation.progress;
            }
            yield return null;
        }

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
