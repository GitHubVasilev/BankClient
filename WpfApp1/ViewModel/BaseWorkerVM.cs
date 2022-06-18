using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces.Accounts;
using LoggerLayer.Interfaces;
using System;
using System.Collections.Generic;
using WpfApp1.Dialogs;
using WpfApp1.Infrastructure;
using WpfApp1.Infrastructure.Exceptions;
using WpfApp1.Interfaces;
using WpfApp1.ViewModel.Base;


namespace WpfApp1.ViewModel
{
    /// <summary>
    /// Реализует методы базового работникаю который ничего не умеет
    /// Реализует интерфейс <see cref="IWorkerVM"/>
    /// Наследует класс <see cref="BaseViewModel"/>
    /// </summary>
    public class BaseWorkerVM : BaseViewModel, IWorkerVM
    {
        public IAccountHeandler<DepositeAccountDTO> DepositeAccountService { get; set; }
        public IAccountHeandler<NoDepositeAccountDTO> NoDepositeAccountService { get; set; }
        public ILoggerService Logger { get; set; }

        /// <summary>
        /// Содержит методы стороннего сервиса Консультант
        /// </summary>
        protected IWorker _service;
        protected string _name;

        /// <summary>
        /// Название работника
        /// </summary>
        public virtual string Name
        {
            get => _name;
            set => Set(ref _name, value, nameof(Name));
        }

        protected RelayCommand _createCustomerCommand;

        /// <summary>
        /// Команда для добавления нового клиента. Выводит сообщение об ошибке "Недостаточно прав"
        /// </summary>
        public virtual RelayCommand CreateCustomerCommand
        {
            get => _createCustomerCommand ??= new RelayCommand(obj =>
            {
                new DialogError().ShowDialog("Недостаточно прав!");
            });
        }

        /// <summary>
        /// Взывает <see cref="InsufficeintPermissionsException"/> c сообщением "Недостаточно прав"
        /// </summary>
        /// <param name="customer"></param>
        public virtual void FirstNameUpdate(CustomerDTO customer)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для изменения имени клиента!");
        }

        /// <summary>
        /// Запрашивает список клиентов у сервиса
        /// </summary>
        /// <returns>Список пользователей</returns>
        public virtual IEnumerable<CustomerVM> GetCustomers()
        {
            List<CustomerVM> result = new();
            try
            {
                foreach (CustomerDTO customer in _service.GetCustomers())
                {
                    result.Add(new CustomerVM(this, customer));
                }
            }
            catch(Exception e)
            {
                DialogError dialogError = new();
                dialogError.ShowDialog($"Неизвестная ошибка: {e.Message}");
            }
            return result;
        }

        /// <summary>
        /// Взывает <see cref="Exception"/> c сообщением "Недостаточно прав"
        /// </summary>
        /// <param name="customer"></param>
        public virtual CustomerVM GetCustomer(Guid UID) 
        {
            try
            {
                CustomerDTO customer = _service.GetCustomer(UID);
                return new CustomerVM(this, customer);
            }
            catch
            {
                return new CustomerVM(this, new CustomerDTO());
            }
            
        }

        /// <summary>
        /// Взывает <see cref="InsufficeintPermissionsException"/> c сообщением "Недостаточно прав"
        /// </summary>
        /// <exception cref="InsufficeintPermissionsException"></exception>
        /// <param name="customer"></param>
        public virtual void LastNameUpdate(CustomerDTO customer)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для изменения фамилии клиента!");
        }

        /// <summary>
        /// Взывает <see cref="InsufficeintPermissionsException"/> c сообщением "Недостаточно прав"
        /// </summary>
        /// <exception cref="InsufficeintPermissionsException"></exception>
        /// <param name="customer"></param>
        public virtual void PassportUpdate(CustomerDTO customer)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для изменения пасспорта клиента!");
        }

        /// <summary>
        /// Взывает <see cref="InsufficeintPermissionsException"/> c сообщением "Недостаточно прав"
        /// </summary>
        /// <exception cref="InsufficeintPermissionsException"></exception>
        /// <param name="customer"></param>
        public virtual void PatronymicUpdate(CustomerDTO customer)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для изменения отчества клиента!");
        }

        /// <summary>
        /// Взывает <see cref="InsufficeintPermissionsException"/> c сообщением "Недостаточно прав"
        /// </summary>
        /// <exception cref="InsufficeintPermissionsException"></exception>
        /// <param name="customer"></param>
        public virtual void TelephoneUpdate(CustomerDTO customer)
        {
            throw new InsufficeintPermissionsException("Недостаточно прав для изменения телефона клиента!");
        }
    }
}
