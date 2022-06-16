using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using System;
using WpfApp1.Infrastructure;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// Реализует методы интерфейса <see cref="IWorkerVM"/> для работника Менеджер
    /// </summary>
    public class ManagerVM : BaseWorkerVM
    {
        public ManagerVM(IManager manager) : base()
        {
            _service = manager;
            Name = "Менеджер";
            DepositeAccountService = _service.DepositeAccountService;
            NoDepositeAccountService = _service.NoDepositeAccountService;
        }

        /// <summary>
        /// Обращается к модели для изменения номера имени.
        /// </summary>
        /// <param name="customer">Обнавленные данные для передачи</param>
        public override void FirstNameUpdate(CustomerDTO customer) => _service.UpdataCustomer(customer, FieldChanged.Firstname);

        /// <summary>
        /// Обращается к модели для изменения номера фамилии.
        /// </summary>
        /// <param name="customer">Обнавленные данные для передачи</param>
        public override void LastNameUpdate(CustomerDTO customer) => _service.UpdataCustomer(customer, FieldChanged.Lastname);

        /// <summary>
        /// Обращается к модели для изменения номера отчества.
        /// </summary>
        /// <param name="customer">Обнавленные данные для передачи</param>
        public override void PatronymicUpdate(CustomerDTO customer) => _service.UpdataCustomer(customer, FieldChanged.Patronymic);

        /// <summary>
        /// Обращается к модели для изменения номера телефона.
        /// </summary>
        /// <param name="customer">Обнавленные данные для передачи</param>
        public override void TelephoneUpdate(CustomerDTO customer) => _service.UpdataCustomer(customer, FieldChanged.Telephone);

        /// <summary>
        /// Обращается к модели для изменения номера паспорта.
        /// </summary>
        /// <param name="customer">Обнавленные данные для передачи</param>
        public override void PassportUpdate(CustomerDTO customer) => _service.UpdataCustomer(customer, FieldChanged.Passport);

        /// <summary>
        /// Команда добавления нового клиента. Команда активна если:
        /// 1. ViewModel клиента не равна null
        /// 2. ViewModel клиента не содержит ошибки
        /// </summary>
        public override RelayCommand CreateCustomerCommand 
        {
            get
            {
                Func<object, bool> canExecute = obj => obj == null || !(obj as CreateCustomerVM).HasErrors;
                return _createCustomerCommand ??= new RelayCommand(obj =>
                             {
                                 try
                                 {
                                     CreateCustomerVM customer = obj as CreateCustomerVM;
                                     (_service as IManager).CreateCustomer((obj as CreateCustomerVM).BaseModel);
                                 }
                                 catch (Exception e) 
                                 {
                                     Dialogs.DialogError dialogError = new();
                                     dialogError.ShowDialog(e.Message);
                                 }
                             }, canExecute);
            }
        }
    }
}