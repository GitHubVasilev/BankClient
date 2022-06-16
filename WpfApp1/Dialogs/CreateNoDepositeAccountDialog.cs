using WpfApp1.Interfaces;
using WpfApp1.View;
using WpfApp1.ViewModel.Accounts;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Класс диалового окна для создания недепозитного счета
    /// </summary>
    public class CreateNoDepositeAccountDialog : ICreateAccountDialog<NoDepositeAccountVM>
    {
        public CreateNoDepositeAccountWindow _window;

        public CreateNoDepositeAccountDialog()
        {
            _window = new();
        }

        /// <summary>
        /// Данные для создания недепозитного счета
        /// </summary>
        public NoDepositeAccountVM AccountVM => _window.DataContext as NoDepositeAccountVM;

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
