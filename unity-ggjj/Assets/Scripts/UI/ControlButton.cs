using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class ControlButton : MonoBehaviour
    {
        [SerializeField] private InputActionReference _inputActionReference;
        [SerializeField] private InputActionReference[] _alternativeInputActionReferences;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private UnityEvent _onButtonRebind;
        [SerializeField] private Color _rebindColor;

        private readonly List<InputActionRebindingExtensions.RebindingOperation> _rebindingOperations = new();
        private Menu _menu;
        private Image _image;
        private Color _originalColor;
        private ControlsButtonContainer _controlsButtonContainer;

        public string OverridePath
        {
            get => _inputActionReference.action.bindings.First().overridePath;
            set
            {
                _inputActionReference.action.RemoveAllBindingOverrides();
                _inputActionReference.action.ApplyBindingOverride(value);
            }
        }

        private void Awake()
        {
            UpdateButton();
            _menu = GetComponentInParent<Menu>();
            _image = GetComponent<Image>();
            _originalColor = _image.color;
            _controlsButtonContainer = GetComponentInParent<ControlsButtonContainer>();
        }

        public void BeginRebind()
        {
            _image.color = _rebindColor;
            _controlsButtonContainer.DeselectControlButtons(this);
            var inputActionReferences = new List<InputActionReference>(_alternativeInputActionReferences)
            {
                _inputActionReference
            };
            foreach (var inputActionReference in inputActionReferences)
            {
                inputActionReference.action.Disable();
                _rebindingOperations.Add(inputActionReference.action.PerformInteractiveRebinding(0)
                    .WithControlsExcluding("<Mouse>")
                    .OnPotentialMatch(OnPotentialMatch)
                    .OnComplete(rebindingOperation => OnRebindComplete(rebindingOperation, inputActionReference))
                    .OnCancel(rebindingOperation => OnRebindComplete(rebindingOperation, inputActionReference))
                    .OnMatchWaitForAnother(0.1f)
                    .Start());
            }
        }

        public void CancelRebind()
        {
            for (var i = _rebindingOperations.Count - 1; i >= 0; i--)
            {
                _rebindingOperations[i].Cancel();
            }
        }

        private void OnRebindComplete(InputActionRebindingExtensions.RebindingOperation rebindingOperation, InputActionReference inputActionReference)
        {
            _image.color = _originalColor;
            inputActionReference.action.Enable();
            rebindingOperation.Dispose();
            _rebindingOperations.Remove(rebindingOperation);
            if (inputActionReference == _inputActionReference)
            {
                _onButtonRebind.Invoke();
                _menu.OnSetInteractable.Invoke(true);
                UpdateButton();
            }
        }

        private void OnPotentialMatch(InputActionRebindingExtensions.RebindingOperation rebindingOperation)
        {
            foreach (var action in _inputActionReference.asset.actionMaps[0].actions)
            {
                foreach (var control in action.controls)
                {
                    if (control == rebindingOperation.selectedControl)
                    {
                        CancelRebind();
                    }
                }
            }
        }

        private void UpdateButton()
        {
            _text.text = _inputActionReference.action.GetBindingDisplayString().Split('|')[0].Trim();
        }
    }
}
