using BusinessLogicLayer.DTO.Accounts;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces.Accounts
{
    /// <summary>
    /// Интрефейс с методами управления счетами
    /// </summary>
    /// <typeparam name="T">Тип счета</typeparam>
    public interface IAccountHeandler<T> : IPutAndWithdrawMoney<T>
        where T : BaseAccountDTO
    {
        /// <summary>
        /// Возвращает данные о счете клиента
        /// </summary>
        /// <param name="UIDCustomer">идентификатор клиента</param>
        /// <returns>счет клиента</returns>
        public T? GetAccountForCustomer(Guid UIDCustomer);
        /// <summary>
        /// </summary>
        /// <returns>Возвращает все счета указанного типа</returns>
        public IEnumerable<T?> GetAllAccounts();
        /// <summary>
        /// Создает счет
        /// </summary>
        /// <param name="account">Данные о создаваемом счете</param>
        public void OpenAccount(T account);
        /// <summary>
        /// Закрывает указанный счет
        /// </summary>
        /// <param name="account">Счет для закрытия</param>
        public void CloseAccount(T account);
    }
}
