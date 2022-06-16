using BusinessLogicLayer.DTO.Accounts;

namespace BusinessLogicLayer.DTO
{
    /// <summary>
    /// Результат выполнения перевода средств между клиентами
    /// </summary>
    public record ResultTransactionDTO
    {
        /// <summary>
        /// Информация о счете списания
        /// </summary>
        public BaseAccountDTO FromAccount { get; init; }
        /// <summary>
        /// Информация о счете пополнения
        /// </summary>
        public BaseAccountDTO ToAccount { get; init; }
    }
}
