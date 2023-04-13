using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Предоставляет доступ к репозиториям 
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<Type, object> _repositories;

        private DbContext _context;

        public UnitOfWork(DbContext dbContext)
        {
            _context = dbContext;
        }

        /// <summary>
        /// Выполняет действия связанные с освобождением ресурсов
        /// </summary>
        public void Dispose()
        {
            _repositories?.Clear();
            _context.Dispose();
        }

        /// <summary>
        /// Возвращает конкретный репозиторий для <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Тип модели для репозиторий</typeparam>
        /// <returns>экземпляр репозитория для модели <typeparamref name="T"/></returns>
        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GeneralRepository<T>(_context);
            }

            return (IRepository<T>)_repositories[type];
        }

        /// <summary>
        /// Добавляет все изменения в источник данных
        /// </summary>
        public void SaveChange()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Добавляет все изменения в источник данных асинхронно
        /// </summary>
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
