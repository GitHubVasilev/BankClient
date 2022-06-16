using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using LoggerLayer.Interfaces;
using WpfApp1.Interfaces;

namespace WpfApp1.ViewModel.Accounts
{
    /// <summary>
    /// Класс модели представления недепозитного счета
    /// </summary>
    public class NoDepositeAccountVM : AbstractAccountVM<IPutAndWithdrawMoney<NoDepositeAccountDTO>, NoDepositeAccountDTO>
    {
        public NoDepositeAccountVM() : base()
        {
        }

        public NoDepositeAccountVM(NoDepositeAccountDTO dto, IWorkerVM worker, ILoggerService logger) : base(dto, worker, logger)
        {
            PutAndWithdrawService = worker.NoDepositeAccountService;
        }

        /// <summary>
        /// Название недепозитного счета
        /// </summary>
        public override string NameTypeAccount => "Недепозитный счет";
    }
}
