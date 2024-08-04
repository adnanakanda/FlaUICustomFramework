namespace Framework.FileUtils
{
    public static class ConfigManager
    {
        private static readonly JsonSettingsFile settings;

        static ConfigManager()
        {
            settings = new JsonSettingsFile("config.json");
        }

        public static JsonSettingsFile GetSettings()
        {
            return settings;
        }
    }
}
