using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Infrastructure.Exceptions;
using BusinessLogicLayer.Interfaces.Accounts;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Services
{
    /// <summary>
    /// Абстрактный класс работника.
    /// По-умолчанию работник умеет изменять все поля, получать данные о клиентах без изменения
    /// </summary>
    public abstract class AbstractWorker
    {
        protected readonly IUnitOfWork _uow;
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
        protected ValidateModel validate = new ValidateModel();

        public AbstractWorker(IUnitOfWork unitOfWork,
            IAccountHeandler<DepositeAccountDTO> accountServiceDeposite,
            IAccountHeandler<NoDepositeAccountDTO> accountServiceNoDeposite,
            IPutAndWithdrawMoney<DepositeAccountDTO> putAndWhitdrawDepositeService,
            IPutAndWithdrawMoney<NoDepositeAccountDTO> putAndWhitdrawNoDepositeService)
        {
            _uow = unitOfWork;
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
            Customer model = _uow.Customers.Find(m => customer.UID == m.UID).First();
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
            _uow.Customers.Updata(model);
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
            return ConverterCustomer.ToCustomerDTO(_uow.Customers.Find(m => m.UID == customerUID).First(), depositeAccountDTO, noDepositeAccountDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список клиентов</returns>
        public virtual IEnumerable<CustomerDTO> GetCustomers()
        {
            List<CustomerDTO> result = new();

            foreach (Customer customer in _uow.Customers.GetAll())
            {
                DepositeAccountDTO depositeAccountDTO = _accountServiceDeposite.GetAccountForCustomer(customer.UID)!;
                NoDepositeAccountDTO noDepositeAccountDTO = _accountServiceNoDeposite.GetAccountForCustomer(customer.UID)!;
                result.Add(ConverterCustomer.ToCustomerDTO(customer, depositeAccountDTO, noDepositeAccountDTO));
            }
            return result;
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
