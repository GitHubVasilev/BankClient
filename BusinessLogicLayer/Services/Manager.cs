using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using BusinessLogicLayer.Interfaces.Accounts;
using BusinessLogicLayer.DTO.Accounts;

namespace BusinessLogicLayer.Services
{
    /// <summary>
    /// Описание работы менеджера
    /// </summary>
    public class Manager : AbstractWorker, IManager
    {
        public Manager(IUnitOfWork unitOfwork,
            IAccountHeandler<DepositeAccountDTO> accountServiceDeposite,
            IAccountHeandler<NoDepositeAccountDTO> accountServiceNoDeposite,
            IPutAndWithdrawMoney<DepositeAccountDTO> putAndWhitdrawDepositeService,
            IPutAndWithdrawMoney<NoDepositeAccountDTO> putAndWhitdrawNoDepositeService)
            : base(unitOfwork, accountServiceDeposite, accountServiceNoDeposite, putAndWhitdrawDepositeService, putAndWhitdrawNoDepositeService)
        {
            Name = "Менеджер";
        }

        /// <summary>
        /// Добавляет новую запись о клиенте в источник данных. Если модель содержит ошибки вызывает исключение <see cref="Exception"/>
        /// </summary>
        /// <param name="customer">DTO нового клиента</param>
        /// <exception cref="Exception"></exception>
        public override void CreateCustomer(CustomerDTO customer)
        {
            Customer model = ConverterCustomer.ToCustomerModel(customer);
            FirstNameUpdata(ref model, customer.FirstName);
            LastNameUpdata(ref model, customer.LastName);
            PatronymicUpdata(ref model, customer.Patronymic);
            TelephoneUpdata(ref model, customer.Telephone);
            PassportUpdata(ref model, customer.Passport);
            model.ChangingWorker = Name;
            model.DateChange = DateTime.Now.Ticks;
            model.TypeChanged = (int)TypeChanged.Create;
            model.FieldChanged = (int)FieldChanged.None;

            _uow.Customers.Create(model);
        }
    }
}
