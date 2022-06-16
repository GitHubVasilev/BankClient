using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Accounts;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Infrastructure
{
    /// <summary>
    /// Содержит методы для преобразования типов <see cref="CustomerDTO"/> и <see cref="Customer"/>
    /// </summary>
    public static class ConverterCustomer
    {
        /// <summary>
        /// Создает новый объект <see cref="CustomerDTO"/>
        /// </summary>
        /// <param name="custromer">ДТО клиента</param>
        /// <returns>Новый объект модели клиента</returns>
        public static Customer ToCustomerModel(CustomerDTO custromer)
        {
            return new Customer()
            {
                UID = custromer.UID,
                FirstName = custromer.FirstName,
                LastName = custromer.LastName,
                Patronymic = custromer.Patronymic,
                Telephone = custromer.Telephone,
                Passport = custromer.Passport,
                DateChange = custromer.DateChange,
                TypeChanged = (int)custromer.TypeChanged,
                FieldChanged = (int)custromer.FieldChanged,
                ChangingWorker = custromer.ChangingWorker
            };
        }

        /// <summary>
        /// Создает новый объект <see cref="Customer"/>
        /// </summary>
        /// <param name="model">модель с данными пользователя</param>
        /// <param name="depositeAccountDTO">Объектдля передачи данных о депозитном счете</param>
        /// <param name="noDepositeAccountDTO">Объект для передачи данных о недепозитном счете</param>
        /// <returns></returns>
        public static CustomerDTO ToCustomerDTO(Customer model, DepositeAccountDTO depositeAccountDTO, NoDepositeAccountDTO noDepositeAccountDTO)
        {
            return new CustomerDTO()
            {
                UID = model.UID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                Passport = model.Passport,
                Telephone = model.Telephone,
                DateChange = model.DateChange,
                TypeChanged = (TypeChanged)model.TypeChanged,
                FieldChanged = (FieldChanged)model.FieldChanged,
                ChangingWorker = model.ChangingWorker,
                DepositeAccount = depositeAccountDTO,
                NoDepositeAccount = noDepositeAccountDTO
            };
        }
    }
}
