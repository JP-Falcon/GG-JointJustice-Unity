using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Object = UnityEngine.Object;

namespace Tests.PlayModeTests.Tools
{
    /// <summary>
    /// Contains useful methods used when testing features that use Unity Input System.
    /// Also contains input devices that should be used to pass ButtonControls to the methods.
    /// </summary>
    public class InputTest : InputTestFixture
    {
        protected Keyboard Keyboard { get; private set; }
        protected Mouse Mouse { get; private set; }

        private EditorWindow _gameViewWindow;

        public override void Setup()
        {
            base.Setup();

            Keyboard = InputSystem.AddDevice<Keyboard>("keyboardMock");
            Mouse = InputSystem.AddDevice<Mouse>("mouseMock");
        }

        public override void TearDown()
        {
            InputSystem.RemoveDevice(Mouse);
            InputSystem.RemoveDevice(Keyboard);

            base.TearDown();
        }

        private EditorWindow GameViewWindow
        {
            get
            {
                if (_gameViewWindow != null)
                {
                    return _gameViewWindow;
                }

                var assembly = typeof(EditorWindow).Assembly;
                var type = assembly.GetType("UnityEditor.GameView");
                _gameViewWindow = EditorWindow.GetWindow(type);
                return _gameViewWindow;
            }
        }

        /// <summary>
        /// Waits for the editor "GameView"-tab to repaint
        /// </summary>
        public IEnumerator WaitForRepaint()
        {
            GameViewWindow.Repaint();
            yield return null;
        }

        /// <summary>
        /// Start this coroutine to press a specified key for one frame.
        /// </summary>
        /// <param name="control">The key to press.</param>
        /// <param name="repeats">The number of times the key should be pressed.</param>
        public IEnumerator PressForFrame(ButtonControl control, int repeats = 1)
        {
            for (var i = 0; i < repeats; i++)
            {
                Press(control);
                GameViewWindow.Repaint();
                yield return null;
                Release(control);
                GameViewWindow.Repaint();
                yield return null;
                yield return null; // Wait for two frames to wait for InputSystem to rebind if necessary
            }
        }

        /// <summary>
        /// Start this coroutine to press a specified key for a specified number of seconds.
        /// </summary>
        /// <param name="control">The key to press.</param>
        /// <param name="seconds">The number of seconds to press the key for.</param>
        /// <param name="repeats">The number of times the key should be pressed.</param>
        public IEnumerator PressForSeconds(ButtonControl control, float seconds, int repeats = 1)
        {
            for (var i = 0; i < repeats; i++)
            {
                Press(control);
                yield return new WaitForSeconds(seconds);
                Release(control);
                yield return null;
            }
        }

        /// <summary>
        /// Sets the mouse to a specific pixel
        /// </summary>
        /// <param name="position">The position to set the mouse to.</param>
        public IEnumerator SetMouseScreenSpacePosition(Vector2 position)
        {
            Set(Mouse.position, position);
            yield return null;
        }
        
        /// <summary>
        /// Sets the position of the mouse in the scene in world space
        /// </summary>
        /// <param name="position">The position to set the mouse to.</param>
        public IEnumerator SetMouseWorldSpacePosition(Vector2 position)
        {
            Set(Mouse.position, Camera.main.WorldToScreenPoint(position));
            yield return null;
        }

        /// <summary>
        /// Spams a button until a particular behaviour is active and enabled.
        /// </summary>
        /// <param name="behaviour">The behaviour to wait for.</param>
        /// <param name="key">The key to press.</param>
        /// <returns></returns>
        public IEnumerator WaitForBehaviourActiveAndEnabled(Behaviour behaviour, ButtonControl key)
        {
            while (!behaviour.isActiveAndEnabled)
            {
                yield return PressForSeconds(key, 0.2f);
            }
        }
        
        /// <summary>
        /// Sets the mouse to specific pixel and clicks
        /// </summary>
        /// <param name="position">The position in pixels to click at</param>
        public IEnumerator ClickAtScreenSpacePosition(Vector2 position)
        {
            yield return SetMouseScreenSpacePosition(position);
            yield return PressForFrame(Mouse.leftButton);
        }

        /// <summary>
        /// Sets the mouse to a position in world space and clicks
        /// </summary>
        /// <param name="position">The position to click at in world space coordinates</param>
        public IEnumerator ClickAtWorldSpacePosition(Vector2 position)
        {
            yield return SetMouseWorldSpacePosition(position);
            yield return PressForFrame(Mouse.leftButton);
        }

        /// <summary>
        /// Holds the X Key until an AppearingDialogueController is not printing text
        /// </summary>
        public IEnumerator ProgressStory()
        {
            Press(Keyboard.xKey);
            yield return TestTools.WaitForState(() => !Object.FindObjectOfType<AppearingDialogueController>().IsPrintingText);
            Release(Keyboard.xKey);
        }
    }
}
