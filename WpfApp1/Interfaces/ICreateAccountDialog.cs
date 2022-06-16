namespace WpfApp1.Interfaces
{
    /// <summary>
    /// Интерфейс диалогового окна для создания счета
    /// </summary>
    /// <typeparam name="T">Тип счета</typeparam>
    public interface ICreateAccountDialog<out T> : IBaseDialog
    {
        /// <summary>
        /// Полученные данные о счете 
        /// </summary>
        T AccountVM { get; }
    }
}
