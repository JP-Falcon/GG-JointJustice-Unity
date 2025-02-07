using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Defines a menu. Used by MenuOpener to enable and disable the menu.
/// Keeps track of which menu item in the menu should be highlighted.
/// Chooses the initial menu item to be selected.
/// </summary>
public class Menu : MonoBehaviour
{
    [SerializeField, Tooltip("The first button that will be selected")]
    private Button _initiallyHighlightedButton;
    
    [field: SerializeField, Tooltip("Enable this if you want the selected button to be the same as when you closed the menu")]
    public bool DontResetSelectedOnClose { get; private set; }

    [SerializeField, Tooltip("Event called when menu is closed")]
    public UnityEvent OnClosed;

    
    public UnityEvent<bool> OnSetInteractable { get; } = new UnityEvent<bool>();
    public Selectable SelectedButton { get; set; } // Set by child buttons when they are selected
    public bool Active => gameObject.activeInHierarchy && (SelectedButton == null || SelectedButton.enabled); // Returns true when no child menus are active ONLY if this menu is enabled
    public MenuOpener ChildMenuOpener { get; set; }
    public bool ChildMenuOpened => ChildMenuOpener != null;

    
    private void OnEnable()
    {
        // This is required, as the EventSystem is never cleared of the last selected object if this menu was opened before
        // This means that if you open a menu, close it, and then open it again, the last selected button will
        // >internally< remain selected, but no events are fired on reinitialized menu items, so the button will not be highlighted
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnDisable()
    {
        OnClosed.Invoke();
    }

    /// <summary>
    /// Enables and disables this section of menu.
    /// Used when opening sub menus.
    /// </summary>
    /// <param name="interactable">Whether the buttons should be interactable or not</param>
    public void SetMenuInteractable(bool interactable)
    {
        OnSetInteractable?.Invoke(interactable);
    }
    
    /// <summary>
    /// If set, selects an initial button other than the first one in the hierarchy.
    /// </summary>
    public void SelectInitialButton(bool shouldIgnoreNextSelectEvent = false)
    {
        if (_initiallyHighlightedButton == null || !_initiallyHighlightedButton.isActiveAndEnabled)
        {
            if (transform.childCount == 0)
            {
                return;
            }

            Selectable selectable = GetComponentInChildren<Selectable>();
            
            if (selectable == null)
            {
                return;
            }
            
            if (EventSystem.current.currentSelectedGameObject == selectable.gameObject)
            {
                return;
            }

            if (selectable.interactable)
            {
                selectable.Select();
            }
            return;
        }
        
        EventSystem.current.SetSelectedGameObject(null); // Select event is not called if selecting the game object that is already selected
        if (shouldIgnoreNextSelectEvent)
        {
            _initiallyHighlightedButton.GetComponent<MenuItem>().ShouldIgnoreNextSelectEvent = true;
        }
        _initiallyHighlightedButton.Select();
    }
}
