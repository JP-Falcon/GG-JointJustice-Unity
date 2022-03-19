using System.Collections;
using NUnit.Framework;
using Tests.PlayModeTests.Tools;
using UnityEditor.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Tests.PlayModeTests.Scripts
{
    public class PenaltyManagerTests
    {
        private PenaltyManager _penaltyManager;
        private readonly InputTestTools _inputTestTools = new InputTestTools();

        public Keyboard Keyboard => _inputTestTools.Keyboard;
    
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return SceneManager.LoadSceneAsync("Game");
            TestTools.StartGame("PenaltyTest");
            _penaltyManager = Object.FindObjectOfType<PenaltyManager>();
        }

        [UnityTest]
        public IEnumerator PenaltiesAreEnabledOnCrossExaminationStart()
        {
            Assert.IsTrue(!_penaltyManager.isActiveAndEnabled || _penaltyManager.PenaltiesLeft == 0);
            yield return _inputTestTools.PressForFrame(Keyboard.xKey);
            Assert.IsTrue(_penaltyManager.isActiveAndEnabled);
        }
    
        [UnityTest]
        public IEnumerator PenaltiesAreDisabledOnCrossExaminationEnd()
        {
            yield return _inputTestTools.PressForFrame(Keyboard.xKey);
            Assert.IsTrue(_penaltyManager.isActiveAndEnabled);
            yield return _inputTestTools.PressForFrame(Keyboard.xKey);
            Assert.IsFalse(_penaltyManager.isActiveAndEnabled);
        }
    
        [UnityTest]
        public IEnumerator NumberOfPenaltiesCanBeReset()
        {
            var narrativeScriptPlayer = Object.FindObjectOfType<NarrativeScriptPlayerComponent>();
            var storyProgresser = new StoryProgresser();

            for (int i = 0; i < 3; i++)
            {
                yield return storyProgresser.ProgressStory();
            }
            
            yield return _inputTestTools.PressForFrame(Keyboard.zKey);
            yield return _inputTestTools.PressForFrame(Keyboard.enterKey);

            yield return TestTools.DoUntilStateIsReached(() => storyProgresser.ProgressStory(), () => !narrativeScriptPlayer.NarrativeScriptPlayer.HasSubStory);
            
            Assert.AreEqual(4, _penaltyManager.PenaltiesLeft);
            yield return _inputTestTools.PressForFrame(Keyboard.xKey);
            Assert.AreEqual(5, _penaltyManager.PenaltiesLeft);
        }
    }
}
