using BusinessLogicLayer.DTO.Accounts;
using System;

namespace BusinessLogicLayer.DTO
{
    /// <summary>
    /// Объект Клинета для передечи данных между сервером и пользователем
    /// </summary>
    public record CustomerDTO
    {
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public Guid UID { get; init; }

        /// <summary>
        /// Имя
        /// </summary>
        public string? FirstName { get; init; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string? LastName { get; init; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; init; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string? Telephone { get; init; }

        /// <summary>
        /// Данные паспорта
        /// </summary>
        public string? Passport { get; init; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public long DateChange { get; init; }

        /// <summary>
        /// Название поля, которое было изменено последним
        /// </summary>
        public FieldChanged FieldChanged { get; init; }

        /// <summary>
        /// Тип последнего изменения
        /// </summary>
        public TypeChanged TypeChanged { get; init; }

        /// <summary>
        /// Название сотрудника, который последний производил изменения
        /// </summary>
        public string? ChangingWorker { get; init; }

        /// <summary>
        /// Первый Счет
        /// </summary>
        public NoDepositeAccountDTO? NoDepositeAccount { get; init; }

        /// <summary>
        /// Второй Счет
        /// </summary>
        public DepositeAccountDTO? DepositeAccount { get; init; }
    }
}
