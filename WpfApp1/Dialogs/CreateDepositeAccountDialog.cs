using WpfApp1.Interfaces;
using WpfApp1.View;
using WpfApp1.ViewModel.Accounts;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Класс диалового окна для создания депозитного счета
    /// </summary>
    public class CreateDepositeAccountDialog : ICreateAccountDialog<DepositeAccountVM>
    {
        private CreateDepositeAccountWindow _window;

        public CreateDepositeAccountDialog()
        {
            _window = new();
        }

        /// <summary>
        /// Данные для создания депозитного счета
        /// </summary>
        public DepositeAccountVM AccountVM => _window.DataContext as DepositeAccountVM;

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
