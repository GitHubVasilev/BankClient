using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces.Accounts;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    /// <summary>
    /// Абстрактный класс работника.
    /// По-умолчанию работник умеет изменять все поля, получать данные о клиентах без изменения
    /// </summary>
    public abstract class AbstractWorker
    {
        protected readonly IRepository<Customer> _repository;
        protected readonly IAccountHeandler<DepositeAccountDTO> _accountServiceDeposite;
        protected readonly IAccountHeandler<NoDepositeAccountDTO> _accountServiceNoDeposite;
        protected IPutAndWithdrawMoney<DepositeAccountDTO> _putAndWhitdrawDepositeService;
        protected IPutAndWithdrawMoney<NoDepositeAccountDTO> _putAndWhitdrawNoDepositeService;

#nullable disable
        /// <summary>
        /// Название консультанта
        /// </summary>
        public string Name { get; set; }
#nullable restore
        protected ValidateModel validate = new();

        public AbstractWorker(IUnitOfWork unitOfWork,
            IAccountHeandler<DepositeAccountDTO> accountServiceDeposite,
            IAccountHeandler<NoDepositeAccountDTO> accountServiceNoDeposite,
            IPutAndWithdrawMoney<DepositeAccountDTO> putAndWhitdrawDepositeService,
            IPutAndWithdrawMoney<NoDepositeAccountDTO> putAndWhitdrawNoDepositeService)
        {
            _repository = unitOfWork.GetRepository<Customer>();
            _accountServiceDeposite = accountServiceDeposite;
            _accountServiceNoDeposite = accountServiceNoDeposite;
            _putAndWhitdrawDepositeService = putAndWhitdrawDepositeService;
            _putAndWhitdrawNoDepositeService = putAndWhitdrawNoDepositeService;
        }
        /// <summary>
        /// Обнавляет данные о клиенте.
        /// Может изменить все поля.
        /// </summary>
        /// <param name="model">Обновленная модель</param>
        /// <param name="type">Тип изменения
        /// Если тип не поддерживается будет вызвано исключение <see cref="Exception"/></param>
        /// <exception cref="Exception"/>
        public void UpdataCustomer(CustomerDTO customer, FieldChanged type)
        {
            Customer model = _repository.Find(m => customer.UID == m.UID).First();
            model.DateChange = DateTime.Now.Ticks;
            model.ChangingWorker = Name;
            model.FieldChanged = (int)type;
            model.TypeChanged = (int)TypeChanged.Update;

            switch (type)
            {
                case FieldChanged.Firstname:
                    FirstNameUpdata(ref model, customer.FirstName);
                    break;
                case FieldChanged.Lastname:
                    LastNameUpdata(ref model, customer.LastName);
                    break;
                case FieldChanged.Patronymic:
                    PatronymicUpdata(ref model, customer.Patronymic);
                    break;
                case FieldChanged.Telephone:
                    TelephoneUpdata(ref model, customer.Telephone);
                    break;
                case FieldChanged.Passport:
                    PassportUpdata(ref model, customer.Passport);
                    break;
                default:
                    throw new InvalidOperationException("Неизветный тип поля");
            }
            _repository.Updata(model);
        }

        /// <summary>
        /// Обнавляет данные о клиенте. Асинхронный метод
        /// Может изменить все поля.
        /// </summary>
        /// <param name="model">Обновленная модель</param>
        /// <param name="type">Тип изменения
        /// Если тип не поддерживается будет вызвано исключение <see cref="Exception"/></param>
        /// <exception cref="Exception"/>
        public async Task UpdataCustomerAsync(CustomerDTO customer, FieldChanged type)
        {
            Task task = Task.Run(() =>
            {
                UpdataCustomer(customer, type);
            });
            await task.ConfigureAwait(false);
        }

        public IAccountHeandler<DepositeAccountDTO> DepositeAccountService => _accountServiceDeposite;
        public IAccountHeandler<NoDepositeAccountDTO> NoDepositeAccountService => _accountServiceNoDeposite;

        public IPutAndWithdrawMoney<DepositeAccountDTO> PutAndWithdrawDepositeService => _putAndWhitdrawDepositeService;
        public IPutAndWithdrawMoney<NoDepositeAccountDTO> PutAndWithdrawNoDepositeService => _putAndWhitdrawNoDepositeService;

        /// <summary>
        /// Если клиент не найден, вызывает исключение <see cref="InvalidOperationException"/>
        /// </summary>
        /// <param name="customerUID">Идентификационный номер клиента</param>
        /// <returns>Возвращает найденого клиента</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public virtual CustomerDTO GetCustomer(Guid customerUID)
        {
            DepositeAccountDTO depositeAccountDTO = _accountServiceDeposite.GetAccountForCustomer(customerUID)!;
            NoDepositeAccountDTO noDepositeAccountDTO = _accountServiceNoDeposite.GetAccountForCustomer(customerUID)!;
            return ConverterCustomer.ToCustomerDTO(_repository.Find(m => m.UID == customerUID).First(), depositeAccountDTO, noDepositeAccountDTO);
        }

        /// <summary>
        /// Если клиент не найден, вызывает исключение <see cref="InvalidOperationException"/>
        /// Асинхронный метод.
        /// </summary>
        /// <param name="customerUID">Идентификационный номер клиента</param>
        /// <returns>Возвращает найденого клиента</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public virtual async Task<CustomerDTO> GetCustomerAsync(Guid customerUID)
        {
            Task<CustomerDTO> task = Task.Run(() => { 
                return GetCustomer(customerUID);
            });
            return await task.ConfigureAwait(false);
        }

        /// <summary>
        /// Создает список клиентов
        /// </summary>
        /// <returns>Список клиентов</returns>
        public virtual IEnumerable<CustomerDTO> GetCustomers()
        {
            List<CustomerDTO> result = new();

            foreach (Customer customer in _repository.GetAll())
            {
                DepositeAccountDTO depositeAccountDTO = _accountServiceDeposite.GetAccountForCustomer(customer.UID)!;
                NoDepositeAccountDTO noDepositeAccountDTO = _accountServiceNoDeposite.GetAccountForCustomer(customer.UID)!;
                result.Add(ConverterCustomer.ToCustomerDTO(customer, depositeAccountDTO, noDepositeAccountDTO));
            }
            return result;
        }

        /// <summary>
        /// Создает список клиентов асинхроно
        /// </summary>
        /// <returns>Список клиентов</returns>
        public virtual async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            Task<IEnumerable<CustomerDTO>> task = Task.Run(() =>
            {
                return GetCustomers();
            });

            return await task.ConfigureAwait(false);
        }

        /// <summary>
        /// Добавляет нового клиента в источник данных.
        /// По-умолчанию бросает исключение <see cref="Exception"/>
        /// </summary>
        /// <param name="customer">модель для добавления</param>
        /// <exception cref="InsufficeintPermissionsException"/>
        public virtual void CreateCustomer(CustomerDTO customer)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для добавления нового пользователя");
        }

        /// <summary>
        /// Добавляет нового клиента в источник данных. Асинхронный метод
        /// По-умолчанию бросает исключение <see cref="Exception"/>
        /// </summary>
        /// <param name="customer">модель для добавления</param>
        /// <exception cref="InsufficeintPermissionsException"/>
        public virtual async Task CreateCustomerAsync(CustomerDTO customer)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для добавления нового пользователя");
        }

        /// <summary>
        /// Обновляет свойство FirstName в <paramref name="customer"/> 
        /// </summary>
        /// <param name="customer">модель для изменения</param>
        protected virtual void FirstNameUpdata(ref Customer customer, string? firstname)
        {
            validate.ValideteFirstName(firstname);
            customer.FirstName = firstname;
        }

        /// <summary>
        /// Обновляет свойство LastName в <paramref name="customer"/> 
        /// </summary>
        /// <param name="customer">модель для изменения</param>
        protected virtual void LastNameUpdata(ref Customer customer, string? lastname)
        {
            validate.ValideteLastName(lastname);
            customer.LastName = lastname;
        }

        /// <summary>
        /// Обновляет свойство Passport в <paramref name="customer"/> 
        /// </summary>
        /// <param name="customer">модель для изменения</param>
        protected virtual void PassportUpdata(ref Customer customer, string? passport)
        {
            validate.ValidetePassport(passport);
            customer.Passport = passport;
        }

        /// <summary>
        /// Обновляет свойство Patronymic в <paramref name="customer"/> 
        /// </summary>
        /// <param name="customer">модель для изменения</param>
        protected virtual void PatronymicUpdata(ref Customer customer, string? patronymic)
        {
            validate.ValidetePatronymic(patronymic);
            customer.Patronymic = patronymic;
        }

        /// <summary>
        /// Обновляет свойство Telephone в <paramref name="customer"/> 
        /// </summary>
        /// <param name="customer">модель для изменения</param>
        protected virtual void TelephoneUpdata(ref Customer customer, string? telephone)
        {
            validate.ValideteTelephone(telephone);
            customer.Telephone = telephone;
        }
    }
}
