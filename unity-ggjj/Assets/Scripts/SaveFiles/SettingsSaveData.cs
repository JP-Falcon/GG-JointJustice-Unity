namespace SaveFiles
{
    /// <summary>
    /// Passive data structure of all available settings of the game
    /// that are actually serialized and stored, so they can persist
    /// even when the game is closed
    /// </summary>
    public sealed class SettingsSaveData
    {
        public int Version;

        /// !!!!! IMPORTANT !!!!!
        /// Increase the value below by one and update <see cref="PlayerPrefsProxy.CreateUpgradedSaveData"/>
        /// to ensure an upgrade path from the previous version to the new version exists,
        /// if you make ANY changes to this class
        /// !!!!! IMPORTANT !!!!!
        public static readonly int LatestVersion = 1;

        public sealed class Settings
        {
            public bool FullScreen;
            public AudioSettings AudioSettings;
            public ControlsSettings ControlsSettings;
        }

        public sealed class AudioSettings
        {
            public float Master;
            public float Music;
            public float SFX;
        }

        public sealed class ControlsSettings
        {
            public string Back;
            public string Select;
            public string PressWitness;
        }

        public Settings GameSettings;

        /// <summary>
        /// Creates SaveData with default settings
        /// </summary>
        /// <param name="version">Version of this SaveData instance</param>
        public SettingsSaveData(int version)
        {
            Version = version;
            GameSettings = new Settings()
            {
                FullScreen = false,
                AudioSettings = new AudioSettings(),
                ControlsSettings = new ControlsSettings()
            };
        }
    }
}