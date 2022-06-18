using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Accounts;

namespace BusinessLogicLayer.Interfaces.Accounts
{
    /// <summary>
    /// Перевод между счетами клиентов
    /// </summary>
    /// <typeparam name="T">Тип счета отправителя</typeparam>
    /// <typeparam name="K">Тип счета получателя</typeparam>
    public interface ITransaction<in T, in K> 
        where T : BaseAccountDTO
        where K : BaseAccountDTO
    {
        /// <summary>
        /// Переводит средства между счетами клиентов
        /// </summary>
        /// <param name="fromAccount">Счет отправителя</param>
        /// <param name="toAccount">Счет получателя</param>
        /// <param name="sum">Сумма для перевода</param>
        /// <returns>Результат выполнения перевода</returns>
        ResultTransactionDTO ToTransaction(T? fromAccount, K? toAccount, decimal sum);
    }
}
