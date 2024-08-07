using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Framework.Utils
{
    public class JsonSettingsFile
    {
        private readonly JObject _settings;

        public JsonSettingsFile(string fileName)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                Logger.Info($"Attempting to load settings file from path: {filePath}");

                if (!File.Exists(filePath))
                {
                    Logger.Error($"Settings file not found at path: {filePath}");
                    throw new FileNotFoundException($"Settings file not found: {filePath}");
                }

                var jsonContent = File.ReadAllText(filePath);
                Logger.Info($"Settings file content loaded successfully from path: {filePath}");
                _settings = JObject.Parse(jsonContent);
                Logger.Info($"Settings file deserialized successfully.");
                Logger.Info($"Settings content: {_settings.ToString()}");
            }
            catch (FileNotFoundException ex)
            {
                Logger.Error("Failed to find the settings file.", ex);
                throw;
            }
            catch (JsonException ex)
            {
                Logger.Error("Failed to deserialize the settings file.", ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error("An unexpected error occurred while loading the settings file.", ex);
                throw;
            }
        }

        public T GetValue<T>(string key)
        {
            try
            {
                Logger.Info($"Retrieving value for key: {key}");
                JToken token = _settings.GetValue(key, StringComparison.OrdinalIgnoreCase);
                if (token == null)
                {
                    Logger.Error($"Key '{key}' not found in settings.");
                    throw new KeyNotFoundException($"Key '{key}' not found in settings.");
                }
                Logger.Info($"Value for key '{key}' retrieved successfully.");
                return token.ToObject<T>();
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to retrieve value for key: {key}", ex);
                throw;
            }
        }
    }
}
