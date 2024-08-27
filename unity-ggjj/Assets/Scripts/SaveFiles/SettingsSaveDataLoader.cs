using UnityEngine;

namespace SaveFiles
{
    public class SettingsSaveDataLoader : MonoBehaviour
    {
        public SettingsSaveData SettingsSaveData { get; private set; }
    
        private void Awake()
        {
            SettingsSaveData = PlayerPrefsProxy.Load<SettingsSaveData>(SettingsSaveData.Key);
        }
    }
}
