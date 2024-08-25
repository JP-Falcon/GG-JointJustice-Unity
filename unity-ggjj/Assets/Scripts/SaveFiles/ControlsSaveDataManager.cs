using UI;
using UnityEngine;

namespace SaveFiles
{
    [RequireComponent(typeof(SettingsSaveDataLoader))]
    public class ControlsSaveDataManager : MonoBehaviour
    {
        [SerializeField] private bool _shouldLoadOnStart = true;
        [SerializeField] private ControlButton _evidenceMenuControlButton;
        [SerializeField] private ControlButton _selectControlButton;
        [SerializeField] private ControlButton _pressWitnessControlButton;

        private SettingsSaveData _settingsSaveData;
    
        private void Start()
        {
            _settingsSaveData = GetComponent<SettingsSaveDataLoader>().SettingsSaveData;

            if (!_shouldLoadOnStart)
            {
                return;
            }

            if (_settingsSaveData.GameControlsSettings.EvidenceMenu != null)
            {
                _evidenceMenuControlButton.OverridePath = _settingsSaveData.GameControlsSettings.EvidenceMenu;
            }

            if (_settingsSaveData.GameControlsSettings.Select != null)
            {
                _selectControlButton.OverridePath = _settingsSaveData.GameControlsSettings.Select;
            }

            if (_settingsSaveData.GameControlsSettings.PressWitness != null)
            {
                _pressWitnessControlButton.OverridePath = _settingsSaveData.GameControlsSettings.PressWitness;
            }
        }

        public void SaveChanges()
        {
            _settingsSaveData.GameControlsSettings.EvidenceMenu = _evidenceMenuControlButton.OverridePath;
            _settingsSaveData.GameControlsSettings.Select = _selectControlButton.OverridePath;
            _settingsSaveData.GameControlsSettings.PressWitness = _pressWitnessControlButton.OverridePath;
            
            PlayerPrefsProxy.Save(_settingsSaveData);
        }
    }
}
