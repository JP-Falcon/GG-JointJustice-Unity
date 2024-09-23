using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Editor.Ink
{
    class EditorCoroutine
    {
        private readonly IEnumerator _routine;

        private EditorCoroutine(IEnumerator routine)
        {
            _routine = routine;
        }

        public static EditorCoroutine Start(IEnumerator routine)
        {
            var coroutine = new EditorCoroutine(routine);
            coroutine.Start();
            return coroutine;
        }

        private void Start()
        {
            UnityEditor.EditorApplication.update += Update;
        }

        private void Stop()
        {
            UnityEditor.EditorApplication.update -= Update;
        }

        private void Update()
        {
            if (!_routine.MoveNext())
            {
                Stop();
            }
        }
    }
    
    public static class BuildScript
    {
        private static DateTimeOffset _startedAt;
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);

        public static void Build()
        {
            AssetDatabase.ImportAsset("Assets/Resources/InkDialogueScripts/CaseDemo/2-1-FullScene.ink");
            AssetDatabase.SaveAssets();
            EditorCoroutine.Start(CheckForTimeoutOrJson());
        }

        private static IEnumerator CheckForTimeoutOrJson()
        {
            var success = false;
            _startedAt = DateTimeOffset.Now;
            while ((DateTimeOffset.Now - _startedAt).TotalSeconds < Timeout.TotalSeconds)
            {
                yield return null;
                System.IO.FileInfo file = new System.IO.FileInfo("Assets/Resources/InkDialogueScripts/CaseDemo/2-1-FullScene.json");
                if (!file.Exists)
                {
                    continue;
                }
                success = true;
                break;
            }
            EditorApplication.Exit(success ? 0 : 1);
        }
    }
}