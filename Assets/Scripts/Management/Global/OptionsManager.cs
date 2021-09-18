using System.IO;
using System.Runtime.Serialization.Formatter.Binary;
using UnityEngine;

namespace FormulaManager.Management.Global
{
    public class OptionsManager
    {
        public bool Save(string saveName, object saveData)
        {
            BinaryFormatter formatter = GetBinaryFormatter();
            
            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");

            string path = Application.persistentDataPath + "/saves/" + saveName + ".save";
            FileStream file = File.Create(path);
            formatter.Serialize(file, saveData);
            file.Close();

            return true;
        }

        public object Load(string path)
        {
            if (!File.Exists(path))
                return null;
            
            BinaryFormatter formatter = GetBinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            try
            {
                object saveData = formatter.Deserialize(file);
                file.Close();
                return saveData;
            }
            catch
            {
                Debug.LogErrorFormat("Failed to load save file at {0}.", path);
                file.Close();
                return null;
            }
        }

        private BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter;
        }
    }
}