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
            
            if (Mathf.Approximately(_settingsSaveData.GameAudioSettings.Master, -1))
            {
                _audioGroup.GetFloat("MasterVolume", out _settingsSaveData.GameAudioSettings.Master);
            }
            else
            {
                _audioGroup.SetFloat("MasterVolume", _settingsSaveData.GameAudioSettings.Master);                   
            }
            
            if (Mathf.Approximately(_settingsSaveData.GameAudioSettings.Music, -1))
            {
                _audioGroup.GetFloat("MusicVolume", out _settingsSaveData.GameAudioSettings.Music);
            }
            else
            {
                _audioGroup.SetFloat("MusicVolume", _settingsSaveData.GameAudioSettings.Music);                   
            }
            
            if (Mathf.Approximately(_settingsSaveData.GameAudioSettings.Sfx, -1))
            {
                _audioGroup.GetFloat("SFXVolume", out _settingsSaveData.GameAudioSettings.Sfx);
            }
            else
            {
                _audioGroup.SetFloat("SFXVolume", _settingsSaveData.GameAudioSettings.Sfx);                   
            }
            
            if (Mathf.Approximately(_settingsSaveData.GameAudioSettings.Dialogue, -1))
            {
                _audioGroup.GetFloat("DialogueVolume", out _settingsSaveData.GameAudioSettings.Dialogue);
            }
            else
            {
                _audioGroup.SetFloat("DialogueVolume", _settingsSaveData.GameAudioSettings.Dialogue);                   
            }
        }

        public void SaveAudioChanges()
        {
            _audioGroup.GetFloat("MasterVolume", out _settingsSaveData.GameAudioSettings.Master);
            _audioGroup.GetFloat("MusicVolume", out _settingsSaveData.GameAudioSettings.Music);
            _audioGroup.GetFloat("SFXVolume", out _settingsSaveData.GameAudioSettings.Sfx);
            _audioGroup.GetFloat("DialogueVolume", out _settingsSaveData.GameAudioSettings.Dialogue);

            PlayerPrefsProxy.Save(_settingsSaveData);
        }
    }
}
