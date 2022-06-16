namespace WpfApp1.Interfaces
{
    /// <summary>
    /// Интерфейс диалогового окна для ввода средств
    /// </summary>
    public interface IInputMoneyDialog : IBaseDialog
    {
        /// <summary>
        /// </summary>
        /// <returns>Введенная сумма</returns>
        int CountMoney();
    }
}
