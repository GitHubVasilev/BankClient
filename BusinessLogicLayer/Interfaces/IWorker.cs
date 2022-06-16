using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IWorker
    {
        /// <summary>
        /// </summary>
        /// <param name="customerUID">Идентификационный номер клиента</param>
        /// <returns>Возвращает найденого клиента</returns>
        CustomerDTO GetCustomer(Guid customerUID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список всех клиентов</returns>
        IEnumerable<CustomerDTO> GetCustomers();

        /// <summary>
        /// Изменяет запись о клиенте. 
        /// </summary>
        /// <param name="model">Обновленная запись клиента</param>
        /// <param name="type">Тип изменения</param>
        void UpdataCustomer(CustomerDTO model, FieldChanged type);


        IAccountHeandler<DepositeAccountDTO> DepositeAccountService { get; }
        IAccountHeandler<NoDepositeAccountDTO> NoDepositeAccountService { get; }
    }
}
