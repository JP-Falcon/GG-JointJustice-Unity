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
            
            _audioGroup.SetFloat("MasterVolume", _settingsSaveData.GameAudioSettings.Master);
            _audioGroup.SetFloat("MusicVolume", _settingsSaveData.GameAudioSettings.Music);
            _audioGroup.SetFloat("SFXVolume", _settingsSaveData.GameAudioSettings.Sfx);
        }

        public void SaveAudioChanges()
        {
            _audioGroup.GetFloat("MasterVolume", out _settingsSaveData.GameAudioSettings.Master);
            _audioGroup.GetFloat("MusicVolume", out _settingsSaveData.GameAudioSettings.Music);
            _audioGroup.GetFloat("SFXVolume", out _settingsSaveData.GameAudioSettings.Sfx);

            PlayerPrefsProxy.Save(_settingsSaveData);
        }
    }
}
