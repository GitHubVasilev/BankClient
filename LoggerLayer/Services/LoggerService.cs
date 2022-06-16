using LoggerLayer.DTO;
using LoggerLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace LoggerLayer.Services
{
    /// <summary>
    /// Сервис управления логгами
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private List<LoggerEventArgs> _log;

        public LoggerService()
        {
            _log = new List<LoggerEventArgs>();
        }

        public delegate void LoggerEventHandler(object sender, LoggerEventArgs e);
        private ILoggerService.LoggerEventHandler? _eventLog;
        /// <summary>
        /// События добавления нового лога
        /// </summary>
        public event ILoggerService.LoggerEventHandler EventAddRecordLog
        {
            add => _eventLog += value;
            remove => _eventLog -= value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Все записи логов</returns>
        public IEnumerable<LoggerEventArgs> GetLogs()
        {
            return _log;
        }

        /// <summary>
        /// Создает запись нового лога
        /// </summary>
        /// <param name="nameWorker">Имя сотрудника, который совержил действие</param>
        /// <param name="descriptionEvent">Тип/Описание события</param>
        public void WriteLog(string nameWorker, string descriptionEvent)
        {
            LoggerEventArgs eventArgs = new()
            {
                NameWorker = nameWorker,
                TypeOperation = descriptionEvent,
                DateOperation = DateTime.Now,
            };
            _log.Add(eventArgs);
            _eventLog?.Invoke(this, eventArgs);
        }
    }
}
