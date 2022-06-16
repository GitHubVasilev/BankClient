using BusinessLogicLayer.DTO.Accounts;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;

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
        /// <returns>Обновленные данные о счете. Если пополение не удалось, вызывается иключение</returns>
        public override DepositeAccountDTO Put(Guid toAccount, decimal sum)
        {
            int indexmModel = _listAccount.FindLastIndex(m => m.UID == toAccount);
            if (indexmModel == -1) { throw new Exception("Счет не найден"); }
            Account model = _listAccount[indexmModel];
            sum *= 1 + (model.Procent / 100);
            model.CountMonetaryUnit += sum;
            _repository.Updata(model);
            return GetAccountForCustomer(model.UIDClient)!;
        }

        /// <summary>
        /// Снимает средства со счета
        /// </summary>
        /// <param name="fromAccount">Счет для снятия</param>
        /// <param name="sum">Сумма снятия</param>
        /// <returns>Обновленные данные о счете. Если снятие не удалось, то вызыется исключение</returns>
        public override DepositeAccountDTO Withdraw(Guid fromAccount, decimal sum)
        {
            int indexModel = _listAccount.FindLastIndex(m => m.UID == fromAccount);
            if (indexModel == -1) { throw new Exception("Счет не найден"); }
            Account model = _listAccount[indexModel];
            if (model.CountMonetaryUnit > 0)
            {
                sum *= 1 + (model.Procent / 100);
            }

            model.CountMonetaryUnit -= sum;
            _repository.Updata(model);
            return GetAccountForCustomer(model.UIDClient)!;
        }
    }
}
