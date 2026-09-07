using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace FNaF_XNA.Utils
{
    /// <summary>
    /// Manages game saves and load operations.
    /// </summary>
    public class SaveManager
    {
        private string savePath;

        public class GameSave
        {
            public int Night { get; set; }
            public int Score { get; set; }
            public float PlayTime { get; set; }
            public DateTime SaveDate { get; set; }
            public Dictionary<string, object> GameState { get; set; }
        }

        public SaveManager(string path = "Data/Saves/")
        {
            savePath = path;
            Directory.CreateDirectory(savePath);
        }

        public void SaveGame(int saveSlot, GameSave gameSave)
        {
            try
            {
                gameSave.SaveDate = DateTime.Now;
                string filename = Path.Combine(savePath, $"save{saveSlot}.json");
                string json = JsonSerializer.Serialize(gameSave, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filename, json);
                Logger.LogSuccess($"Game saved to slot {saveSlot}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to save game: {ex.Message}");
            }
        }

        public GameSave LoadGame(int saveSlot)
        {
            try
            {
                string filename = Path.Combine(savePath, $"save{saveSlot}.json");
                if (File.Exists(filename))
                {
                    string json = File.ReadAllText(filename);
                    GameSave gameSave = JsonSerializer.Deserialize<GameSave>(json);
                    Logger.LogSuccess($"Game loaded from slot {saveSlot}");
                    return gameSave;
                }
                else
                {
                    Logger.LogWarning($"Save file slot {saveSlot} not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to load game: {ex.Message}");
                return null;
            }
        }

        public bool DeleteSave(int saveSlot)
        {
            try
            {
                string filename = Path.Combine(savePath, $"save{saveSlot}.json");
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                    Logger.LogSuccess($"Save slot {saveSlot} deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to delete save: {ex.Message}");
                return false;
            }
        }

        public bool SaveExists(int saveSlot)
        {
            string filename = Path.Combine(savePath, $"save{saveSlot}.json");
            return File.Exists(filename);
        }
    }
}
