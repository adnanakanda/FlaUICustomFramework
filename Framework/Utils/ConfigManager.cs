namespace Framework.Utils
{
    public static class ConfigManager
    {
        private static readonly JsonSettingsFile settings = new JsonSettingsFile("config.json");

        public static JsonSettingsFile GetSettings() => settings;
    }

}
