using BusinessLogicLayer.DTO;

namespace BusinessLogicLayer.Interfaces
{
    /// <summary>
    /// Интерфейс описывающий работу консультанта
    /// </summary>
    public interface IManager : IConsultant
    {
        /// <summary>
        /// Добавляет нового пользователя.
        /// </summary>
        /// <param name="customer">Модель нового клиента</param>
        void CreateCustomer(CustomerDTO customer);
    }
}
