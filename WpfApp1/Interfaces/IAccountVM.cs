using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using System;
using WpfApp1.Infrastructure;

namespace WpfApp1.Interfaces
{
    /// <summary>
    /// Интерфейс модели представления счета
    /// </summary>
    /// <typeparam name="T">Тип сервиса для пополения и снятия средств</typeparam>
    /// <typeparam name="K">Тип модели для передачи данных о счете</typeparam>
    public interface IAccountVM<out T, out K>
        where T : IPutAndWithdrawMoney<K>
        where K : BaseAccountDTO
    {
        /// <summary>
        /// Идентификатор клиента, владельца счета
        /// </summary>
        Guid UIDCustmer { get; }
        /// <summary>
        /// Модели для передачи данных о счете
        /// </summary>
        K BaseModel { get; }
        /// <summary>
        /// Название счета
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Название типа счета
        /// </summary>
        string NameTypeAccount { get; }
        /// <summary>
        /// Остаток на счете
        /// </summary>
        decimal CountMonetaryUnit { get; set; }
        /// <summary>
        /// Команда для пополнения счета
        /// </summary>
        RelayCommand PutMoneyAccountCommand { get;}
        /// <summary>
        /// Сервис для упраления снятием и пополнением средств 
        /// </summary>
        T PutAndWithdrawService { get; }
        /// <summary>
        /// Обновляет данные о модели представления
        /// </summary>
        void UpdateProperty();
    }
}
