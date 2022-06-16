using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using BusinessLogicLayer.Services.AccountServices;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Ninject.Modules;

namespace BusinessLogicLayer.Infrastructure
{
    /// <summary>
    /// Добавление внедрение зависимости
    /// </summary>
    public class ServiceModule : NinjectModule
    {
        /// <summary>
        /// Сопоставляет интерфейс с конкретной реализацией 
        /// </summary>
        public override void Load()
        {
            Bind<IUnitOfWork>().To<FileUnitOfWork>().InSingletonScope();
            Bind<IAccountHeandler<NoDepositeAccountDTO>>().To<NoDepositeAccountService>().InSingletonScope();
            Bind<IAccountHeandler<DepositeAccountDTO>>().To<DepositeAccountService>().InSingletonScope();
            Bind<IPutAndWithdrawMoney<NoDepositeAccountDTO>>().To<NoDepositeAccountService>().InSingletonScope();
            Bind<IPutAndWithdrawMoney<DepositeAccountDTO>>().To<DepositeAccountService>().InSingletonScope();
        }
    }
}
