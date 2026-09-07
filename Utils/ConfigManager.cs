using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace FNaF_XNA.Utils
{
    /// <summary>
    /// Manages game configuration and settings.
    /// </summary>
    public class ConfigManager
    {
        private Dictionary<string, object> settings;
        private string configPath;

        public ConfigManager(string path = "Data/Config/settings.json")
        {
            configPath = path;
            settings = new Dictionary<string, object>();
            LoadConfig();
        }

        public void LoadConfig()
        {
            try
            {
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    settings = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                    Logger.LogSuccess("Configuration loaded successfully");
                }
                else
                {
                    CreateDefaultConfig();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to load config: {ex.Message}");
                CreateDefaultConfig();
            }
        }

        private void CreateDefaultConfig()
        {
            settings.Clear();
            settings["MasterVolume"] = 0.8f;
            settings["MusicVolume"] = 0.6f;
            settings["SFXVolume"] = 0.8f;
            settings["Difficulty"] = "Normal";
            settings["FullScreen"] = false;
            settings["ResolutionWidth"] = 1280;
            settings["ResolutionHeight"] = 720;
            SaveConfig();
        }

        public void SaveConfig()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(configPath));
                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, json);
                Logger.LogSuccess("Configuration saved successfully");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to save config: {ex.Message}");
            }
        }

        public void Set<T>(string key, T value)
        {
            settings[key] = value;
        }

        public T Get<T>(string key, T defaultValue = default)
        {
            try
            {
                if (settings.ContainsKey(key))
                {
                    return (T)Convert.ChangeType(settings[key], typeof(T));
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Failed to get config value {key}: {ex.Message}");
            }
            return defaultValue;
        }
    }
}
