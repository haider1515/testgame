using System.IO;
using UnityEngine;

namespace Game.Config
{
    public static class ConfigLoader
    {
        public static ConfigData LoadConfig(string filename = "config.json")
        {
            string path = Path.Combine(Application.streamingAssetsPath, filename);

            if (!File.Exists(path))
            {
                Debug.LogError($"Config file not found at: {path}");
                return new ConfigData { rows = 4, columns = 4 };
            }

            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<ConfigData>(json);
        }
    }
}
