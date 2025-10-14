using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Misc;
using Newtonsoft.Json;
using UnityEngine;

namespace Data.PersistentData
{
    public static class PersistentDataManager
    {
        private static GameplayData _gameplayData;
        public static GameplayData GameplayData => _gameplayData;

        private static readonly string FilePath =
            $"{Application.persistentDataPath}/{Constants.PlayerDataFileName}.{Constants.SaveFileExtensionName}";

        public static async UniTask SaveDataToDisk()
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(_gameplayData, Formatting.Indented);
                Debug.LogError(jsonData);
                await File.WriteAllTextAsync(FilePath, jsonData);
                Debug.Log("GameplayData successfully saved and encrypted.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save GameplayData: {e.Message}");
            }
        }

        public static async UniTask LoadSaveDataFromDisk()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var jsonData = await File.ReadAllTextAsync(FilePath);
                    _gameplayData = JsonConvert.DeserializeObject<GameplayData>(jsonData) ?? new GameplayData();
                    Debug.Log("GameplayData successfully loaded.");
                }
                else
                {
                    _gameplayData = new GameplayData();
                    Debug.Log("No save file found. Initialized with default GameplayData.");
                    await SaveDataToDisk(); 
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load GameplayData: {e.Message}");
            }
        }
    }
}