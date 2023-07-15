using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Match3
{
    public class SaveManager
    {
        private readonly string _filePath;

        public SaveManager()
        {
            _filePath = Application.dataPath + "/Config/Save/Save.json";
        }

        // json save
        public void SaveData(SaveData saveData)
        {
            string json = JsonUtility.ToJson(saveData);
            using (var writer = new StreamWriter(_filePath))
                writer.Write(json);
        }

        public SaveData GetData()
        {
            string json = "";
            if(File.Exists(_filePath))
            {
                using (var reader = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        json += line;
                    }
                }
            }
            if (string.IsNullOrEmpty(json))
            {
                return new SaveData();
            }
            return JsonUtility.FromJson<SaveData>(json);
        }

        // binary save
        public void SaveDataBinary(SaveData saveData)
        {
            using FileStream file = File.Create(_filePath);
            new BinaryFormatter().Serialize(file, saveData);
        }

        public SaveData GetDataBinary()
        {
            if (!File.Exists(_filePath))
            {
                return new SaveData();
            }

            SaveData saveData;
            using (FileStream file = File.Open(_filePath, FileMode.Open))
            {
                object LoadedData = new BinaryFormatter().Deserialize(file);
                saveData = (SaveData)LoadedData;
            }
            return saveData;
        }
    }
}
