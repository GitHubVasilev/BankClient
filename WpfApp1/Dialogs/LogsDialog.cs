using WpfApp1.Interfaces;
using WpfApp1.View;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Диалоговое окно, которое содержит записи о логах
    /// </summary>
    internal class LogsDialog : ILogsDialog
    {
        private LogsWindow _window;
        public LogsDialog()
        {
            _window = new();
        }

        /// <summary>
        /// Результат выполнения диалога
        /// </summary>
        /// <returns>
        ///     True - если пользователь подтвердил выполнение.
        ///     False - если пользователь просто закрыл окно
        /// </returns>
        public bool ResultDialog()
        {
            return _window.DialogResult ?? false;
        }

        /// <summary>
        /// Метод создания нового диалогового окна
        /// </summary>
        public void ShowDialog()
        {
            _window.ShowDialog();
        }
    }
}
