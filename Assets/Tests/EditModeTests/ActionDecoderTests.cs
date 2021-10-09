using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Tests.EditModeTests
{
    public class ActionDecoderTests
    {
        /// <summary>
        /// Clumsy implementation of IActorController for tests
        /// </summary>
        private class FakeActorController : IActorController
        {
            private bool talking;
            private SpeakingType speakingType;
            private string pose = "";
            private string activeSpeaker = "";
            private string activeActor = "";
            public string emotion = "";
            public string emotionActiveActor;
            public readonly Dictionary<int, string> actorSlots = new Dictionary<int, string>();

            public Action onAnimationDone;

            public void AssignActorToSlot(string actor, int oneBasedSlotIndex)
            {
                this.actorSlots[oneBasedSlotIndex] = actor;
            }

            public void OnAnimationDone()
            {
                onAnimationDone?.Invoke();
            }

            public void PlayEmotion(string emotion, string actorName = null)
            {
                this.emotion = emotion;
                this.emotionActiveActor = actorName;
            }

            public void SetActiveActor(string actor)
            {
                this.activeActor = actor;
            }

            public void SetActiveSpeaker(string actor)
            {
                this.activeSpeaker = actor;
            }

            public void SetPose(string pose, string actorName = null)
            {
                this.pose = pose;
            }

            public void SetSpeakingType(SpeakingType speakingType)
            {
                this.speakingType = speakingType;
            }

            public void StartTalking()
            {
                this.talking = true;
            }

            public void StopTalking()
            {
                this.talking = false;
            }
        }

        [Test]
        public void RunInvalidCommand()
        {
            var decoder = new ActionDecoder();

            Assert.Throws(typeof(UnknownCommandException), () =>
            {
                decoder.OnNewActionLine("spujb");
            });
        }

        [Test]
        public void UseInvalidSyntax()
        {
            var decoder = new ActionDecoder();

            Assert.Throws(typeof(InvalidSyntaxException), () =>
            {
                decoder.OnNewActionLine("&SPEAK:Arin:Dan:Ross");
            });
        }

        [Test]
        public void UseValidSyntax()
        {
            var decoder = new ActionDecoder();
            decoder.OnActionDone = new UnityEvent();
            decoder.ActorController = new FakeActorController();

            Assert.DoesNotThrow(() =>
            {
                decoder.OnNewActionLine("&SPEAK:Arin");
            });
        }

        [Test]
        public void TestSetActorPosition()
        {
            // Arrange
            var decoder = new ActionDecoder();
            decoder.OnActionDone = new UnityEvent();
            var actorController = new FakeActorController();
            decoder.ActorController = actorController;
            bool actionDoneCalled = false;
            decoder.OnActionDone.AddListener(() =>
            {
                actionDoneCalled = true;
            });

            // Act
            decoder.OnNewActionLine("&SET_ACTOR_POSITION:3,Arin "); // <-- that space needs to be there... weird

            // Assert
            Assert.AreEqual("Arin", actorController.actorSlots[3]);
            Assert.IsTrue(actionDoneCalled);
        }

        [Test]
        public void TestPlayEmotionWithoutOptionalParameter()
        {
            // Arrange
            var decoder = new ActionDecoder();
            decoder.OnActionDone = new UnityEvent();
            var actorController = new FakeActorController();
            decoder.ActorController = actorController;
            bool actionDoneCalled = false;
            decoder.OnActionDone.AddListener(() =>
            {
                actionDoneCalled = true;
            });

            // Act
            decoder.OnNewActionLine("&PLAY_EMOTION:Happy ");

            // Assert
            Assert.AreEqual("Happy", actorController.emotion);
            Assert.IsFalse(actionDoneCalled);
        }
    }
}