using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Tests.PlayModeTests.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.PlayModeTests.Suites.Scripts.InvestigationState
{
    public class InvestigationStateKeyboardTests : InvestigationStateTest
    {
        [UnityTest]
        public IEnumerator InvestigationMenuInitialTalkSegmentPlaysFirst()
        {
            yield return SelectAndSkipThroughInitialTalk();
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Talk, 0);
        }
        
        [UnityTest]
        public IEnumerator InvestigationMenuCanUnlockAndMarkChoices()
        {
            var currentScriptName = NarrativeScriptPlayerComponent.NarrativeScriptPlayer.ActiveNarrativeScript.Script.name;
            
            // Select move option 1
            string currentPreviewImageName = null;
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Move, 0, () => {
                Assert.AreEqual(2, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
                currentPreviewImageName = InvestigateMoveContainer.transform.Find("SceneImage").GetComponent<Image>().sprite.texture.name;
                Assert.IsNotEmpty(currentPreviewImageName);
            });
            
            yield return SelectAndSkipThroughInitialTalk();
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Talk, 0, () => {
                Assert.AreEqual(3, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            });
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Talk, 1, () => {
                Assert.AreEqual(3, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
                Assert.AreEqual(1, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Count(item => item.transform.Find("AlreadyExamined").gameObject.activeSelf));
            });
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Talk, 2, () => {
                Assert.AreEqual(4, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
                Assert.AreEqual(2, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Count(item => item.transform.Find("AlreadyExamined").gameObject.activeSelf));
            });
            
            // Select move option 2 verbosely, rather than using SelectChoiceAndReturnToMainMenu, as we'll change scenes and won't return to the main menu
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationMoveMenu.isActiveAndEnabled);
            Assert.True(InvestigateMoveContainer.activeInHierarchy);
            yield return null;
            Assert.AreEqual(4, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
            // Verify option 1 is marked; `Move` choices don't track via checkboxes, rather verify that the preview image has changed
            var newPreviewImageName = InvestigateMoveContainer.transform.Find("SceneImage").GetComponent<Image>().sprite.texture.name;
            Assert.IsNotEmpty(newPreviewImageName);
            Assert.AreNotEqual(currentPreviewImageName, newPreviewImageName);
            
            yield return PressDown();
            yield return PressX();
            
            Assert.False(InvestigationMoveMenu.isActiveAndEnabled);
            Assert.False(InvestigateMoveContainer.activeInHierarchy);
            
            yield return StoryProgresser.ProgressStory();
            
            // Verify that we've moved on to a different script
            Assert.AreNotEqual(currentScriptName, NarrativeScriptPlayerComponent.NarrativeScriptPlayer.ActiveNarrativeScript.Script.name);
        }

        [UnityTest] public IEnumerator InvestigationMenuCanUnlockAndLockTalkChoices()
        {
            yield return SelectAndSkipThroughInitialTalk();
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Talk, 1, () => {
                Assert.AreEqual(3, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            });
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Talk, 2, () => {
                Assert.AreEqual(4, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            });
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Talk, 0, () => {
                Assert.AreEqual(3, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            });
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Talk, 2, () => {
                Assert.AreEqual(4, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            });
        }
        
        [UnityTest]
        public IEnumerator InvestigationMenuCanUnlockAndLockMoveChoices()
        {
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Move, 0, () => {
                Assert.AreEqual(2, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length); 
            });
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Move, 1, () => {
                Assert.AreEqual(3, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
            });
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Move, 0, () => {
                Assert.AreEqual(2, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
            });
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Move, 1, () => {
                Assert.AreEqual(3, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
            });
        }
        
        [UnityTest]
        public IEnumerator InvestigationExaminationCanReturnToMainMenu()
        {
            yield return PressX();
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressEsc();
        }
        
        [UnityTest]
        public IEnumerator InvestigationTalkCanReturnToMainMenuUsingBackButton()
        {
            yield return SelectAndSkipThroughInitialTalk();
            yield return SelectBackButton(IInvestigationState.ChoiceType.Talk);
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Talk, 0, () => {
                Assert.AreEqual(3, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
                Assert.AreEqual(0, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Count(item => item.transform.Find("AlreadyExamined").gameObject.activeSelf));
            });
        }
        
        [UnityTest]
        public IEnumerator InvestigationMoveCanReturnToMainMenuUsingBackButton()
        {
            string previewImageName = null;
            yield return SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType.Move, 1, () => {
                Assert.AreEqual(2, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
                previewImageName = InvestigateMoveContainer.transform.Find("SceneImage").GetComponent<Image>().sprite.texture.name;
                Assert.IsNotEmpty(previewImageName);
            });
            
            yield return SelectBackButton(IInvestigationState.ChoiceType.Move, () => {
                Assert.AreEqual(2, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
                Assert.AreEqual(previewImageName, InvestigateMoveContainer.transform.Find("SceneImage").GetComponent<Image>().sprite.texture.name);
            });
        }

        #region Helper methods
        private IEnumerator SelectAndSkipThroughInitialTalk()
        {
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);

            yield return PressRight();
            yield return PressX();
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            
            while (!InvestigationMainMenu.isActiveAndEnabled) {
                yield return StoryProgresser.ProgressStory();
            }
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
        }

        private IEnumerator SelectChoiceAndReturnToMainMenu(IInvestigationState.ChoiceType choiceType, int index, System.Action onSubmenuOpen = null)
        {
            // Ensure this is called from the main menu
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            
            yield return PressRight();
            if (choiceType == IInvestigationState.ChoiceType.Move) {
                yield return PressRight();
            }
            yield return PressX();
            // Ensure we're in the correct menu
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True((choiceType == IInvestigationState.ChoiceType.Move ? InvestigationMoveMenu : InvestigationTalkMenu).isActiveAndEnabled);
            onSubmenuOpen?.Invoke();
            
            // Select option
            for (var i = 0; i < index; i++) {
                yield return PressDown();
            }

            yield return PressX();
            Assert.False(InvestigationMoveMenu.isActiveAndEnabled);
            
            // Play through the dialogue
            while (!InvestigationMainMenu.isActiveAndEnabled) {
                yield return StoryProgresser.ProgressStory();
            }
            // Assert we're back at the main menu
            Assert.False((choiceType == IInvestigationState.ChoiceType.Move ? InvestigationMoveMenu : InvestigationTalkMenu).isActiveAndEnabled);
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
        }
        
        private IEnumerator SelectBackButton(IInvestigationState.ChoiceType choiceType, System.Action onMenuOpen = null)
        {
            var subMenu = IInvestigationState.ChoiceType.Move == choiceType ? InvestigationMoveMenu : InvestigationTalkMenu;
            
            // Ensure this is called from the main menu
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            
            yield return PressRight();
            if (choiceType == IInvestigationState.ChoiceType.Move) {
                yield return PressRight();
            }
            yield return PressX();
            // Ensure we're in the correct menu
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(subMenu.isActiveAndEnabled);
            onMenuOpen?.Invoke();
            
            // Select back button
            var availableChoices = subMenu.GetComponentsInChildren<MenuItem>().Length-1;
            for (var i = 0; i < availableChoices; i++) {
                yield return PressDown();
            }
            yield return PressX();
            // Assert we're back at the main menu
            Assert.False(subMenu.isActiveAndEnabled);
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
        }
        
        private IEnumerator PressX()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.xKey);
        }
        
        private IEnumerator PressEsc()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.escapeKey);
        }
        
        private IEnumerator PressRight()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.rightArrowKey);
        }
        
        private IEnumerator PressDown()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.downArrowKey);
        }
        #endregion
    }
}
