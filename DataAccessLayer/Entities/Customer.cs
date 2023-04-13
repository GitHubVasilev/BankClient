using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Модель данных клиента
    /// </summary>
    public class Customer : Entity<Guid>
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// Данные паспорта
        /// </summary>
        public string Passport { get; set; }
        /// <summary>
        /// Список счетов клиента
        /// </summary>
        public List<Account> Accounts { get; set; } = new();
        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public long DateChange { get; set; }
        /// <summary>
        /// Последние измененное поле
        /// </summary>
        public int FieldChanged { get; set; }
        /// <summary>
        /// Тип последнего изменения
        /// </summary>
        public int TypeChanged { get; set; }
        /// <summary>
        /// Название сотрудника, который последний изменил запись
        /// </summary>
        public string ChangingWorker { get; set; }
    }
}
