using AutoMapper;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using LoggerLayer.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using WpfApp1.Dialogs;
using WpfApp1.Infrastructure;
using WpfApp1.Infrastructure.MapperConfiguration;
using WpfApp1.Interfaces;
using WpfApp1.ViewModel.Base;

namespace WpfApp1.ViewModel.Accounts
{
    /// <summary>
    /// Абстрактный класс модели представления счета
    /// </summary>
    /// <typeparam name="K">Тип модели для передачи данных о счете</typeparam>
    /// <typeparam name="T">Тип сервиса для снятия и пополнения счетов</typeparam>
    public abstract class AbstractAccountVM<T, K> : ValidationBaseViewModel, IAccountVM<T, K>
        where K : BaseAccountDTO, new()
        where T : IPutAndWithdrawMoney<K>
    {
        protected readonly IDialogError dialogError;
        protected bool _isNull;
        protected ILoggerService _logger;
        protected IWorkerVM _worker;

        public AbstractAccountVM()
        {
            UpdateProperty();
            dialogError = new DialogError();
        }


        public AbstractAccountVM(K dto, IWorkerVM worker, ILoggerService loggerService)
        {
            _worker = worker;
            _logger = loggerService;
            _isNull = dto == null;
            dialogError = new DialogError();
            K _dto = dto ?? new K();

            MapperConfiguration mapperConfiguration = new(prof =>
            {
                prof.AddProfile<MapperConfigurationToAccountVM>();
            });
            Mapper _mapper = new(mapperConfiguration);
            _mapper.Map(_dto, this, typeof(K), this.GetType());
        }

        /// <summary>
        /// Сервиса для снятия и пополнения счетов
        /// </summary>
        public T PutAndWithdrawService { get; protected set; }

        /// <summary>
        /// Модель для передачи данных о счете
        /// </summary>
        public K BaseModel 
        {
            get
            {  
                if (!_isNull) 
                {
                    MapperConfiguration mapperConfiguration = new(prof =>
                    {
                        prof.AddProfile<MapperConfigurationToAccountDTO>();
                    });
                    Mapper _mapper = new(mapperConfiguration);
                    var t = (K)_mapper.Map(this, new K(), this.GetType(), typeof(K));
                    return t;
                }
                return default;
            }
        }

        /// <summary>
        /// Название типа счета
        /// </summary>
        public abstract string NameTypeAccount { get; }

        protected Guid _uid;

        /// <summary>
        /// Идентификатор счета
        /// </summary>
        public Guid UID
        {
            get => _uid;
            set => Set(ref _uid, value, nameof(UID));
        }

        protected Guid _UIDCustomer;
        /// <summary>
        /// Идентификатор клиента, который владеет счетом
        /// </summary>
        public Guid UIDCustmer
        {
            get => _UIDCustomer;
            set => Set(ref _UIDCustomer, value, nameof(UIDCustmer));
        }

        protected string _name;

        /// <summary>
        /// Название счета
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Name
        {
            get => _name;
            set => Set(ref _name, value, nameof(Name));
        }

        protected DateTime _dateOpen;
        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public DateTime DateOpen
        {
            get => _dateOpen;
            set => Set(ref _dateOpen, value, nameof(DateOpen));
        }

        protected decimal _countMonetaryUnit;
        /// <summary>
        /// Остаток средств на счету
        /// </summary>
        public decimal CountMonetaryUnit
        {
            get => _countMonetaryUnit;
            set => Set(ref _countMonetaryUnit, value, nameof(CountMonetaryUnit));
        }

        protected bool _isLook;
        /// <summary>
        /// Определяет закрытый ли счет
        /// </summary>
        public bool IsLook
        {
            get => _isLook;
            set => Set(ref _isLook, value, nameof(IsLook));
        }

        protected RelayCommand _putMoneyAccountCommand;

        /// <summary>
        /// Команда для пополнения счета
        /// </summary>
        public RelayCommand PutMoneyAccountCommand
        {
            get => _putMoneyAccountCommand ??= new RelayCommand(obj =>
            {
                IInputMoneyDialog dialog = new InputMoneyDialog();
                dialog.ShowDialog();
                if (dialog.ResultDialog())
                {
                    try
                    {
                        CountMonetaryUnit = PutAndWithdrawService.Put(BaseModel.UID, dialog.CountMoney()).CountMonetaryUnit;
                        _logger.WriteLog(_worker.Name, "Пополнение счета");
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        dialogError.ShowDialog(e.Message);
                    }
                    catch (Exception e)
                    {
                        dialogError.ShowDialog($"Неизвестная ошибка: {e.Message}");
                    }
                }
            }, _ => UID != new Guid());
        }

        /// <summary>
        /// Обновляет данные модели представления
        /// </summary>
        public virtual void UpdateProperty()
        {
            OnPropertyChanged(nameof(UID));
            OnPropertyChanged(nameof(UIDCustmer));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(DateOpen));
            OnPropertyChanged(nameof(CountMonetaryUnit));
            OnPropertyChanged(nameof(IsLook));
        }
    }
}
