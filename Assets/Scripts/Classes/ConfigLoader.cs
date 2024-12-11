using UnityEngine;
using System.IO;

public class ConfigLoader
{
    public string ApiUrl { get; private set; }

    public ConfigLoader()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "config.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Config config = JsonUtility.FromJson<Config>(json);
            ApiUrl = config.apiUrl;
            Debug.Log("API URL: " + ApiUrl);
        }
        else
        {
            Debug.LogError("Config file not found: " + path);
        }
    }

    [System.Serializable]
    private class Config
    {
        public string apiUrl;
    }
}
