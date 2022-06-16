using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using LoggerLayer.Interfaces;
using WpfApp1.Interfaces;

namespace WpfApp1.ViewModel.Accounts
{
    /// <summary>
    /// Класс модели представления депозитного счета
    /// </summary>
    public class DepositeAccountVM : AbstractAccountVM<IPutAndWithdrawMoney<DepositeAccountDTO>, DepositeAccountDTO>
    {
        public DepositeAccountVM() : base()
        {
            OnPropertyChanged(nameof(Procent));
        }

        public DepositeAccountVM(DepositeAccountDTO dto, IWorkerVM worker, ILoggerService logger) : base(dto, worker, logger)
        {
            PutAndWithdrawService = worker.DepositeAccountService;
        }


        /// <summary>
        /// Название счета
        /// </summary>
        public override string NameTypeAccount => "Депозитный счет";

        protected int _procent;

        /// <summary>
        /// Процентная ставка по счету
        /// </summary>
        public int Procent 
        {
            get => _procent;
            set => Set(ref _procent, value, nameof(Procent));
        }
    }
}
