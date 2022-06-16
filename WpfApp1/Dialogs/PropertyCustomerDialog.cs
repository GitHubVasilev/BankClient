using WpfApp1.Interfaces;
using WpfApp1.View;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Класс окна свойств о клиенте
    /// </summary>
    public class PropertyCustomerDialog : IPropertyCustomerDialog
    {
        private PropertyCustomerWindow _window;

        public PropertyCustomerDialog()
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
