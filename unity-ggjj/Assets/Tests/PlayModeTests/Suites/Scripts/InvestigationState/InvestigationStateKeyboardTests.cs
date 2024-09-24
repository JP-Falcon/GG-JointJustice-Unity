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
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressX();
            
            // Select talk
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            // #initial dialogue plays
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.False(InvestigationTalkMenu.isActiveAndEnabled);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select talk again
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            // Select talk option 1
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationTalkMenu.isActiveAndEnabled);
            yield return PressX();
            
            // #talk option 1 dialogue plays
            Assert.False(InvestigationTalkMenu.isActiveAndEnabled);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Return to the main menu
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
        }
        
        [UnityTest]
        public IEnumerator InvestigationMenuCanUnlockAndMarkChoices()
        {
            var currentScriptName = NarrativeScriptPlayerComponent.NarrativeScriptPlayer.ActiveNarrativeScript.Script.name;
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressX();
            
            // Select move option 1
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationMoveMenu.isActiveAndEnabled);
            Assert.True(InvestigateMoveContainer.activeInHierarchy);
            Assert.AreEqual(2, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
            
            // store preview image as it should change after examining move option 1
            var currentPreviewImageName = InvestigateMoveContainer.transform.Find("SceneImage").GetComponent<Image>().sprite.texture.name;
            Assert.IsNotEmpty(currentPreviewImageName);
            yield return PressX();
            
            Assert.False(InvestigationMoveMenu.isActiveAndEnabled);
            Assert.False(InvestigateMoveContainer.activeInHierarchy);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select talk
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            // #initial dialogue plays
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.False(InvestigationTalkMenu.isActiveAndEnabled);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select talk again
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            // Select talk option 1
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationTalkMenu.isActiveAndEnabled);
            Assert.AreEqual(3, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            yield return PressX();
            
            Assert.False(InvestigationTalkMenu.isActiveAndEnabled);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select talk option 2 and verify option 1 is marked as examined
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationTalkMenu.isActiveAndEnabled);
            yield return null;
            Assert.AreEqual(3, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            Assert.AreEqual(1, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Count(item => item.transform.Find("AlreadyExamined").gameObject.activeSelf));
            yield return PressDown();
            yield return PressX();
            
            Assert.False(InvestigationTalkMenu.isActiveAndEnabled);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select talk option 3 and verify option 2 is marked as examined
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationTalkMenu.isActiveAndEnabled);
            yield return null;
            Assert.AreEqual(4, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            Assert.AreEqual(2, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Count(item => item.transform.Find("AlreadyExamined").gameObject.activeSelf));
            yield return PressDown();
            yield return PressDown();
            yield return PressX();
            
            Assert.False(InvestigationTalkMenu.isActiveAndEnabled);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select move option 2 and verify option 1 is marked as examined
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationMoveMenu.isActiveAndEnabled);
            Assert.True(InvestigateMoveContainer.activeInHierarchy);
            yield return null;
            Assert.AreEqual(3, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
            Assert.AreEqual(1, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Count(item => item.transform.Find("AlreadyExamined").gameObject.activeSelf));
            
            // Verfiy that the preview image has changed
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
        
        [UnityTest]
        public IEnumerator InvestigationExaminationCanReturnToMainMenu()
        {
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);

            yield return PressX();
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            
            // Select Examination button
            yield return PressX();
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            
            yield return PressEsc();
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
        }
        
        [UnityTest]
        public IEnumerator InvestigationTalkCanReturnToMainMenuUsingBackButton()
        {
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressX();
            
            // Select Talk button
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            // #initial dialogue plays
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.False(InvestigationTalkMenu.isActiveAndEnabled);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select talk again
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            // Select back button
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressDown();
            yield return PressDown();
            yield return PressDown();
            yield return PressX();  
            
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            
            // Select Talk button again and verify back button isn't checked
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationTalkMenu.isActiveAndEnabled);
            yield return null;
            Assert.AreEqual(3, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            Assert.AreEqual(0, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Count(item => item.transform.Find("AlreadyExamined").gameObject.activeSelf));
        }
        
        [UnityTest]
        public IEnumerator InvestigationMoveCanReturnToMainMenuUsingBackButton()
        {
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressX();
            
            // Select Move button
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationMoveMenu.isActiveAndEnabled);
            Assert.True(InvestigateMoveContainer.activeInHierarchy);
            yield return null;
            Assert.AreEqual(2, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
            Assert.AreEqual(0, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Count(item => item.transform.Find("AlreadyExamined").gameObject.activeSelf));
            
            // Select back button
            yield return PressDown();
            yield return PressX();
            
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            
            // Select Move button again and verify back button isn't checked
            yield return PressRight();
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationMoveMenu.isActiveAndEnabled);
            yield return null;
            Assert.AreEqual(2, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
            Assert.AreEqual(0, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Count(item => item.transform.Find("AlreadyExamined").gameObject.activeSelf));
        }
        
        private IEnumerator PressX()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.xKey);
        }
        
        private IEnumerator PressEsc()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.escapeKey);
        }
        
        private IEnumerator PressLeft()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.leftArrowKey);
        }
        
        private IEnumerator PressRight()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.rightArrowKey);
        }
        
        private IEnumerator PressUp()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.upArrowKey);
        }
        
        private IEnumerator PressDown()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.downArrowKey);
        }
    }
}
