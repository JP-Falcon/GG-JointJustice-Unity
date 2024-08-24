namespace SaveFiles
{
    /// <summary>
    /// Passive data structure of all available settings of the game
    /// that are actually serialized and stored, so they can persist
    /// even when the game is closed
    /// </summary>
    public sealed class SettingsSaveData : ISaveData
    {
        public int Version = LatestVersion;
        
        /// !!!!! IMPORTANT !!!!!
        /// Increase the value below by one and update <see cref="PlayerPrefsProxy.CreateUpgradedSaveData"/>
        /// to ensure an upgrade path from the previous version to the new version exists,
        /// if you make ANY changes to this class
        /// !!!!! IMPORTANT !!!!!
        public static readonly int LatestVersion = 1;

        int ISaveData.LatestVersion => LatestVersion;
        
        int ISaveData.Version
        {
            get => Version;
            set => Version = value;
        }

        public static string Key => "SettingsSaveData";

        string ISaveData.Key => Key;

        public sealed class Settings
        {
            public bool FullScreen;
            public AudioSettings AudioSettings = new();
            public ControlsSettings ControlsSettings = new();
        }

        public sealed class AudioSettings
        {
            public float Master = 1;
            public float Music = 1;
            public float Sfx = 1;
        }

        public sealed class ControlsSettings
        {
            public string Back;
            public string Select;
            public string PressWitness;
        }

        public Settings GameSettings = new();
    }
}