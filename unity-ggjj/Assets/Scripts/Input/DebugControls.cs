// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/DebugControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DebugControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DebugControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DebugControls"",
    ""maps"": [
        {
            ""name"": ""Keyboard/Mouse"",
            ""id"": ""40c3f060-180e-43d6-981b-b4ece39e00eb"",
            ""actions"": [
                {
                    ""name"": ""ReloadScript"",
                    ""type"": ""Button"",
                    ""id"": ""948de251-4bce-431b-b749-fa13b7c4499b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenEditor"",
                    ""type"": ""Button"",
                    ""id"": ""dce34266-8a6c-4591-a6e9-d2e1fa6dca54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""6989c641-2aa1-4c80-9640-99fc81755009"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenEditor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""cd357d8f-24b0-477a-80bd-7bf1653f4940"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenEditor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""1560bb35-bd93-4f6d-93f6-283feb1179f8"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenEditor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""4e3ea4bf-5f8d-49d3-a489-e6fea35a68cc"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadScript"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""91e7de19-54aa-4f02-a436-321ed1016798"",
                    ""path"": ""<Keyboard>/leftMeta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadScript"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""758dcdc5-5149-4888-9100-0b8da9fd631b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadScript"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""3d27f092-b388-4256-af07-dc501c179b20"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadScript"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""670beeea-f43b-4c15-9592-725c686962ed"",
                    ""path"": ""<Keyboard>/rightMeta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadScript"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""ed4d0d27-a30e-4b50-a78b-77627a58ce46"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadScript"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""a45ff7d8-b3f7-401e-8c66-e7315249d7a4"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadScript"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""5ab825e6-3501-44b6-8aab-f4ae1a4e8bbb"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadScript"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""47d5bbe6-dd77-40b1-8e42-080fe23657ed"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadScript"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard/Mouse
        m_KeyboardMouse = asset.FindActionMap("Keyboard/Mouse", throwIfNotFound: true);
        m_KeyboardMouse_ReloadScript = m_KeyboardMouse.FindAction("ReloadScript", throwIfNotFound: true);
        m_KeyboardMouse_OpenEditor = m_KeyboardMouse.FindAction("OpenEditor", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Keyboard/Mouse
    private readonly InputActionMap m_KeyboardMouse;
    private IKeyboardMouseActions m_KeyboardMouseActionsCallbackInterface;
    private readonly InputAction m_KeyboardMouse_ReloadScript;
    private readonly InputAction m_KeyboardMouse_OpenEditor;
    public struct KeyboardMouseActions
    {
        private @DebugControls m_Wrapper;
        public KeyboardMouseActions(@DebugControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ReloadScript => m_Wrapper.m_KeyboardMouse_ReloadScript;
        public InputAction @OpenEditor => m_Wrapper.m_KeyboardMouse_OpenEditor;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardMouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardMouseActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardMouseActions instance)
        {
            if (m_Wrapper.m_KeyboardMouseActionsCallbackInterface != null)
            {
                @ReloadScript.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnReloadScript;
                @ReloadScript.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnReloadScript;
                @ReloadScript.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnReloadScript;
                @OpenEditor.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnOpenEditor;
                @OpenEditor.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnOpenEditor;
                @OpenEditor.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnOpenEditor;
            }
            m_Wrapper.m_KeyboardMouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ReloadScript.started += instance.OnReloadScript;
                @ReloadScript.performed += instance.OnReloadScript;
                @ReloadScript.canceled += instance.OnReloadScript;
                @OpenEditor.started += instance.OnOpenEditor;
                @OpenEditor.performed += instance.OnOpenEditor;
                @OpenEditor.canceled += instance.OnOpenEditor;
            }
        }
    }
    public KeyboardMouseActions @KeyboardMouse => new KeyboardMouseActions(this);
    public interface IKeyboardMouseActions
    {
        void OnReloadScript(InputAction.CallbackContext context);
        void OnOpenEditor(InputAction.CallbackContext context);
    }
}
