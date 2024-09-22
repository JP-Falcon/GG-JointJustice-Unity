using System;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Menu))]
public class ChoiceMenu : MonoBehaviour, IChoiceMenu
{
    [SerializeField] private NarrativeGameState _narrativeGameState;
    
    [Tooltip("Drag the prefab for choice menu items here.")]
    [SerializeField] private MenuItem _choiceMenuItem;

    [Tooltip("Drag the Menu component here.")]
    [SerializeField] private Menu _menu;
    
    [Tooltip("Drag a menu opener component here.")]
    [SerializeField] private MenuOpener _menuOpener;

    public const string BACK_BUTTON_LABEL = "Back";

    /// <summary>
    /// Creates a choice menu using a choice list.
    /// Opens the menu, instantiates the correct number of buttons and
    /// assigns their text and onClick events.
    /// </summary>
    /// <param name="choiceList">The list of choices in the choice menu.</param>
    /// <param name="onBackButtonClick">If the menu should have the option to go back, supply logic that should be executed when the back button is clicked.</param>
    public void Initialise(List<Choice> choiceList, Action onBackButtonClick)
    {
        if (gameObject.activeInHierarchy)
        {
            return; // Don't make another menu if its already active
        }
        
        if (!HasRequiredComponents())
        {
            return;
        }
        
        _menuOpener.OpenMenu();
        
        if (_choiceMenuItem == null)
        {
            Debug.LogError("Could not create choice menu. Choice menu item prefab has not been assigned.", gameObject);
        }
        
        var firstButtonIndex = choiceList.Any(choice => choice.tags != null && choice.tags.Select(choiceTag => choiceTag.ToLower()).Contains("initial")) ? 1 : 0;
        
        foreach (var choice in choiceList)
        {
            if (choice.tags != null && choice.tags.Select(choiceTag => choiceTag.ToLower()).Contains("initial"))
            {
                continue;
            }
            
            var menuItem = Instantiate(_choiceMenuItem, transform);
            if (choice.index == firstButtonIndex)
            {
                _menu.SelectInitialButton();
            }
            menuItem.Text = choice.text;
            ((Button)menuItem.Selectable).onClick.AddListener(() => OnChoiceClicked(choice.index));
        }
        
        if (onBackButtonClick != null)
        {
            var menuItem = Instantiate(_choiceMenuItem, transform);
            menuItem.Text = BACK_BUTTON_LABEL;
            ((Button)menuItem.Selectable).onClick.AddListener(() =>
            {
                onBackButtonClick();
            });
        }
    }

    /// <summary>
    /// Called when a choice is clicked.
    /// Deactivates the menu and calls the HandleChoice method with the given index
    /// </summary>
    /// <param name="choiceIndex"></param>
    private void OnChoiceClicked(int choiceIndex)
    {
        DeactivateChoiceMenu();
        _narrativeGameState.NarrativeScriptPlayerComponent.NarrativeScriptPlayer.HandleChoice(choiceIndex);
    }

    /// <summary>
    /// Call this to deactivate the choice menu after clicking a choice.
    /// </summary>
    public void DeactivateChoiceMenu()
    {
        _menuOpener.CloseMenu();
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Check if all required components have been assigned.
    /// </summary>
    /// <returns>Whether the required components have been assigned (true) or not (false).</returns>
    private bool HasRequiredComponents()
    {
        bool hasRequiredComponents = true;
        
        if (_menu == null)
        {
            PrintMissingComponent("Menu");
            hasRequiredComponents = false;
        }
        
        if (_menuOpener == null)
        {
            PrintMissingComponent("MenuOpener");
            hasRequiredComponents = false;
        }

        return hasRequiredComponents;
    }

    /// <summary>
    /// Method used by HasRequiredComponents method to print an appropriates error message.
    /// </summary>
    /// <param name="componentName">The name of the missing component.</param>
    private void PrintMissingComponent(string componentName)
    {
        Debug.LogError($"{componentName} has not been assigned to component {this} on {gameObject.name}.", this);
    }
}
