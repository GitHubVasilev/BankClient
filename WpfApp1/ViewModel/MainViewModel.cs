using BusinessLogicLayer.Interfaces;
using LoggerLayer.DTO;
using LoggerLayer.Interfaces;
using System.Collections.Generic;
using WpfApp1.Dialogs;
using WpfApp1.Infrastructure;
using WpfApp1.Interfaces;
using WpfApp1.ViewModel.Base;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// Модель представления данных для главного окна
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private ILoggerService _loggerService;

        public MainViewModel(IConsultant consultant, IManager manager, ILoggerService logger)
        {
            Workers = new List<IWorkerVM>()
            {
                new ManagerVM(manager) { Logger = logger },
                new ConsultantVM(consultant) { Logger = logger }
            };
            _loggerService = logger;
            _loggerService.EventAddRecordLog += OnPropertyChangeLastLog;
        }

        /// <summary>
        /// Список, доступных к выбору, сотрудников
        /// </summary>
        public IEnumerable<IWorkerVM> Workers { get; set; }

        private CustomerVM _selectedCustomer;
        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public CustomerVM SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        private IEnumerable<CustomerVM> _customersVM;

        /// <summary>
        /// Список моделей представления для изменения данных о клиенте
        /// </summary>
        public IEnumerable<CustomerVM> CustomersVM
        {
            get => _customersVM;
            set => Set(ref _customersVM, value, nameof(CustomersVM));
        }

        private IWorkerVM _selectedWorker;
        /// <summary>
        /// Выбранный сотрудник из списка <see cref="Workers"/>
        /// Во время установки нового значения изменяется список клиентов <see cref="CustomersVM"/>
        /// </summary>
        public IWorkerVM SelectedWorker 
        {
            get => _selectedWorker ?? new BaseWorkerVM();
            set 
            {
                _selectedWorker = value;
                CustomersVM = _selectedWorker.GetCustomers();
            }
        }

        private LogVM _lastLog;

        public LogVM LastLog 
        {
            get => _lastLog;
            set 
            {
                _lastLog = value;
                OnPropertyChanged(nameof(LastLog));
            }
        }

        private RelayCommand _createCustomers;

        /// <summary>
        /// Команда для добавления нового сотрудника.
        /// Команда не активна когда свойство <see cref="SelectedWorker"/> == False,
        /// или тип свойства <see cref="SelectedWorker"/> == <see cref="ConsultantVM"/>
        /// </summary>
        public RelayCommand CreateCustomer
        {
            get => _createCustomers ??= new RelayCommand(obj => 
            {
                CreateCustomerDialog createClientDialog = new();
                createClientDialog.OpenWindow();
                if (createClientDialog.ResultDialog)
                {
                    CustomersVM = _selectedWorker.GetCustomers();
                }
            }, _ => SelectedWorker.GetType() != typeof(BaseWorkerVM) && SelectedWorker.GetType() == typeof(ManagerVM));
        }

        private RelayCommand _openWindowPropertyCustomer;

        /// <summary>
        /// Команда для открытия окна свойств клиента
        /// Команда не активна когда свойство <see cref="SelectedWorker"/> == False
        /// </summary>
        public RelayCommand OpenWindowPropertyCustomer 
        {
            get => _openWindowPropertyCustomer ??= new RelayCommand(obj =>
             {
                 IPropertyCustomerDialog dialog = new PropertyCustomerDialog();
                 dialog.ShowDialog();
             }, _ => SelectedWorker.GetType() != typeof(BaseWorkerVM));
        }

        private RelayCommand _openWindowLogs;

        /// <summary>
        /// Команда открывает окно со списком логов
        /// </summary>
        public RelayCommand OpenWindowLogs 
        {
            get => _openWindowLogs ??= new RelayCommand(obj =>
            {
                ILogsDialog logsDialog = new LogsDialog();
                logsDialog.ShowDialog();
            });
        }

        private void OnPropertyChangeLastLog(object sender, LoggerEventArgs args) 
        {
            LastLog = new LogVM(args);
        }
    }
}
