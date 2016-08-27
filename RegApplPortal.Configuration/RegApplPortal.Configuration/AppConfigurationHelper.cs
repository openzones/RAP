using System.Configuration;

namespace RegApplPortal.Configuration
{
    public class AppConfigurationHelper
    {
        public static string GetConfiguration(System.Configuration.Configuration instance, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ConfigurationErrorsException("Configuration key is null or empty.");
            }

            string result = null;
            KeyValueConfigurationElement element = instance.AppSettings.Settings[key];
            if (element != null)
            {
                result = element.Value;
            }

            if (string.IsNullOrEmpty(result))
            {
                throw new ConfigurationErrorsException(string.Format("Configuration value {0} is null or empty.", key));
            }

            return result;
        }

        public static string GetConfiguration(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ConfigurationErrorsException("Configuration key is null or empty.");
            }

            string result = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(result))
            {
                throw new ConfigurationErrorsException(string.Format("Configuration value {0} is null or empty.", key));
            }

            return result;
        }
    }
}
