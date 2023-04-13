using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Репозиторий определяет базовые методы для обрщению к источнику данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class GeneralRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        private DbSet<T> _dbSet;


        public GeneralRepository(DbContext dbContext)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// Добавляет запись в список моделей
        /// </summary>
        /// <param name="item">Новая модель</param>
        public void Create(T item)
        {
            _dbSet.Add(item);
            SaveChange();
        }

        /// <summary>
        /// Поиск по параметру (Where запрос)
        /// </summary>
        /// <param name="predicate">Условие выбора модели</param>
        /// <returns>Список моделей, подходящие под условие</returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        /// <summary>
        /// Список всех моделей
        /// </summary>
        /// <returns>IQueryable запрос</returns>
        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        /// <summary>
        /// Удаляет запись из источника данных
        /// </summary>
        /// <param name="item">Объект для удаления</param>
        public void Remove(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
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

        /// <summary>
        /// Обновляет запись в списке моделей
        /// </summary>
        /// <param name="item">Обновленная модель</param>
        public void Updata(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            SaveChange();
        }
    }
}
