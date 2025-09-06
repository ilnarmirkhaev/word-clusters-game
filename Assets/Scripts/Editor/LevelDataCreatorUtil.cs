using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Serialization;
using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class LevelDataCreatorUtil : EditorWindow
    {
        private string _word = "";
        private string _lengthsText = "";

        private readonly List<Word> _createdWords = new();
        private readonly JsonSerializer _serializer = new();

        [MenuItem("Utils/Create LevelData")]
        private static void Init()
        {
            GetWindow<LevelDataCreatorUtil>(true, "Create LevelData", true);
        }

        private void OnGUI()
        {
            _word = EditorGUILayout.TextField("Word", _word);
            _lengthsText = EditorGUILayout.TextField("Lengths (2 2 3)", _lengthsText);

            if (GUILayout.Button("Add word"))
            {
                TryCreateWord();
            }

            for (var i = 0; i < _createdWords.Count; i++)
            {
                var word = _createdWords[i];
                DrawWord(word);
            }

            if (_createdWords.Count == 4 && GUILayout.Button("Save"))
            {
                var levelData = new LevelData(_createdWords);
                var json = _serializer.Serialize(levelData);
                SaveWordsToFile(json);
            }
        }

        private void TryCreateWord()
        {
            try
            {
                var lengths = _lengthsText.Split(" ").Select(int.Parse).ToArray();
                var word = new Word(_word, lengths);
                _createdWords.Add(word);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void DrawWord(Word word)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField($"{word} - {string.Join(',', word.Clusters)}");
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                _createdWords.Remove(word);
            }

            EditorGUILayout.EndHorizontal();
        }

        private static void SaveWordsToFile(string json)
        {
            var path = EditorUtility.SaveFilePanelInProject("Save level data", "", "json", "wtf");
            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, json);
                AssetDatabase.Refresh();
            }
        }
    }
}