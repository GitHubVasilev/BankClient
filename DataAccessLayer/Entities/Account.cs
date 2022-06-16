using System;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Модель данных о счете
    /// </summary>
    public class Account : Entity<Guid>
    {
        /// <summary>
        /// Идентификатор клиента которому принадлежит счет
        /// </summary>
        public Guid UIDClient { get; set; }
        /// <summary>
        /// Название счета
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public long DateOpen { get; set; }
        /// <summary>
        /// Процентр
        /// </summary>
        public int Procent { get; set; }
        /// <summary>
        /// Количество валютной единицы
        /// </summary>
        public decimal CountMonetaryUnit { get; set; }
        /// <summary>
        /// Тип аккаунта
        /// </summary>
        public int TypeAccount { get; set; }
        /// <summary>
        /// Определяет можно ли производить денежные операции
        /// </summary>
        public bool IsLock { get; set; }
        /// <summary>
        /// Определяет закрыт ли счет. True - закрыт
        /// </summary>
        public bool IsClose { get; set; }
    }
}
