using WpfApp1.Interfaces;
using WpfApp1.View;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Класс окна для перевода средств между счетами
    /// </summary>
    public class TransferDialog : ITransfer
    {
        private TransferMoneyWindow _window;

        public TransferDialog()
        {
            _window = new();
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
