using BusinessLogicLayer.DTO.Accounts;
using System;

namespace BusinessLogicLayer.Interfaces.Accounts
{
    /// <summary>
    /// Содержит методы для пополнения и снятия средств со счетов
    /// </summary>
    /// <typeparam name="T">Тип счета</typeparam>
    public interface IPutAndWithdrawMoney<out T>
        where T : BaseAccountDTO
    {
        /// <summary>
        /// Пополняет указанный счет
        /// </summary>
        /// <param name="toAccount">Счет для пополения</param>
        /// <param name="sum">Сумма пополнения</param>
        /// <returns>Обновленные данные о счете</returns>
        T Put(Guid toAccount, decimal sum);
        /// <summary>
        /// Снимает средства со счета
        /// </summary>
        /// <param name="fromAccount">Счет для снятия</param>
        /// <param name="sum">Сумма снятия</param>
        /// <returns>Обновленные данные о счете</returns>
        T Withdraw(Guid fromAccount, decimal sum);
    }
}
