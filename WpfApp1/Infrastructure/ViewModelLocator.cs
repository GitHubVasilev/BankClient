using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces.Accounts;
using Ninject;
using Ninject.Modules;
using System.Collections.Generic;
using WpfApp1.Interfaces;
using WpfApp1.ViewModel;

namespace WpfApp1.Infrastructure
{
    /// <summary>
    /// Буфер контейнер между View и ViewModel
    /// </summary>
    public class ViewModelLocator
    {
        private readonly StandardKernel _kernal;

        public ViewModelLocator()
        {
            NinjectModule registrations = new IocConfiguration();
            NinjectModule serviceModule = new ServiceModule();
            _kernal = new StandardKernel(registrations, serviceModule);
            MainViewModel = _kernal.Get<MainViewModel>();
            LogsVM = _kernal.Get<LogsVM>();
        }

        public LogsVM LogsVM { get; }
        /// <summary>
        /// Модель предстваления главного окна
        /// </summary>
        public MainViewModel MainViewModel { get; }
        /// <summary>
        /// Модель предстваления окна создания пользователя
        /// </summary>
        public CustomerVM CustomerVM => new(MainViewModel.SelectedWorker, new BusinessLogicLayer.DTO.CustomerDTO());
        /// <summary>
        /// Модель предстваления для пополнения счета
        /// </summary>
        public InputMoneyVM InputMoneyVM => new();
        /// <summary>
        /// Модель представления перевода между счетами
        /// </summary>
        public TransferMoneyVM TransferMoneyVM => new(MainViewModel.SelectedCustomer, AllAccounts);

        /// <summary>
        /// Список всех счетов
        /// </summary>
        private IEnumerable<IAccountVM<IPutAndWithdrawMoney<BaseAccountDTO>, BaseAccountDTO>> AllAccounts 
        {
            get
            {
                List<IAccountVM<IPutAndWithdrawMoney<BaseAccountDTO>, BaseAccountDTO>> result = new();

                foreach (CustomerVM customer in MainViewModel.CustomersVM) 
                {
                    if (customer.DepositeAccount.BaseModel != null)
                    { result.Add(customer.DepositeAccount); }
                    if (customer.NoDepositeAccount.BaseModel != null)
                    { result.Add(customer.NoDepositeAccount); }
                }

                return result;
            }
        }
    }
}
