using BusinessLogicLayer.DTO.Accounts;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Linq;

namespace BusinessLogicLayer.Services.AccountServices
{
    /// <summary>
    /// Класс для управления депозитными счетами
    /// </summary>
    public class DepositeAccountService : AbstractAccountService<DepositeAccountDTO>
    {
        public DepositeAccountService(IUnitOfWork uow) : base(uow)
        {
            _typeAccounts = TypeAccounts.Depisite;
        }

        /// <summary>
        /// Пополняет указанный счет
        /// </summary>
        /// <param name="toAccount">Счет для пополнения</param>
        /// <param name="sum">Сумма пополения</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Обновленные данные о счете. Если пополение не удалось, вызывается иключение</returns>
        public override DepositeAccountDTO Put(Guid toAccount, decimal sum)
        {
            Account? model = _repository.Find(m => m.UID == toAccount).OrderBy(m=>m).LastOrDefault();
            if (model is null) 
                { throw new ArgumentOutOfRangeException(toAccount.ToString(),"Счет c ID не найден"); }
            sum *= 1 + (model.Procent / 100);
            model.CountMonetaryUnit += sum;
            _repository.Updata(model);
            return GetAccountForCustomer(model.Customer.UID)!;
        }

        /// <summary>
        /// Снимает средства со счета
        /// </summary>
        /// <param name="fromAccount">Счет для снятия</param>
        /// <param name="sum">Сумма снятия</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>Обновленные данные о счете. Если снятие не удалось, то вызыется исключение</returns>
        public override DepositeAccountDTO Withdraw(Guid fromAccount, decimal sum)
        {
            Account? model = _repository.Find(m => m.UID == fromAccount).OrderBy(m => m).LastOrDefault();
            if (model is null)
                { throw new ArgumentOutOfRangeException(fromAccount.ToString(), "Счет c ID не найден"); }

            if (model.CountMonetaryUnit > 0)
            {
                sum *= 1 + (model.Procent / 100);
            }

            model.CountMonetaryUnit -= sum;
            _repository.Updata(model);
            return GetAccountForCustomer(model.Customer.UID)!;
        }
    }
}
