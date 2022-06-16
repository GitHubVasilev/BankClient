using WpfApp1.ViewModel.Base;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// Модель представления для пополнения счета
    /// </summary>
    public class InputMoneyVM : ValidationBaseViewModel
    {
        private int _inputMoney;
        /// <summary>
        /// Сумма пополнения
        /// </summary>
        public int InputMoney 
        {
            get => _inputMoney;
            set => Set(ref _inputMoney, value, nameof(InputMoney));
        }
    }
}
