using WpfApp1.Interfaces;
using WpfApp1.View;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Фунционал работы с диалоговым окном создания нового клиента
    /// </summary>
    internal class CreateCustomerDialog : ICreateCustomerDialog
    {
        private readonly CreateCustomerWindow _dialog;

        public CreateCustomerDialog()
        {
            _dialog = new CreateCustomerWindow();
        }

        /// <summary>
        /// Результат работы окна. True - пользователь подтвердил добавление, False - отказался от добавления
        /// </summary>
        public bool ResultDialog => _dialog.DialogResult ?? false;

        /// <summary>
        /// Открывет новое диалоговое окно
        /// </summary>
        public void OpenWindow()
        {
            _dialog.ShowDialog();
        }
    }
}
