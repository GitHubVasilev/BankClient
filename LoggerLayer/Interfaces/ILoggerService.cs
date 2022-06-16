using LoggerLayer.DTO;
using System.Collections.Generic;

namespace LoggerLayer.Interfaces
{
    /// <summary>
    /// Описывает работу логгера
    /// </summary>
    public interface ILoggerService
    {
        delegate void LoggerEventHandler(object sender, LoggerEventArgs e);
        /// <summary>
        /// События добавления новой записи
        /// </summary>
        event LoggerEventHandler EventAddRecordLog;

        /// <summary>
        /// Создает новую запись в логгах
        /// </summary>
        /// <param name="nameWorker">Имя сотрудника, который совержил действие</param>
        /// <param name="descriptionEvent">Тип/Описание события</param>
        void WriteLog(string nameWorker, string descriptionEvent);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Все записи логгов</returns>
        IEnumerable<LoggerEventArgs> GetLogs();

    }
}
