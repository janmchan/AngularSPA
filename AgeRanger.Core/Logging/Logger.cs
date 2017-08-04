using AgeRanger.Core.Contracts;
using AgeRanger.Core.Contracts.Logging;

namespace AgeRanger.Core.Logging
{
    public class Logger : ILogger, ISingletonDependency
    {
        private static readonly log4net.ILog LoggerInstance =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public void Error(string message)
        {
            LoggerInstance.Error(message);
        }

        public void Info(string message)
        {
            LoggerInstance.Info(message);
        }
    
        public void Warning(string message)
        {
            LoggerInstance.Warn(message);
        }
    }
}
