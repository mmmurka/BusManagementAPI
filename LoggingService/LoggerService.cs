using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoggerService;
public class LoggerService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public void LogInfo(string message) => Logger.Info(message);
    public void LogError(string message) => Logger.Error(message);
    public void LogDebug(string message) => Logger.Debug(message);
}