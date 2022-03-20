using System;
using System.Collections.Generic;
using System.Linq;
using Ink;
using Ink.Parsed;
using Ink.Runtime;
using Ink.UnityIntegration;
using UnityEditor;
using UnityEngine;
using Story = Ink.Runtime.Story;

namespace Editor.Ink
{
    public class Validation : AssetPostprocessor
    {
        /// <summary>
        /// Called when an Ink file is compiled after editing it
        /// </summary>
        private static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
            foreach (var importedAssetPath in importedAssets)
            {
                if (!InkEditorUtils.IsInkFile(importedAssetPath))
                {
                    continue;
                }
                var inkFile = InkLibrary.GetInkFileWithPath(importedAssetPath);
                ValidateNarrativeScript(inkFile);
            }
        }

        /// <summary>
        /// Menu item to allow validation of all Ink files in the project
        /// </summary>
        [UnityEditor.MenuItem("Assets/Validate Narrative Scripts", false, 202)]
        private static void ValidateAllNarrativeScripts()
        {
            InkLibrary.GetMasterInkFiles().ToList().ForEach(ValidateNarrativeScript);
        }

        /// <summary>
        /// Reads an Ink file and attempts to parse every action.
        /// Logs any errors found to the console
        /// </summary>
        /// <param name="inkFile">The Ink file to read in JSON format</param>
        private static void ValidateNarrativeScript(InkFile inkFile)
        {
            var lines = new List<string>();
            var story = new Story(inkFile.jsonAsset.text);
            NarrativeScript.ReadContent(story.mainContentContainer.content, lines);
            
            for (var i = 0; i < lines.Count; i++)
            {
                if (lines[i] == string.Empty || lines[i][0] != ActionDecoderBase.ACTION_TOKEN)
                {
                    continue;
                }
                
                try
                {
                    ActionDecoderBase.GenerateInvocationDetails(lines[i], typeof(ActionDecoder));
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Error on line {i + 1} of {inkFile}: {exception.Message}");
                }
            }
        }
    }
}
