﻿using System.Collections;
using System.Linq;
using NUnit.Framework;
using Tests.PlayModeTests.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayModeTests.Scenes.MainMenu
{
    public class ViaKeyboard : InputTestTools
    {
        [UnitySetUp]
        public IEnumerator UnitySetUp()
        {
            yield return SceneManager.LoadSceneAsync("MainMenu");
        }

        [UnityTest]
        public IEnumerator CanEnterAndCloseTwoSubMenusIndividually()
        {
            // as the containing GameObjects are enabled, `GameObject.Find()` will not find them
            // and we query all existing menus instead
            Menu[] menus = TestTools.FindInactiveInScene<Menu>();
            Menu mainMenu = menus.First(menu => menu.gameObject.name == "MenuButtons");
            Menu subMenu = menus.First(menu => menu.gameObject.name == "TestSubMenu");
            Menu secondSubMenu = menus.First(menu => menu.gameObject.name == "TestDoubleSubMenu");

            string selectedButton = mainMenu.SelectedButton.name;
            yield return PressForFrame(Keyboard.rightArrowKey);
            string newSelectedButton = mainMenu.SelectedButton.name;
            Assert.AreNotEqual(selectedButton, newSelectedButton);

            Assert.True(mainMenu.Active);
            Assert.False(subMenu.Active);

            yield return PressForFrame(Keyboard.enterKey);
            Assert.False(mainMenu.Active);
            Assert.True(subMenu.Active);

            yield return PressForFrame(Keyboard.enterKey);
            Assert.False(subMenu.Active);
            Assert.True(secondSubMenu.Active);

            yield return PressForFrame(Keyboard.downArrowKey);
            yield return PressForFrame(Keyboard.downArrowKey);
            yield return PressForFrame(Keyboard.downArrowKey);

            yield return PressForFrame(Keyboard.enterKey);
            Assert.False(secondSubMenu.Active);
            Assert.True(subMenu.Active);

            yield return PressForFrame(Keyboard.downArrowKey);
            yield return PressForFrame(Keyboard.downArrowKey);
            yield return PressForFrame(Keyboard.downArrowKey);

            yield return PressForFrame(Keyboard.enterKey);
            Assert.False(subMenu.Active);
            Assert.True(mainMenu.Active);
        }

        [UnityTest]
        public IEnumerator CanEnterAndCloseTwoSubMenusWithCloseAllButton()
        {
            // as the containing GameObjects are enabled, `GameObject.Find()` will not find them
            // and we query all existing menus instead
            Menu[] menus = TestTools.FindInactiveInScene<Menu>();
            Menu mainMenu = menus.First(menu => menu.gameObject.name == "MenuButtons");
            Menu subMenu = menus.First(menu => menu.gameObject.name == "TestSubMenu");
            Menu secondSubMenu = menus.First(menu => menu.gameObject.name == "TestDoubleSubMenu");

            string selectedButton = mainMenu.SelectedButton.name;
            yield return PressForFrame(Keyboard.rightArrowKey);
            string newSelectedButton = mainMenu.SelectedButton.name;
            Assert.AreNotEqual(selectedButton, newSelectedButton);

            Assert.True(mainMenu.Active);
            Assert.False(subMenu.Active);

            yield return PressForFrame(Keyboard.enterKey);
            Assert.False(mainMenu.Active);
            Assert.True(subMenu.Active);

            yield return PressForFrame(Keyboard.enterKey);
            Assert.False(subMenu.Active);
            Assert.True(secondSubMenu.Active);

            yield return PressForFrame(Keyboard.enterKey);
            Assert.False(secondSubMenu.Active);
            Assert.False(subMenu.Active);
            Assert.True(mainMenu.Active);
        }

        [UnityTest]
        public IEnumerator CanStartGame()
        {
            yield return PressForFrame(Keyboard.enterKey);
            yield return TestTools.WaitForState(() => SceneManager.GetActiveScene().name == "Game");
        }
    }
}