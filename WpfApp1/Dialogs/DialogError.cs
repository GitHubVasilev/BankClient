using System.Windows;
using WpfApp1.Interfaces;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Фунционал работы с диалоговым окном ошибки
    /// </summary>
    public class DialogError : IDialogError
    {
        /// <summary>
        /// Открывает новое диалоговое окно с ошибкой
        /// </summary>
        /// <param name="message">Текст ошибки</param>
        public void ShowDialog(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
