using System;
using System.Collections.Generic;
using System.Linq;
using Ink;
using Ink.Runtime;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditModeTests.Suites
{
    public class NarrativeScriptTests
    {
        private const string TEST_SCRIPT = "This is a test script" +
                                           "&ACTOR:Arin\n" +
                                           "&ACTOR:Arin\n" +
                                           "&ACTOR:Dan\n" +
                                           "&ACTOR:Dan\n" +
                                           "&SPEAK:Arin\n" +
                                           "&SPEAK:Arin\n" +
                                           "&SET_ACTOR_POSITION:1,Jory\n" +
                                           "&SET_ACTOR_POSITION:1,Jory\n" +
                                           "&SCENE:TMPHCourt\n" +
                                           "&SCENE:TMPHCourt\n" +
                                           "&SHOW_ITEM:BentCoins,Right\n" +
                                           "-> NamedContainer\n" +
                                           "=== NamedContainer ===\n" +
                                           "&SHOW_ITEM:Bent_Coins,Right\n" +
                                           "&ADD_EVIDENCE:StolenDinos\n" +
                                           "&ADD_EVIDENCE:StolenDinos\n" +
                                           "&ADD_RECORD:TutorialBoy\n" +
                                           "&ADD_RECORD:TutorialBoy\n" +
                                           "&PLAY_SFX:Damage1\n" +
                                           "&PLAY_SFX:Damage1\n" +
                                           "&PLAY_SONG:ABoyAndHisTrial\n" +
                                           "&PLAY_SONG:ABoyAndHisTrial\n" +
                                           "-> END";

        [Test]
        public void ReadScriptRunsCorrectNumberOfActions()
        {
            var story = ParseStory(TEST_SCRIPT);

            var uniqueActionLines = TEST_SCRIPT.Split('\n')
                .Distinct()
                .Where(line => line.StartsWith("&"))
                .ToList();
            var methodCalls = new List<string>();

            var objectPreloaderMock = new Mock<IActionDecoder>();
            objectPreloaderMock.Setup(mock => mock.InvokeMatchingMethod(It.IsAny<string>()))
                .Callback<string>(line => methodCalls.Add(line));

            _ = new NarrativeScript(new TextAsset(story.ToJson()), objectPreloaderMock.Object);

            foreach (var uniqueActionLine in uniqueActionLines)
            {
                Assert.That(methodCalls, Has.Exactly(1).EqualTo($"{uniqueActionLine}"));
            }
        }

        [Test]
        public void ScriptReadingParsesStitches()
        {
            const string TEST_ACTION_1 = "&TESTACTION1";
            const string TEST_ACTION_2 = "&TESTACTION2";
            
            const string SCRIPT =
                "Test Script\n" +
                "<- TestKnot.TestStitch1\n" +
                "<- TestKnot.TestStitch2\n" +
                "=== TestKnot\n" +
                "= TestStitch1\n" +
                TEST_ACTION_1 + "\n" +
                "-> DONE\n" +
                "= TestStitch2\n" +
                TEST_ACTION_2 + "\n" +
                "-> DONE";

            var storyData = GetStoryDataFromScript(SCRIPT);
            
            Assert.That(storyData.DistinctActions, Has.Exactly(1).EqualTo(TEST_ACTION_1));
            Assert.That(storyData.DistinctActions, Has.Exactly(1).EqualTo(TEST_ACTION_2));
        }
        
        [TestCaseSource(nameof(ChoicePathsTestCases))]
        public void ScriptReadingExploresMultipleChoicePaths(string script, IEnumerable<string> expectedActions)
        {
            var storyData = GetStoryDataFromScript(script);
            Assert.That(storyData.DistinctActions, Is.EquivalentTo(expectedActions));
        }
        
        private static TestCaseData[] ChoicePathsTestCases => new[]
        {
            new TestCaseData(EXPLORE_CHOICES_SCRIPT, CreateTestActions(6)).SetName("Explores multiple choices"),
            new TestCaseData(DEAD_END_CHOICE_SCRIPT, CreateTestActions(5)).SetName("Handles dead end choices"),
            new TestCaseData(TUNNEL_SCRIPT, CreateTestActions(2)).SetName("Handle tunnels")
        };
        
        private const string EXPLORE_CHOICES_SCRIPT =
            "Test Script\n" +
            "-> Choice1\n" +
            "=== Choice1\n" +
            "+ [Choice1_Option1]\n" +
            "&TESTACTION1\n" +
            "-> Choice1\n" +
            "+ [Choice1_Option2]\n" +
            "&TESTACTION2\n" +
            "-> Choice1\n" +
            "+ [Choice1_Option3]\n" +
            "&TESTACTION3\n" +
            "-> Choice2\n" +
            "=== Choice2\n" +
            "+ [Choice2_Option1]\n" +
            "&TESTACTION4\n" +
            "-> Choice2\n" +
            "+ [Choice2_Option2]\n" +
            "&TESTACTION5\n" +
            "-> Choice2\n" +
            "+ [Choice2_Option3]\n" +
            "&TESTACTION6\n" +
            "-> END\n";
        
        private const string DEAD_END_CHOICE_SCRIPT =
            "Test Script\n" +
            "-> Choice1\n" +
            "=== Choice1\n" +
            "+ [Choice1_Option1]\n" +
            "&TESTACTION1\n" +
            "-> DeadEndChoice\n" +
            "+ [Choice1_Option2]\n" +
            "&TESTACTION2\n" +
            "-> Choice1\n" +
            "+ [Choice1_Option3]\n" +
            "&TESTACTION3\n" +
            "-> END\n" +
            "=== DeadEndChoice\n" +
            "+ [DeadEndChoice_Option1]\n" +
            "&TESTACTION4\n" +
            "-> END\n" +
            "+ [DeadEndChoice_Option2]\n" +
            "&TESTACTION5\n" +
            "-> END";

        private const string TUNNEL_SCRIPT =
            "-> TestTunnel ->\n" +
            "&TESTACTION2\n" +
            "=== TestTunnel\n" +
            "&TESTACTION1\n" +
            "->->";

        private static IEnumerable<string> CreateTestActions(int numberOfActions) =>
            Enumerable.Range(1, numberOfActions).Select(i => $"&TESTACTION{i}");

        private static Story ParseStory(string script)
        {
            var parser = new InkParser(script);
            var story = parser.Parse().ExportRuntime();
            return story;
        }

        private static StoryData GetStoryDataFromScript(string script)
        {
            var story = ParseStory(script);
            var storyData = NarrativeScript.ReadContent(story);
            return storyData;
        }
    }
}