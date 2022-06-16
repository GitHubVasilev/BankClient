using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// Реализует методы интерфейса <see cref="IWorkerVM"/> для работника Консультант
    /// </summary>
    public class ConsultantVM : BaseWorkerVM
    {
        public ConsultantVM(IConsultant consultant) : base()
        {
            _service = consultant;
            Name = "Консультант";
            DepositeAccountService = _service.DepositeAccountService;
            NoDepositeAccountService = _service.NoDepositeAccountService;
        }

        /// <summary>
        /// Обращается к модели для изменения номера телефона.
        /// </summary>
        /// <param name="customer">Обнавленные данные для передачи</param>
        public override void TelephoneUpdate(CustomerDTO customer)
        {
            _service.UpdataCustomer(customer, FieldChanged.Telephone);
        }
    }
}