using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace SaveFiles
{
    public class SettingsSaveDataManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixerGroup;

        private void Start()
        {
            var settings = PlayerPrefsProxy.Load<SettingsSaveData>(SettingsSaveData.Key);

            audioMixerGroup.SetFloat("MasterVolume", settings.GameSettings.AudioSettings.Master);
            audioMixerGroup.SetFloat("MusicVolume", settings.GameSettings.AudioSettings.Music);
            audioMixerGroup.SetFloat("SFXVolume", settings.GameSettings.AudioSettings.Sfx);
        }

        public void SaveAudioChanges()
        {
            var settings = PlayerPrefsProxy.Load<SettingsSaveData>(SettingsSaveData.Key);

            audioMixerGroup.GetFloat("MasterVolume", out settings.GameSettings.AudioSettings.Master);
            audioMixerGroup.GetFloat("MusicVolume", out settings.GameSettings.AudioSettings.Music);
            audioMixerGroup.GetFloat("SFXVolume", out settings.GameSettings.AudioSettings.Sfx);

            PlayerPrefsProxy.Save(settings);
        }
    }
}
