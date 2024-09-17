using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayModeTests.Suites.Scripts.InvestigationState
{
    public class InvestigationStateMouseTests : InvestigationStateTest
    {
        private IEnumerator HoverOverDetail(string detailName)
        {
            var detailContainer = GameObject.Find(detailName);
            if (detailContainer == null)
            {
                throw new ArgumentNullException(nameof(detailContainer), $"Detail with name {detailName} not found.");
            }
            var detailComponent = detailContainer.GetComponent<Detail>();
            if (detailComponent == null)
            {
                throw new ArgumentNullException(nameof(detailComponent), $"Detail component not found on {detailName}.");
            }

            yield return StoryProgresser.SetMouseWorldSpacePosition(detailComponent.GetComponent<PolygonCollider2D>().bounds.center);
        }

        private IEnumerator LeftClick()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.mouse.leftButton);
        }
        
        /// <summary>
        /// Attempts to pick up a detail.
        /// </summary>
        [UnityTest]
        public IEnumerator InvestigationExaminationCanPickUpDetail()
        {
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressX();
            
            // Select Examination button
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressX();
            
            // Hover over nothing and click to ensure nothing happens
            yield return StoryProgresser.SetMouseWorldSpacePosition(new Vector3(0, 0, 0));
            yield return LeftClick();
            
            // Hover over and click on Stool
            yield return HoverOverDetail("Stool");
            yield return LeftClick();
            
            Assert.True(SpeechPanel.activeInHierarchy);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
            // Select Examination button again
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressX();
            
            // Assert that the picked-up detail is no longer available
            var exception = Assert.Throws<ArgumentNullException>(() => HoverOverDetail("Stool").MoveNext());
            Assert.AreEqual("detailContainer", exception.ParamName);
        }
        
        /// <summary>
        /// Attempts to select a detail that can't be picked up.
        /// </summary>
        [UnityTest]
        public IEnumerator InvestigationExaminationCanPlayDetailWithoutPickupTwice()
        {
            Assert.False(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressX();
            
            // Select Examination button
            Assert.True(InvestigationMainMenu.isActiveAndEnabled);
            yield return PressX();
            
            // Hover over nothing and click to ensure nothing happens
            yield return StoryProgresser.SetMouseWorldSpacePosition(new Vector3(0, 0, 0));
            yield return LeftClick();
            
            // Hover over and click on Stool
            yield return HoverOverDetail("Speaker");
            yield return LeftClick();
            
            Assert.True(SpeechPanel.activeInHierarchy);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            // Wait for menu to be ready
            yield return null;
            
            // Select Examination button again
            yield return PressX();
            
            // Hover over nothing and click to ensure nothing happens
            yield return StoryProgresser.SetMouseWorldSpacePosition(new Vector3(0, 0, 0));
            yield return LeftClick();
            
            // Hover over and click on Stool
            yield return HoverOverDetail("Speaker");
            yield return LeftClick();
            
            Assert.True(SpeechPanel.activeInHierarchy);
            while (!InvestigationMainMenu.isActiveAndEnabled)
            {
                yield return StoryProgresser.ProgressStory();
            }
            
        }
        
        private IEnumerator PressX()
        {
            yield return StoryProgresser.PressForFrame(StoryProgresser.keyboard.xKey);
        }
    }
}
