using LoggerLayer.DTO;
using LoggerLayer.Interfaces;
using System.Collections.Generic;
using WpfApp1.ViewModel.Base;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// Модель представления для вывода логов
    /// </summary>
    public class LogsVM : BaseViewModel
    {
        private ILoggerService _loggerService;

        public LogsVM(ILoggerService logger)
        {
            _loggerService = logger;
        }

        /// <summary>
        /// Список логов
        /// </summary>
        public IEnumerable<LogVM> Logs 
        {
            get 
            {
                List<LogVM> result = new();
                foreach (LoggerEventArgs log in _loggerService.GetLogs()) 
                    { result.Add(new LogVM(log)); }
                return result;
            }
        }
    }
}
