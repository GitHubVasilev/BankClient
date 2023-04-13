using DataAccessLayer.Entities;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    /// <summary>
    /// Предоставляет доступ к репозиториям 
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Интерфес взаимодействия с данными из источника данных
        /// </summary>
        IRepository<T> GetRepository<T>() where T : class;

        /// <summary>
        /// Добавляет все изменения в источник данных
        /// </summary>
        void SaveChange();
        /// <summary>
        /// Добавляет все изменения в источник данных асинхронно
        /// </summary>
        Task SaveChangeAsync();
    }
}
