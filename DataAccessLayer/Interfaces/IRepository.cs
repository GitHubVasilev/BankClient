using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    /// <summary>
    /// Интерфейс взаимодействия с данными хранящимися в источнике
    /// </summary>
    /// <typeparam name="T">Тип модели, хранящееся в источнике</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Список всех моделей
        /// </summary>
        /// <returns>IQueryable запрос</returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Поиск по параметру (Where запрос)
        /// </summary>
        /// <param name="predicate">Условие выбора модели</param>
        /// <returns>Список моделей, подходящие под условие</returns>
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Обновляет запись в списке моделей
        /// </summary>
        /// <param name="item">Обновленная модель</param>
        void Updata(T item);

        /// <summary>
        /// Обновляет запись в списке моделей асинхроно
        /// </summary>
        /// <param name="item">Обновленная модель</param>
        Task UpdataAsync(T item);

        /// <summary>
        /// Добавляет запись в список моделей
        /// </summary>
        /// <param name="item">Новая модель</param>
        void Create(T item);

        /// <summary>
        /// Добавляет запись в список моделей асинхроно
        /// </summary>
        /// <param name="item">Новая модель</param>
        Task CreateAsync(T item);

        /// <summary>
        /// Удаляет запись из источника данных асинхроно
        /// </summary>
        /// <param name="item">Объект для удаления</param>
        Task RemoveAsync(T item);

        /// <summary>
        /// Удаляет запись из источника данных
        /// </summary>
        /// <param name="item">Объект для удаления</param>
        void Remove(T item);

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
