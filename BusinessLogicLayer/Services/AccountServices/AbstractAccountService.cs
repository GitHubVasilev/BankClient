using AutoMapper;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services.AccountServices
{
    /// <summary>
    /// Абстрактный класс описывает методы для управления счетами
    /// </summary>
    /// <typeparam name="T">Тип счета</typeparam>
    public abstract class AbstractAccountService<T> : IAccountHeandler<T>
        where T : BaseAccountDTO
    {
        protected List<Account> _listAccount;
        protected readonly IRepository<Account> _repository;
        protected TypeAccounts _typeAccounts;

        public AbstractAccountService(IUnitOfWork uow)
        {
            _repository = uow.Accounts;
            _listAccount = uow.Accounts.GetAll().ToList();
        }

        /// <summary>
        /// Закрывает счет
        /// </summary>
        /// <param name="account">Данные счета для закрытия</param>
        public void CloseAccount(T? account)
        {
            if (account == null)
                { throw new Exception("Счет не найден"); }

            int indexModel = _listAccount.FindLastIndex(m => m.UID == account.UID);

            if (indexModel == -1)
                { throw new Exception("Счет не найден"); }

            _listAccount[indexModel].IsClose = true;
            _repository.Updata(_listAccount[indexModel]);
        }

        /// <summary>
        /// Возвращает счет клиента
        /// </summary>
        /// <param name="UIDCustomer">Идентификатор клиента</param>
        /// <returns>Счет клиента</returns>
        public virtual T? GetAccountForCustomer(Guid UIDCustomer) 
        {
            Account? findAccount = _listAccount.LastOrDefault(m => m.UIDClient == UIDCustomer
                                                     && m.TypeAccount == (int)_typeAccounts
                                                     && m.IsClose == false);
            if (findAccount == null) { return null; }
            MapperConfiguration config = new(cfg => cfg.CreateMap<Account, T>());
            Mapper mapper = new(config);
            return mapper.Map<Account, T>(findAccount);
        }

        /// <summary>
        /// Возращает все существующие счета
        /// </summary>
        /// <returns>Счета всех пользователей</returns>
        public virtual IEnumerable<T> GetAllAccounts()
        {
            List<T> result = new();
            MapperConfiguration config = new(cfg => cfg.CreateMap<Account, T>());
            Mapper mapper = new(config);
            foreach (Account account in _listAccount)
            {
                if ((TypeAccounts)account.TypeAccount == _typeAccounts && account.IsClose == false)
                {
                    result.Add(mapper.Map<Account, T>(account));
                }
            }
            return result;
        }

        /// <summary>
        /// Открывает счет
        /// </summary>
        /// <param name="account">Данные для открытия счета</param>
        public virtual void OpenAccount(T account) 
        {
            MapperConfiguration config = new(cfg => cfg.CreateMap<T, Account>()
                                            .ForMember("DateOpen", opt => opt.MapFrom(m => DateTime.Now.Ticks))
                                            .ForMember("TypeAccount", opt => opt.MapFrom(m => (int)_typeAccounts))
                                            .ForMember("IsClose", opt => opt.MapFrom(m => false)));
            Mapper mapper = new(config);
            Account model = mapper.Map<T, Account>(account);
            _repository.Create(model);
            _listAccount.Add(model);
        }

        /// <summary>
        /// Пополняет счет
        /// </summary>
        /// <param name="toAccount">Счет для попонения</param>
        /// <param name="sum">Сумма пополения</param>
        /// <returns></returns>
        public abstract T Put(Guid toAccount, decimal sum);

        /// <summary>
        /// Снимает средства со счета
        /// </summary>
        /// <param name="fromAccount">Счет для снятия</param>
        /// <param name="sum">Сумма снятия</param>
        /// <returns>Обновленные данны о счете</returns>
        public abstract T Withdraw(Guid fromAccount, decimal sum);
    }
}
