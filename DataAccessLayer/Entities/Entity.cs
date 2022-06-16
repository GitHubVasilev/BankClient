namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Абстрактный класс модели
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатора</typeparam>
    public abstract class Entity<TKey>
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary>
        public TKey UID { get; set; }
    }
}
