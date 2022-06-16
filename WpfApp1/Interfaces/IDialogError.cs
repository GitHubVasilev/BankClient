namespace WpfApp1.Interfaces
{
    public interface IDialogError
    {
        /// <summary>
        /// Создает новое диалоговое окно с сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        void ShowDialog(string message);
    }
}
