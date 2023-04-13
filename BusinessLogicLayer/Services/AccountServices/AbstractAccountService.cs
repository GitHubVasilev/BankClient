using AutoMapper;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Infrastructure;
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
        protected readonly IRepository<Account> _repository;
        protected TypeAccounts _typeAccounts;

        public AbstractAccountService(IUnitOfWork uow)
        {
            _repository = uow.GetRepository<Account>();
        }

        /// <summary>
        /// Закрывает счет
        /// </summary>
        /// <param name="account">Данные счета для закрытия</param>
        public void CloseAccount(T? account)
        {
            if (account == null)
                { throw new ArgumentNullException(nameof(account), "Счет не найден"); }

            Account? model = _repository.Find(m => m.UID == account.UID).OrderBy(m => m).LastOrDefault();

            if (model is null)
                { throw new ArgumentOutOfRangeException(nameof(account.UID) ,"Счет не найден"); }

            _repository.Remove(model);
        }

        /// <summary>
        /// Возвращает счет клиента
        /// </summary>
        /// <param name="UIDCustomer">Идентификатор клиента</param>
        /// <returns>Счет клиента</returns>
        public virtual T? GetAccountForCustomer(Guid UIDCustomer) 
        {
            Account? findAccount = _repository.Find(m => m.Customer.UID == UIDCustomer
                                                     && m.TypeAccount == (int)_typeAccounts
                                                     && m.IsClose == false)
                                                .OrderBy(m => m)
                                                .LastOrDefault();
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
            foreach (Account account in _repository.GetAll())
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
        public virtual void OpenAccount(T? account) 
        {
            if (account is null)
            {
                new ArgumentNullException(nameof(account),$"В поле {account} передано пустое значение");
            }
            ValidateModelAccount validator = new();
            validator.ValidateName(account!.Name);
            MapperConfiguration config = new(cfg => cfg.CreateMap<T, Account>()
                                            .ForMember("DateOpen", opt => opt.MapFrom(m => DateTime.Now.Ticks))
                                            .ForMember("TypeAccount", opt => opt.MapFrom(m => (int)_typeAccounts))
                                            .ForMember("IsClose", opt => opt.MapFrom(m => false))
                                            .ForMember("ClientId", opt => opt.MapFrom(m => m.UIDClient)));
            Mapper mapper = new(config);
            try 
            {
                Account model = mapper.Map<T, Account>(account!);
                _repository.Create(model);
            }
            catch (Exception ex) 
            {
                
            }
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
