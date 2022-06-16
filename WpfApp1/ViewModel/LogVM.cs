using LoggerLayer.DTO;
using System;
using WpfApp1.ViewModel.Base;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// Модель представление лога
    /// </summary>
    public class LogVM : BaseViewModel
    {
        public LogVM(LoggerEventArgs log=null)
        {
            LoggerEventArgs logArgs = log ?? new LoggerEventArgs();
            NameWorker = logArgs.NameWorker ?? "";
            TypeOperation = logArgs.TypeOperation ?? "";
            DateOperation = logArgs.DateOperation;
        }

        private string _nameWorker;
        /// <summary>
        /// Имя работника
        /// </summary>
        public string NameWorker 
        {
            get => _nameWorker;
            set 
            {
                _nameWorker = value;
                OnPropertyChanged(nameof(NameWorker));
            }
        }

        private string _typeOperation;
        /// <summary>
        /// Описание операции
        /// </summary>
        public string TypeOperation 
        {
            get => _typeOperation;
            set 
            {
                _typeOperation = value;
                OnPropertyChanged(nameof(TypeOperation));
            }
        }

        private DateTime _dateOperation;
        /// <summary>
        /// Дата создания события
        /// </summary>
        public DateTime DateOperation 
        {
            get => _dateOperation;
            set
            {
                _dateOperation = value;
                OnPropertyChanged(nameof(DateOperation));
            }
        }
    }
}
