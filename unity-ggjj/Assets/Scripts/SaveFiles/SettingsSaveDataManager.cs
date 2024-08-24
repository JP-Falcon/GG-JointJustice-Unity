using UnityEngine;
using UnityEngine.Audio;

namespace SaveFiles
{
    public class SettingsSaveDataManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioGroup;

        private void Start()
        {
            var settings = PlayerPrefsProxy.Load<SettingsSaveData>(SettingsSaveData.Key);

            _audioGroup.SetFloat("MasterVolume", settings.GameSettings.AudioSettings.Master);
            _audioGroup.SetFloat("MusicVolume", settings.GameSettings.AudioSettings.Music);
            _audioGroup.SetFloat("SFXVolume", settings.GameSettings.AudioSettings.Sfx);
        }

        public void SaveAudioChanges()
        {
            var settings = PlayerPrefsProxy.Load<SettingsSaveData>(SettingsSaveData.Key);

            _audioGroup.GetFloat("MasterVolume", out settings.GameSettings.AudioSettings.Master);
            _audioGroup.GetFloat("MusicVolume", out settings.GameSettings.AudioSettings.Music);
            _audioGroup.GetFloat("SFXVolume", out settings.GameSettings.AudioSettings.Sfx);

            PlayerPrefsProxy.Save(settings);
        }
    }
}
