using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using Exceptions;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    /// <summary>
    /// Класс описывает методы которые может использовать консультант
    /// </summary>
    public class Consultant : AbstractWorker, IConsultant
    {
        public Consultant(IUnitOfWork unitOfwork,
            IAccountHeandler<DepositeAccountDTO> accountServiceDeposite,
            IAccountHeandler<NoDepositeAccountDTO> accountServiceNoDeposite,
            IPutAndWithdrawMoney<DepositeAccountDTO> putAndWhitdrawDepositeService,
            IPutAndWithdrawMoney<NoDepositeAccountDTO> putAndWhitdrawNoDepositeService)
            : base(unitOfwork, accountServiceDeposite, accountServiceNoDeposite, putAndWhitdrawDepositeService, putAndWhitdrawNoDepositeService)
        {
            Name = "Консультант";
        }

        /// <summary>
        /// Заменяет значение поля Passport на звездочки(*). Если поле изначально не заполнено, то поле останется пустым
        /// </summary>
        /// <returns>Список клиентов</returns>
        public override IEnumerable<CustomerDTO> GetCustomers()
        {
            List<CustomerDTO> result = new();

            foreach (Customer customer in _repository.GetAll()) 
            {
                string hiddenPassport = string.IsNullOrWhiteSpace(customer.Passport) ? " ": "******************";
                DepositeAccountDTO depositeAccountDTO = _accountServiceDeposite.GetAccountForCustomer(customer.UID)!;
                NoDepositeAccountDTO noDepositeAccountDTO = _accountServiceNoDeposite.GetAccountForCustomer(customer.UID)!;
                result.Add(new CustomerDTO()
                {
                    UID = customer.UID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Patronymic = customer.Patronymic,
                    Telephone = customer.Telephone,
                    Passport = hiddenPassport,
                    DateChange = customer.DateChange,
                    TypeChanged = (TypeChanged)customer.TypeChanged,
                    FieldChanged = (FieldChanged)customer.FieldChanged,
                    ChangingWorker = customer.ChangingWorker,
                    DepositeAccount = depositeAccountDTO,
                    NoDepositeAccount = noDepositeAccountDTO
                });
            }
            return result;
        }


        /// <summary>
        /// Заменяет значение поля Passport на звездочки(*). Если поле изначально не заполнено, то поле останется пустым
        /// </summary>
        /// <returns>Список клиентов</returns>
        public override async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            Task<IEnumerable<CustomerDTO>> task = Task.Run(() =>
            {
                return GetCustomers();
            });

            return await task.ConfigureAwait(false);
        }


        /// <summary>
        /// Заменяет значение поля Passport на звездочки(*). Если изначально поле не заполнено, поле останется пустым
        /// Если клиент не найден, вызывает исключение <see cref="InvalidOperationException"/>
        /// </summary>
        /// <param name="customerUID">Идентификационный номер клиента</param>
        /// <returns>Найденный клиент</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override CustomerDTO GetCustomer(Guid customerUID)
        {
            Customer model = _repository.Find(m => m.UID == customerUID).First();
            model.Passport = string.IsNullOrWhiteSpace(model.Passport) ? " " : "******************";
            DepositeAccountDTO depositeAccountDTO = _accountServiceDeposite.GetAccountForCustomer(customerUID)!;
            NoDepositeAccountDTO noDepositeAccountDTO = _accountServiceNoDeposite.GetAccountForCustomer(customerUID)!;
            CustomerDTO customer = ConverterCustomer.ToCustomerDTO(model, depositeAccountDTO, noDepositeAccountDTO);
            return customer;
        }

        /// <summary>
        /// Заменяет значение поля Passport на звездочки(*). Если изначально поле не заполнено, поле останется пустым
        /// Если клиент не найден, вызывает исключение <see cref="InvalidOperationException"/>
        /// Асинхронный метод
        /// </summary>
        /// <param name="customerUID">Идентификационный номер клиента</param>
        /// <returns>Найденный клиент</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override async Task<CustomerDTO> GetCustomerAsync(Guid customerUID)
        {
            Task<CustomerDTO> task = Task.Run(() =>
            {
                return GetCustomer(customerUID);
            });
            return await task.ConfigureAwait(false);
        }

        /// <summary>
        /// Выдает исключение <see cref="InsufficeintPermissionsException"/>
        /// </summary>
        /// <param name="customer">Клиент</param>
        /// <param name="firstname">Имя клиента</param>
        /// <exception cref="InsufficeintPermissionsException"/>
        protected override void FirstNameUpdata(ref Customer customer, string? firstname)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для изменения имени пользователя!");
        }

        /// <summary>
        /// Выдает исключение <see cref="InsufficeintPermissionsException"/>
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="lastname"></param>
        /// <exception cref="InsufficeintPermissionsException"/>
        protected override void LastNameUpdata(ref Customer customer, string? lastname)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для изменения имени пользователя!");
        }

        /// <summary>
        /// Выдает исключение <see cref="InsufficeintPermissionsException"/>
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="patronymic"></param>
        /// <exception cref="InsufficeintPermissionsException"/>
        protected override void PatronymicUpdata(ref Customer customer, string? patronymic)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для изменения отчества пользователя!");
        }

        /// <summary>
        /// Выдает исключение <see cref="InsufficeintPermissionsException"/>
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="passport"></param>
        /// <exception cref="InsufficeintPermissionsException"/>
        protected override void PassportUpdata(ref Customer customer, string? passport)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для изменения паспорта пользователя!");
        }
    }
}
