using DataAccessLayer.Entities;
using System;

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
        IRepository<Customer> Customers { get; }
        IRepository<Account> Accounts { get; }
        void Save();
    }
}
