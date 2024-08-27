using SaveFiles;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts.SaveFiles
{
    public class ClearSaveDataButtons : MonoBehaviour
    {
        [UnityEditor.MenuItem("Edit/Delete Save Data %#d")]
        static void DeleteSaveData()
        {
            if (!EditorUtility.DisplayDialog("Delete Save Data", "Are you sure you want to delete your Save Data?\r\n\r\nYou will lose ALL progress in the game!", "Delete", "Cancel"))
            {
                return;
            }

            Debug.LogWarning("Save Data has been successfully deleted!");
            PlayerPrefsProxy.DeleteSaveData(SaveData.Key);
        }
    
        [UnityEditor.MenuItem("Edit/Delete Settings Save Data")]
        static void DeleteSettingsSaveData()
        {
            if (!EditorUtility.DisplayDialog("Delete Settings Save Data", "Are you sure you want to delete your saved settings?","Delete", "Cancel"))
            {
                return;
            }

            Debug.LogWarning("Settings Save Data has been successfully deleted!");
            PlayerPrefsProxy.DeleteSaveData(SettingsSaveData.Key);
        }
    }
}