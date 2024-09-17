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
        public IEnumerator InvestigationMenuCanUnlockTalkOptions()
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
            Assert.AreEqual(1, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
            yield return PressX();
            
            Assert.False(InvestigationMoveMenu.isActiveAndEnabled);
            Assert.False(InvestigateMoveContainer.activeInHierarchy);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select talk option 1
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationTalkMenu.isActiveAndEnabled);
            Assert.AreEqual(2, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            yield return PressX();
            
            Assert.False(InvestigationTalkMenu.isActiveAndEnabled);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select talk option 2
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationTalkMenu.isActiveAndEnabled);
            Assert.AreEqual(2, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            yield return PressDown();
            yield return PressX();
            
            Assert.False(InvestigationTalkMenu.isActiveAndEnabled);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select talk option 3
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationTalkMenu.isActiveAndEnabled);
            Assert.AreEqual(3, InvestigationTalkMenu.GetComponentsInChildren<MenuItem>().Length);
            yield return PressDown();
            yield return PressDown();
            yield return PressX();
            
            Assert.False(InvestigationTalkMenu.isActiveAndEnabled);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select move option 2
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressRight();
            yield return PressRight();
            yield return PressX();
            
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            Assert.True(InvestigationMoveMenu.isActiveAndEnabled);
            Assert.True(InvestigateMoveContainer.activeInHierarchy);
            Assert.AreEqual(2, InvestigationMoveMenu.GetComponentsInChildren<MenuItem>().Length);
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
