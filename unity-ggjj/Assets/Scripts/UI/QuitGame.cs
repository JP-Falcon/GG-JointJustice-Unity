using System.Collections;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private ITransition _transition;
    
    private void Awake()
    {
        _transition = GetComponent<ITransition>();
    }
    
    /// <summary>
    /// Called "on select" to run the transition which, in turn, will call the `ExecuteQuit()` method when ending the transition
    /// </summary>
    public void QueueTransition()
    {
        if (_transition != null)
        {
            _transition.Transition(ExecuteQuit);
        }
        else
        {
            ExecuteQuit(); // No ITransition attached? Then quit without a transition.
        }
    }

    /// <summary>
    /// Selects a method to quit the game depending on whether this is run in Editor or not
    /// </summary>
    private void ExecuteQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
