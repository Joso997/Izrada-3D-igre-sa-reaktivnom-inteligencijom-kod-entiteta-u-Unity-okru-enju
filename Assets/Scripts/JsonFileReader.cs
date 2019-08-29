using UnityEngine;
using System.IO;

    public class JsonFileReader
    {
        public static string LoadJsonAsResource(string path)
        {
            string loadedJsonfile = File.ReadAllText(Application.dataPath + path);
            return loadedJsonfile;
        }
        public static void ToJsonAsResource(string path, string json)
        {
            File.WriteAllText(Application.dataPath + path, json);
        }
        public static void ClearResource(string path)
        {
            File.Create(Application.dataPath + path).Close();
        }
}

