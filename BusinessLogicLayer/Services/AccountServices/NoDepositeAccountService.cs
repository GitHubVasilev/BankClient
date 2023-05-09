using BusinessLogicLayer.DTO.Accounts;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.AccountServices
{
    /// <summary>
    /// Класс для управления недепозитными счетми
    /// </summary>
    public class NoDepositeAccountService : AbstractAccountService<NoDepositeAccountDTO>
    {
        public NoDepositeAccountService(IUnitOfWork uow) : base(uow)
        {
            _typeAccounts = TypeAccounts.NoDepisite;
        }

        /// <summary>
        /// Пополняет указанный счет
        /// </summary>
        /// <param name="toAccount">Счет для пополнения</param>
        /// <param name="sum">Сумма пополения</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Обновленные данные о счете. Если пополение не удалось, вызывается иключение</returns>
        public override NoDepositeAccountDTO Put(Guid toAccount, decimal sum)
        {
#nullable disable
            Account model = _repository.Find(m => m.UID == toAccount).LastOrDefault();
            if (model is null)
            { throw new ArgumentOutOfRangeException(toAccount.ToString(), "Счет c ID не найден"); }

            model.CountMonetaryUnit += sum;
            _repository.Updata(model);
            return GetAccountForCustomer(model.Customer.UID);
#nullable restore
        }

        /// <summary>
        /// Пополняет указанный счет
        /// Асинхронный метод
        /// </summary>
        /// <param name="toAccount">Счет для пополнения</param>
        /// <param name="sum">Сумма пополения</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Обновленные данные о счете. Если пополение не удалось, вызывается иключение</returns>
        public override async Task<NoDepositeAccountDTO> PutAsync(Guid toAccount, decimal sum)
        {
#nullable disable
            Task<NoDepositeAccountDTO> task = Task.Run(async () =>
            {
                Account model = _repository.Find(m => m.UID == toAccount).LastOrDefault();
                if (model is null)
                { throw new ArgumentOutOfRangeException(toAccount.ToString(), "Счет c ID не найден"); }

                model.CountMonetaryUnit += sum;
                await _repository.UpdataAsync(model);
                return await GetAccountForCustomerAsync(model.Customer.UID);
            });
            return await task.ConfigureAwait(false);
#nullable restore
        }

        /// <summary>
        /// Снимает средства со счета
        /// </summary>
        /// <param name="fromAccount">Счет для снятия</param>
        /// <param name="sum">Сумма снятия</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Обновленные данные о счете. Если снятие не удалось, то вызыется исключение</returns>
        public override NoDepositeAccountDTO Withdraw(Guid fromAccount, decimal sum)
        {
#nullable disable
            Account model = _repository.Find(m => m.UID == fromAccount).LastOrDefault();
            if (model is null)
                { throw new ArgumentOutOfRangeException(fromAccount.ToString(), "Счет c ID не найден"); }

            model.CountMonetaryUnit -= sum;
            _repository.Updata(model);
            return GetAccountForCustomer(model.Customer.UID);
#nullable restore
        }

        /// <summary>
        /// Снимает средства со счета.
        /// Асинхронный метод
        /// </summary>
        /// <param name="fromAccount">Счет для снятия</param>
        /// <param name="sum">Сумма снятия</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Обновленные данные о счете. Если снятие не удалось, то вызыется исключение</returns>
        public override async Task<NoDepositeAccountDTO> WithdrawAsync(Guid fromAccount, decimal sum)
        {
#nullable disable
            Task<NoDepositeAccountDTO> task = Task.Run(async () =>
            {
                Account model = _repository.Find(m => m.UID == fromAccount).LastOrDefault();
                if (model is null)
                { throw new ArgumentOutOfRangeException(fromAccount.ToString(), "Счет c ID не найден"); }

                model.CountMonetaryUnit -= sum;
                await _repository.UpdataAsync(model);
                return await GetAccountForCustomerAsync(model.Customer.UID);
            });
            return await task.ConfigureAwait(false);
#nullable restore
        }
    }
}
