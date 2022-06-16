using BusinessLogicLayer.DTO;
using LoggerLayer.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using WpfApp1.Dialogs;
using WpfApp1.Infrastructure;
using WpfApp1.Interfaces;
using WpfApp1.ViewModel.Accounts;
using WpfApp1.ViewModel.Base;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// Содержит свойства, для изменения данных в источнике и вывод уже исеющихся значений
    /// </summary>
    public class CustomerVM : ValidationBaseViewModel
    {
        private readonly IDialogError dialogError;
        private readonly ILoggerService _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentWorker">Работник, который содержит методы для изменения данных клиента</param>
        /// <param name="baseModel">Модель представления данных клиента</param>
        public CustomerVM(IWorkerVM currentWorker, CustomerDTO baseModel)
        {
            CurrentWorker = currentWorker;
            dialogError = new DialogError();
            _logger = currentWorker.Logger;
            UpdateData(baseModel);
            UpdateModel();
        }

        /// <summary>
        /// Оповещает слушателей об изменении значений свойств 
        /// </summary>
        private void UpdateModel()
        {
            OnPropertyChanged(nameof(UID));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Patronymic));
            OnPropertyChanged(nameof(Telephone));
            OnPropertyChanged(nameof(Passport));
            OnPropertyChanged(nameof(FieldChanged));
            OnPropertyChanged(nameof(TypeChanged));
            OnPropertyChanged(nameof(DateChange));
            OnPropertyChanged(nameof(ChangingWorker));
            OnPropertyChanged(nameof(DepositeAccount));
            OnPropertyChanged(nameof(NoDepositeAccount));
        }

        private void UpdateData(CustomerDTO baseModel) 
        {
            UID = baseModel.UID;
            _firstName = baseModel.FirstName ?? "";
            _lastName = baseModel.LastName ?? "";
            _patronymic = baseModel.Patronymic ?? "";
            _telephone = baseModel.Telephone ?? "";
            _passport = baseModel.Passport ?? "";
            _dateChange = new DateTime(baseModel.DateChange);
            _fieldChanged = baseModel.FieldChanged;
            _typeChanged = baseModel.TypeChanged;
            _changingWorker = baseModel.ChangingWorker ?? "";
            _depositeAccount = new(baseModel.DepositeAccount, CurrentWorker, _logger);
            _noDepositeAccount = new(baseModel.NoDepositeAccount, CurrentWorker, _logger);
        }

        public CustomerDTO BaseModel => new()
        {
            UID = UID,
            FirstName = FirstName,
            LastName = LastName,
            Patronymic = Patronymic,
            Telephone = Telephone,
            Passport = Passport
        };

        public IWorkerVM CurrentWorker { get; }

        private Guid _uid;
        /// <summary>
        /// Содержит идентификационный номер клиента.
        /// При установке значения данные изменятся в источнике данных.
        /// Если получаемые данные не корректные будет выведено диалогове окно с сообщением об ошибке.
        /// </summary>
        public Guid UID 
        {
            get => _uid;
            set => Set(ref _uid, value, nameof(UID));
        }


        private string _firstName;
        /// <summary>
        /// Содержит имя клиента.
        /// При установке значения данные изменятся в источнике данных.
        /// Если получаемые данные не корректные будет выведено диалогове окно с сообщением об ошибке.
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string FirstName
        {
            get => _firstName;
            set 
            {
                string oldValue = _firstName.Clone() as string;
                _firstName = value;
                try
                {
                    CustomerDTO baseModel = BaseModel;
                    CurrentWorker.FirstNameUpdate(baseModel);
                    _logger.WriteLog(CurrentWorker.Name, $"Обновление имени клиента на: {value}");
                    UpdateModel();
                }
                catch (Exception e) 
                {
                    FirstName = oldValue;
                    dialogError.ShowDialog(e.Message); 
                }
            }
        }

        private string _lastName;
        /// <summary>
        /// Содержит фамилию клиента.
        /// При установке значения данные изменятся в источнике данных.
        /// Если получаемые данные не корректные будет выведено диалогове окно с сообщением об ошибке.
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string LastName
        {
            get => _lastName;
            set
            {
                string oldValue = _lastName.Clone() as string;
                _lastName = value;
                try
                {
                    CustomerDTO baseModel = BaseModel;
                    CurrentWorker.LastNameUpdate(baseModel);
                    _logger.WriteLog(CurrentWorker.Name, $"Обновление фамилии клиента на: {value}");
                    UpdateModel();
                }
                catch (Exception e) 
                {
                    _lastName = oldValue;
                    dialogError.ShowDialog(e.Message); 
                }
            }
        }

        private string _patronymic;
        /// <summary>
        /// Содержит отчество клиента.
        /// При установке значения данные изменятся в источнике данных.
        /// Если получаемые данные не корректные будет выведено диалогове окно с сообщением об ошибке.
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                string oldValue = _patronymic.Clone() as string;
                _patronymic = value;
                try
                {
                    CustomerDTO baseModel = BaseModel;
                    CurrentWorker.PatronymicUpdate(baseModel);
                    _logger.WriteLog(CurrentWorker.Name, $"Обновление отвества клиента на: {value}");
                    UpdateModel();
                }
                catch (Exception e) 
                {
                    _patronymic = oldValue;
                    dialogError.ShowDialog(e.Message);
                }
            }
        }

        private string _telephone;
        /// <summary>
        /// Содержит телефон клиента.
        /// При установке значения данные изменятся в источнике данных.
        /// Если получаемые данные не корректные будет выведено диалогове окно с сообщением об ошибке.
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Telephone
        {
            get => _telephone;
            set
            {
                string oldValue = _telephone.Clone() as string;
                _telephone = value;
                try
                {
                    CustomerDTO baseModel = BaseModel;
                    CurrentWorker.TelephoneUpdate(baseModel);
                    _logger.WriteLog(CurrentWorker.Name, $"Обновление телефона клиента на: {value}");
                    UpdateModel();
                }
                catch (Exception e) 
                {
                    _telephone = oldValue;
                    dialogError.ShowDialog(e.Message);
                }
            }
        }

        private string _passport;
        /// <summary>
        /// Содержит данные паспорта клиента.
        /// При установке значения данные изменятся в источнике данных.
        /// Если получаемые данные не корректные будет выведено диалогове окно с сообщением об ошибке.
        /// </summary>
        public string Passport
        {
            get => _passport;
            set
            {
                string oldValue = _passport.Clone() as string;
                _passport = value;
                try
                {
                    CustomerDTO baseModel = BaseModel;
                    CurrentWorker.PassportUpdate(baseModel);
                    _logger.WriteLog(CurrentWorker.Name, $"Обновление паспорта клиента на: {value}");
                    UpdateModel();
                }
                catch (Exception e) 
                {
                    _passport = oldValue;
                    dialogError.ShowDialog(e.Message);
                }
            }
        }

        private DateTime _dateChange;

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime DateChange
        {
            get => _dateChange;
            set => Set(ref _dateChange, value, nameof(DateChange));
        }

        private FieldChanged _fieldChanged;

        /// <summary>
        /// Последние изменное поле
        /// </summary>
        public FieldChanged FieldChanged
        {
            get => _fieldChanged;
            set => Set(ref _fieldChanged, value, nameof(FieldChanged));
        }

        private TypeChanged _typeChanged;

        /// <summary>
        /// Тип последнего изменения
        /// </summary>
        public TypeChanged TypeChanged
        {
            get => _typeChanged;
            set => Set(ref _typeChanged, value, nameof(TypeChanged));
        }

        private string _changingWorker;

        /// <summary>
        /// Название сотрудника, который сделал последние изменение
        /// </summary>
        public string ChangingWorker
        {
            get => _changingWorker;
            set => Set(ref _changingWorker, value, nameof(ChangingWorker));
        }

        private DepositeAccountVM _depositeAccount;
        /// <summary>
        /// Депозитный счет
        /// </summary>
        public DepositeAccountVM DepositeAccount 
        {
            get => _depositeAccount;
            set 
            {
                _depositeAccount = value;
                OnPropertyChanged(nameof(DepositeAccount));
            }
        }

        private NoDepositeAccountVM _noDepositeAccount;
        /// <summary>
        /// Недепозитном счет
        /// </summary>
        public NoDepositeAccountVM NoDepositeAccount
        {
            get => _noDepositeAccount;
            set 
            {
                _noDepositeAccount = value;
                OnPropertyChanged(nameof(NoDepositeAccount));
            }
        }

        private RelayCommand _createDepositeAccountCommand;
        /// <summary>
        /// Команда для создания депозитного счета
        /// </summary>
        public RelayCommand CreateDepositeAccountCommand
        {
            get => _createDepositeAccountCommand ??= new RelayCommand(obj =>
            {
                ICreateAccountDialog<DepositeAccountVM> dialog = new CreateDepositeAccountDialog();
                dialog.ShowDialog();
                if (dialog.ResultDialog()) 
                {
                    try
                    {
                        dialog.AccountVM.UIDCustmer = UID;
                        CurrentWorker.DepositeAccountService.OpenAccount(dialog.AccountVM.BaseModel);
                        DepositeAccount = new(CurrentWorker.DepositeAccountService.GetAccountForCustomer(UID), CurrentWorker, _logger);
                        _logger.WriteLog(CurrentWorker.Name, $"Открытие нового депозитного счета: {DepositeAccount.Name}");
                        UpdateModel();
                    }
                    catch (Exception e)
                    {
                        dialogError.ShowDialog(e.Message);
                    }
                }
            }, _ => DepositeAccount.UID == new Guid());
        }

        private RelayCommand _createNoDepositeAccountCommand;
        /// <summary>
        /// Команда для создания недепозитного счета
        /// </summary>
        public RelayCommand CreateNoDepositeAccountCommand
        {
            get => _createNoDepositeAccountCommand ??= new RelayCommand(obj =>
            {
                ICreateAccountDialog<NoDepositeAccountVM> dialog = new CreateNoDepositeAccountDialog();
                dialog.ShowDialog();
                if (dialog.ResultDialog())
                {
                    try
                    {
                        dialog.AccountVM.UIDCustmer = UID;
                        CurrentWorker.NoDepositeAccountService.OpenAccount(dialog.AccountVM.BaseModel);
                        NoDepositeAccount = new(CurrentWorker.NoDepositeAccountService.GetAccountForCustomer(UID), CurrentWorker, _logger);
                        _logger.WriteLog(CurrentWorker.Name, $"Открытие нового недепозитного счета: {NoDepositeAccount.Name}");
                        UpdateModel();
                    }
                    catch (Exception e)
                    {
                        dialogError.ShowDialog(e.Message);
                    }
                }
            }, _ => NoDepositeAccount.UID == new Guid());
        }

        private RelayCommand _closeDepositeAccountCommand;
        /// <summary>
        /// Команда для закрытия депозитного счета
        /// </summary>
        public RelayCommand CloseDepositeAccountCommand 
        {
            get => _closeDepositeAccountCommand ??= new RelayCommand(obj =>
              {
                  MessageBoxResult resultDialog = MessageBox.Show("Вы действительно ходите закрыть счет", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                  if (resultDialog == MessageBoxResult.Yes) 
                  {
                      string nameAccount = DepositeAccount.Name;
                      CurrentWorker.DepositeAccountService.CloseAccount(DepositeAccount.BaseModel);
                      DepositeAccount = new(CurrentWorker.DepositeAccountService.GetAccountForCustomer(UID), CurrentWorker, _logger);
                      _logger.WriteLog(CurrentWorker.Name, $"Закрытие депозитного счета: {nameAccount}");
                      UpdateModel();
                  }
              }, _ => DepositeAccount.UID != new Guid());
        }

        private RelayCommand _closeNoDepositeAccountCommand;
        /// <summary>
        /// Команда для закрытия недепозитного счета
        /// </summary>
        public RelayCommand CloseNoDepositeAccountCommand
        {
            get => _closeNoDepositeAccountCommand ??= new RelayCommand(obj =>
            {
                MessageBoxResult resultDialog = MessageBox.Show("Вы действительно ходите закрыть счет", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                if (resultDialog == MessageBoxResult.Yes)
                {
                    string nameAccount = NoDepositeAccount.Name;
                    CurrentWorker.NoDepositeAccountService.CloseAccount(NoDepositeAccount.BaseModel);
                    NoDepositeAccount = new(CurrentWorker.NoDepositeAccountService.GetAccountForCustomer(UID), CurrentWorker, _logger);
                    _logger.WriteLog(CurrentWorker.Name, $"Закрытие недепозитного счета: {nameAccount}");
                    UpdateModel();
                }
                
            }, _ => NoDepositeAccount.UID != new Guid());
        }

        private RelayCommand _transferMoney;
        /// <summary>
        /// Команда для перевода средств между счетами
        /// </summary>
        public RelayCommand TransferMoneyCommand
        {
            get => _transferMoney ??= new RelayCommand(obj =>
            {
                ITransfer dialog = new TransferDialog();
                dialog.ShowDialog();
            });
        }
    }
}
