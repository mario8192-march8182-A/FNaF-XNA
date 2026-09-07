using System;
using System.Collections.Generic;

namespace FNaF_XNA.Utils
{
    /// <summary>
    /// Utility class for debugging and logging game events.
    /// </summary>
    public static class Logger
    {
        private static List<string> logs = new List<string>();
        private static bool debugMode = true;

        public static void Log(string message)
        {
            if (debugMode)
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                string logMessage = $"[{timestamp}] {message}";
                logs.Add(logMessage);
                System.Diagnostics.Debug.WriteLine(logMessage);
            }
        }

        public static void LogWarning(string message)
        {
            Log($"[WARNING] {message}");
        }

        public static void LogError(string message)
        {
            Log($"[ERROR] {message}");
        }

        public static void LogSuccess(string message)
        {
            Log($"[SUCCESS] {message}");
        }

        public static List<string> GetLogs()
        {
            return new List<string>(logs);
        }

        public static void ClearLogs()
        {
            logs.Clear();
        }
    }
}
