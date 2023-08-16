using System;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

namespace Save
{
    public static class SaveSystem
    {
        public static void CreatePath(LevelManager levelManager)
        {
            if (!File.Exists(Application.persistentDataPath + "/data.b"))
            {
                LevelsData levelsData = new LevelsData(levelManager) {IsFirstStart = true};
                SaveData(levelManager);
                Debug.Log($"Create datafile! \nIsFirstStart: {levelsData.IsFirstStart}");
            }

            Debug.Log("DataFile has already create!");
            LoadData();
        }
        
        public static void SaveData(LevelManager levelManager)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/data.b";
            FileStream stream = new FileStream(path, FileMode.Create);
            LevelsData levelsData = new LevelsData(levelManager);
            
            binaryFormatter.Serialize(stream, levelsData);
            
            Debug.Log($"Saved path: {path}");
            
            stream.Close();
        }
        
        private static LevelsData LoadData()
        {
            string path = Application.persistentDataPath + "/data.b";
            
            if (File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                LevelsData levelsData = binaryFormatter.Deserialize(stream) as LevelsData;

                if (levelsData != null)
                    Debug.Log($"Load path: {path} \nNumbers saved levels {levelsData.NumberSaveLevel}");
                
                stream.Close();

                return levelsData;
            }
            
            throw new Exception($"Exception! File does not exist! File does not found in {path}");
        }
    }
}