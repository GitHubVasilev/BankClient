using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;

namespace BusinessLogicLayer.Services.AccountServices
{
    /// <summary>
    /// Класс для управления переводами средств между счетами
    /// </summary>
    /// <typeparam name="T">Тип счета для снятия</typeparam>
    /// <typeparam name="K">Тип счета для пополения</typeparam>
    public class Transaction<T, K> : ITransaction<T, K>
        where T : BaseAccountDTO
        where K : BaseAccountDTO
    {
        private readonly IPutAndWithdrawMoney<T> _accountHeandlerT;
        private readonly IPutAndWithdrawMoney<K> _accountHeandlerK;

        public Transaction(IPutAndWithdrawMoney<T> putAndWhitdrawMoneyT, IPutAndWithdrawMoney<K> putAndWhitdrawMoneyK)
        {
            _accountHeandlerT = putAndWhitdrawMoneyT;
            _accountHeandlerK = putAndWhitdrawMoneyK;
        }

        /// <summary>
        /// Выполняет перевод между счетами
        /// </summary>
        /// <param name="fromAccount">Счет для снятия</param>
        /// <param name="toAccount">Счет для перевода</param>
        /// <param name="sum">Сумма перевода</param>
        /// <returns>Результат перевода. В случае неудачи выбрасывает исключение</returns>
        public ResultTransactionDTO ToTransaction(T? fromAccount, K? toAccount, decimal sum)
        {
            if (fromAccount != null && toAccount != null)
            {
                BaseAccountDTO resultT = _accountHeandlerT.Withdraw(fromAccount.UID, sum);
                BaseAccountDTO resultK = _accountHeandlerK.Put(toAccount.UID, sum);
                return new ResultTransactionDTO() 
                {
                    FromAccount =  resultT,
                    ToAccount = resultK
                };
            }

            if (fromAccount == null)
                throw new System.ArgumentNullException(nameof(toAccount),"Не переданны данные о счетe отправителя");
            throw new System.ArgumentNullException(nameof(fromAccount), "Не переданны данные о счетe получателя");
        }
    }
}
