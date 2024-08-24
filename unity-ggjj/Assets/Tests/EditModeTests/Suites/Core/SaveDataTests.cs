using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SaveFiles;
using UnityEngine;
using static System.Int32;

namespace Tests.EditModeTests.Suites
{
    /// <remarks>
    /// A new save file is created for each individual test. See: <see cref="EnsureDefaultSaveFileExists"/>
    /// </remarks>
    public class SaveDataTests
    {
        private const string PLAYER_PREFS_KEY = "SaveData";
        private SaveData _previouslyStoredSaveData;

        [SetUp]
        public void EnsureDefaultSaveFileExists()
        {
            // Force creation of a new default SaveData object
            PlayerPrefsProxy.DeleteSaveData(SaveData.Key);
            var saveData = new SaveData(SaveData.LatestVersion);
            PlayerPrefsProxy.Save(saveData);
        }

        [OneTimeTearDown]
        public void RestorePreviouslyExistingSaveFile()
        {
            PlayerPrefsProxy.DeleteSaveData(SaveData.Key);
            if (_previouslyStoredSaveData != null)
            {
                PlayerPrefsProxy.Save(_previouslyStoredSaveData);
            }
        }

        [OneTimeSetUp]
        public void BackupPreviouslyExistingSaveFile()
        {
            if (!PlayerPrefsProxy.HasExistingSaveData(SaveData.Key))
            {
                return;
            }
            _previouslyStoredSaveData = PlayerPrefsProxy.Load<SaveData>(SaveData.Key);
        }

        [Test]
        public void LoadReturnsUpdatedSaveData()
        {
            var saveData = PlayerPrefsProxy.Load<SaveData>(SaveData.Key);
            Assert.IsFalse(saveData.GameProgression.UnlockedChapters.HasFlag(SaveData.Progression.Chapters.Chapter2));
            Assert.IsFalse(saveData.GameProgression.UnlockedChapters.HasFlag(SaveData.Progression.Chapters.BonusChapter1));

            saveData.GameProgression.UnlockedChapters.AddChapter(SaveData.Progression.Chapters.Chapter2);
            PlayerPrefsProxy.Save(saveData);

            saveData = PlayerPrefsProxy.Load<SaveData>(SaveData.Key);

            Assert.IsTrue(saveData.GameProgression.UnlockedChapters.HasFlag(SaveData.Progression.Chapters.Chapter2));

            saveData.GameProgression.UnlockedChapters.AddChapter(SaveData.Progression.Chapters.BonusChapter1);
            PlayerPrefsProxy.Save(saveData);

            saveData = PlayerPrefsProxy.Load<SaveData>(SaveData.Key);

            Assert.IsTrue(saveData.GameProgression.UnlockedChapters.HasFlag(SaveData.Progression.Chapters.Chapter2));

            saveData.GameProgression.UnlockedChapters.AddChapter(SaveData.Progression.Chapters.Chapter3);
            PlayerPrefsProxy.Save(saveData);

            saveData = PlayerPrefsProxy.Load<SaveData>(SaveData.Key);

            Assert.IsTrue(saveData.GameProgression.UnlockedChapters.HasFlag(SaveData.Progression.Chapters.Chapter3));
        }

        [Test]
        public void LoadingOutdatedSaveDataGetsUpgraded()
        {
            // deliberately overwrite the current SaveData version to 0
            var currentInternalSaveData = PlayerPrefs.GetString(PLAYER_PREFS_KEY);
            var saveDataAtVersionZero = new Regex("\"Version\":\\d+").Replace(currentInternalSaveData, "version:0");
            StringAssert.AreNotEqualIgnoringCase(currentInternalSaveData, saveDataAtVersionZero);
            PlayerPrefs.SetString(PLAYER_PREFS_KEY, saveDataAtVersionZero);
            PlayerPrefs.Save();
            StringAssert.AreEqualIgnoringCase(saveDataAtVersionZero, PlayerPrefs.GetString(PLAYER_PREFS_KEY, saveDataAtVersionZero));

            // loading is successful and SaveData is set to the current version
            var saveData = PlayerPrefsProxy.Load<SaveData>(SaveData.Key);
            Assert.AreEqual(saveData.Version, SaveData.LatestVersion);
        }

        [Test]
        public void ThrowsIfSaveGameIsTooNew()
        {
            const int absurdlyHighVersionNumber = MaxValue - 1;
            // deliberately overwrite the current SaveData version to be close to Int32 maximum
            var currentInternalSaveData = PlayerPrefs.GetString(PLAYER_PREFS_KEY);
            var saveDataAtMaxBounds = new Regex("\"Version\":\\d+").Replace(currentInternalSaveData, $"version:{absurdlyHighVersionNumber}");
            StringAssert.AreNotEqualIgnoringCase(currentInternalSaveData, saveDataAtMaxBounds);
            PlayerPrefs.SetString(PLAYER_PREFS_KEY, saveDataAtMaxBounds);
            PlayerPrefs.Save();
            StringAssert.AreEqualIgnoringCase(saveDataAtMaxBounds, PlayerPrefs.GetString(PLAYER_PREFS_KEY, saveDataAtMaxBounds));

            var exception = Assert.Throws<NotSupportedException>(() => {
                var _ = PlayerPrefsProxy.Load<SaveData>(SaveData.Key);
            });
            StringAssert.Contains($"'{absurdlyHighVersionNumber}'", exception.Message);
            StringAssert.Contains($"'{SaveData.LatestVersion}'", exception.Message);
        }

        [Test]
        public void ThrowsIfAttemptingToLoadWithoutSaveDataPresent()
        {
            PlayerPrefsProxy.DeleteSaveData(SaveData.Key);

            Assert.Throws<KeyNotFoundException>(() => {
                var _ = PlayerPrefsProxy.Load<SaveData>(SaveData.Key);
            });
        }
    }
}