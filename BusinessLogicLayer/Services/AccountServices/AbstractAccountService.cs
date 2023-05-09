using AutoMapper;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces.Accounts;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            {
                throw new ArgumentNullException(nameof(account), "Счет не найден"); 
            }

            Account? model = _repository.Find(m => m.UID == account.UID).LastOrDefault();

            if (model is null)
            {
                throw new ArgumentOutOfRangeException(nameof(account.UID) ,"Счет не найден");
            }

            _repository.Remove(model);
        }

        /// <summary>
        /// Закрывает счет. Асинхронный метод
        /// </summary>
        /// <param name="account">Данные счета для закрытия</param>
        public async Task CloseAccountAsync(T? account)
        {
            Task<Account> task = Task.Run(() =>
            {
                if (account == null)
                {
                    throw new ArgumentNullException(nameof(account), "Счет не найден");
                }

                Account? model = _repository.Find(m => m.UID == account.UID).LastOrDefault();

                if (model is null)
                {
                    throw new ArgumentOutOfRangeException(nameof(account.UID), "Счет не найден");
                }
                return model;
            });

            await _repository.RemoveAsync(await task.ConfigureAwait(false));
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
                                                .LastOrDefault();
            if (findAccount == null) { return null; }
            MapperConfiguration config = new(cfg => cfg.CreateMap<Account, T>());
            Mapper mapper = new(config);
            return mapper.Map<Account, T>(findAccount);
        }

        /// <summary>
        /// Возвращает счет клиента
        /// Асинхронный метод
        /// </summary>
        /// <param name="UIDCustomer">Идентификатор клиента</param>
        /// <returns>Счет клиента</returns>
        public virtual async Task<T?> GetAccountForCustomerAsync(Guid UIDCustomer)
        {
            Task<T?> task = Task.Run(() =>
            {
                Account? findAccount = _repository.Find(m => m.Customer.UID == UIDCustomer
                                                         && m.TypeAccount == (int)_typeAccounts
                                                         && m.IsClose == false)
                                                    .LastOrDefault();
                if (findAccount == null) { return null; }
                MapperConfiguration config = new(cfg => cfg.CreateMap<Account, T>());
                Mapper mapper = new(config);
                return mapper.Map<Account, T>(findAccount);
            });
            return await task.ConfigureAwait(false);
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
        /// Возращает все существующие счета
        /// Асинхронный метод
        /// </summary>
        /// <returns>Счета всех пользователей</returns>
        public virtual async Task<IEnumerable<T>> GetAllAccountsAsync()
        {
            Task<List<T>> task = Task.Run(() =>
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
            });
            return await task.ConfigureAwait(false);
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
        /// Открывает счет. Асинхронный метод
        /// </summary>
        /// <param name="account">Данные для открытия счета</param>
        public virtual async Task OpenAccountAsync(T? account)
        {
            Task task = Task.Run(async () =>
            {
                if (account is null)
                {
                    new ArgumentNullException(nameof(account), $"В поле {account} передано пустое значение");
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
                    await _repository.CreateAsync(model);
                }
                catch (Exception ex)
                {

                }
            });

            await task.ConfigureAwait(false);
        }

        /// <summary>
        /// Пополняет счет
        /// </summary>
        /// <param name="toAccount">Счет для попонения</param>
        /// <param name="sum">Сумма пополения</param>
        /// <returns></returns>
        public abstract T Put(Guid toAccount, decimal sum);

        /// <summary>
        /// Пополняет счет
        /// Асинхронный метод
        /// </summary>
        /// <param name="toAccount">Счет для попонения</param>
        /// <param name="sum">Сумма пополения</param>
        /// <returns></returns>
        public abstract Task<T> PutAsync(Guid toAccount, decimal sum);

        /// <summary>
        /// Снимает средства со счета
        /// </summary>
        /// <param name="fromAccount">Счет для снятия</param>
        /// <param name="sum">Сумма снятия</param>
        /// <returns>Обновленные данны о счете</returns>
        public abstract T Withdraw(Guid fromAccount, decimal sum);

        /// <summary>
        /// Снимает средства со счета
        /// Асинхронный метод
        /// </summary>
        /// <param name="fromAccount">Счет для снятия</param>
        /// <param name="sum">Сумма снятия</param>
        /// <returns>Обновленные данны о счете</returns>
        public abstract Task<T> WithdrawAsync(Guid fromAccount, decimal sum);
    }
}
