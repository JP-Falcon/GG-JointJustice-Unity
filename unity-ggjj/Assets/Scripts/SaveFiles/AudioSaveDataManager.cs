using UnityEngine;
using UnityEngine.Audio;

namespace SaveFiles
{
    [RequireComponent(typeof(SettingsSaveDataLoader))]
    public class AudioSaveDataManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioGroup;

        private SettingsSaveData _settingsSaveData;
        
        private void Start()
        {
            _settingsSaveData = GetComponent<SettingsSaveDataLoader>().SettingsSaveData;
            
            _audioGroup.SetFloat("MasterVolume", _settingsSaveData.GameSettings.AudioSettings.Master);
            _audioGroup.SetFloat("MusicVolume", _settingsSaveData.GameSettings.AudioSettings.Music);
            _audioGroup.SetFloat("SFXVolume", _settingsSaveData.GameSettings.AudioSettings.Sfx);
        }

        public void SaveAudioChanges()
        {
            _audioGroup.GetFloat("MasterVolume", out _settingsSaveData.GameSettings.AudioSettings.Master);
            _audioGroup.GetFloat("MusicVolume", out _settingsSaveData.GameSettings.AudioSettings.Music);
            _audioGroup.GetFloat("SFXVolume", out _settingsSaveData.GameSettings.AudioSettings.Sfx);

            PlayerPrefsProxy.Save(_settingsSaveData);
        }
    }
}
