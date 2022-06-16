using System;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    /// <summary>
    /// Интерфейс взаимодействия с данными хранящимися в источнике
    /// </summary>
    /// <typeparam name="T">Тип модели, хранящееся в источнике</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список всех моделей из источника</returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">Условие выбора модели</param>
        /// <returns>Список моделей, подходящие под условие</returns>
        IEnumerable<T> Find(Func<T, bool> predicate);

        /// <summary>
        /// Обновляет запись в списке моделей
        /// </summary>
        /// <param name="item">Обновленная модель</param>
        void Updata(T item);

        /// <summary>
        /// Добавляет запись в список моделей
        /// </summary>
        /// <param name="item">Новая модель</param>
        void Create(T item);
    }
}
