namespace WpfApp1.Interfaces
{
    /// <summary>
    /// Определяет диалоговое окно для создания нового пользователя
    /// </summary>
    public interface ICreateCustomerDialog
    {
        /// <summary>
        /// Результат выполнения диалога
        /// </summary>
        bool ResultDialog { get; }

        /// <summary>
        /// Метод создания нового диалогового окна
        /// </summary>
        void OpenWindow();
    }
}
