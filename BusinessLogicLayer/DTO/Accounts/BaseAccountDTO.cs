using System;

namespace BusinessLogicLayer.DTO.Accounts
{
    /// <summary>
    /// Абстрактный объект для передачи данных о счете
    /// </summary>
    public abstract record BaseAccountDTO
    {
        /// <summary>
        /// Идентификтатор счета
        /// </summary>
        public Guid UID { get; init; }
        /// <summary>
        /// Название счета
        /// </summary>
        public string? Name { get; init; }
        /// <summary>
        /// Идентификатор клиента которому принадлежит счет
        /// </summary>
        public Guid UIDClient { get; init; }
        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public long DateOpen { get; init; }
        /// <summary>
        /// Количество валютной единицы
        /// </summary>
        public decimal CountMonetaryUnit { get; init; }
        /// <summary>
        /// Определяет можно ли производить денежные операции
        /// </summary>
        public bool IsLock { get; init; }
        /// <summary>
        /// Определяет закрыт ли счет. True - закрыт
        /// </summary>
        public bool IsClose { get; init; }
    }
}
