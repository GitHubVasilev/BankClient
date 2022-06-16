using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using LoggerLayer.Interfaces;
using System;
using System.Collections.Generic;
using WpfApp1.Infrastructure;
using WpfApp1.ViewModel;

namespace WpfApp1.Interfaces
{
    /// <summary>
    /// Определяет работу сотрудника
    /// </summary>
    public interface IWorkerVM
    {
        IAccountHeandler<DepositeAccountDTO> DepositeAccountService { get; }
        IAccountHeandler<NoDepositeAccountDTO> NoDepositeAccountService { get; }
        ILoggerService Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список моделей-представлений клиента</returns>
        IEnumerable<CustomerVM> GetCustomers();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID">Идентификационный номер клиента</param>
        /// <returns>Модель представления для конкретного клиента</returns>
        CustomerVM GetCustomer(Guid UID);

        /// <summary>
        /// Название работника
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Изменяет имя клиента в источнике данных
        /// </summary>
        /// <param name="customer">Объект для передачи данных в источник</param>
        void FirstNameUpdate(CustomerDTO customer);

        /// <summary>
        /// Изменяет фамилию клиента в источнике данных
        /// </summary>
        /// <param name="customer">Объект для передачи данных в источник</param>
        void LastNameUpdate(CustomerDTO customer);

        /// <summary>
        /// Изменяет отчество клиента в источнике данных
        /// </summary>
        /// <param name="customer">Объект для передачи данных в источник</param>
        void PatronymicUpdate(CustomerDTO customer);

        /// <summary>
        /// Изменяет телефон клиента в источнике данных
        /// </summary>
        /// <param name="customer">Объект для передачи данных в источник</param>
        void TelephoneUpdate(CustomerDTO customer);

        /// <summary>
        /// Изменяет паспортные данные клиента в источнике данных
        /// </summary>
        /// <param name="customer">Объект для передачи данных в источник</param>
        void PassportUpdate(CustomerDTO customer);

        /// <summary>
        /// Команда для создания нового клиента
        /// </summary>
        public RelayCommand CreateCustomerCommand { get; }
    }
}
