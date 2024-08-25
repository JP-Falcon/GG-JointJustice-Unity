using UI;
using UnityEngine;

namespace SaveFiles
{
    [RequireComponent(typeof(SettingsSaveDataLoader))]
    public class ControlsSaveDataManager : MonoBehaviour
    {
        [SerializeField] private ControlButton _evidenceMenuControlButton;
        [SerializeField] private ControlButton _selectControlButton;
        [SerializeField] private ControlButton _pressWitnessControlButton;

        private SettingsSaveData _settingsSaveData;
    
        void Start()
        {
            _settingsSaveData = GetComponent<SettingsSaveDataLoader>().SettingsSaveData;
        }
    }
}
