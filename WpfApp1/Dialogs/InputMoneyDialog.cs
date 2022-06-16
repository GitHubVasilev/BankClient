using WpfApp1.Interfaces;
using WpfApp1.View;
using WpfApp1.ViewModel;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Класс окна для ввода суммы пополения
    /// </summary>
    public class InputMoneyDialog : IInputMoneyDialog
    {
        private InputCountMoneyWindow _window;

        public InputMoneyDialog()
        {
            _window = new InputCountMoneyWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Количество средств введенными пользователем</returns>
        public int CountMoney()
        {
            return (_window.DataContext as InputMoneyVM).InputMoney;
        }

        /// <summary>
        /// Результат работы окна
        /// </summary>
        /// <returns>True - если пользователь подтвердил ввод. False - если пользователь отказался от ввода</returns>
        public bool ResultDialog()
        {
            return _window.DialogResult ?? false;
        }

        /// <summary>
        /// Открывает окно
        /// </summary>
        public void ShowDialog()
        {
            _window.ShowDialog();
        }
    }
}
