using System;
using AgeRanger.Core.Contracts;
using AgeRanger.Core.Contracts.Config;
using AgeRanger.Core.Contracts.Logging;
using AgeRanger.Core.Contracts.Messages;
using System.Configuration;

namespace AgeRanger.Core.Config
{
    public class Config: IConfig, IDependency
    {
        private readonly ILogger _logger;
        private const string MissingConnectionString = "Connection String configuration entry '{0}' is missing";
        private const string MissingAppSetting= "AppSetting configuration entry '{0}' is missing";

        public Config(ILogger logger)
        {
            _logger = logger;
        }
        public string AgeRangerDb => GetConnectionString(ConfigKey.AgeRangerDb);
        
        private string GetConnectionString(ConfigKey key)
        {
            var errorMessage = string.Format(MissingConnectionString, key);
            try
            {
                return ConfigurationManager.ConnectionStrings[key.ToString()].ConnectionString;
            }
            catch (Exception ex)
            {
                _logger.Error(errorMessage + ex);
                throw new ConfigurationErrorsException(errorMessage);
            }
           
        }
        private string GetAppSettings(ConfigKey key)
        {
            var errorMessage = string.Format(MissingAppSetting, key.ToString());
            try
            {
                var value = ConfigurationManager.AppSettings[key.ToString()];
                if (value == null)
                {
                    throw new ConfigurationErrorsException(errorMessage);
                }
                return ConfigurationManager.AppSettings[key.ToString()];
            }
            catch(Exception ex)
            {
                throw new ConfigurationErrorsException(errorMessage, ex);
            }

        }
    }
}
