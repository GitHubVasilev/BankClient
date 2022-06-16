namespace BusinessLogicLayer.DTO.Accounts
{
    /// <summary>
    /// Объект для передачи данных депозитного счета
    /// </summary>
    public sealed record DepositeAccountDTO : NoDepositeAccountDTO
    {
        /// <summary>
        /// Процент по вкладу
        /// </summary>
        public int Procent { get; init; }
    }
}
