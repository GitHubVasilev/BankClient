using DataAccessLayer.DataAccess;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Интерфейс взаимодействия со списком Клиентов в файле
    /// </summary>
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly FileContext context;

        public CustomerRepository(FileContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Добавляет запись нового клиента в файл
        /// </summary>
        /// <param name="item">Модель с данными нового клиента</param>
        public void Create(Customer item)
        {
            List<Customer> customers = context.Customers;
            customers.Add(item);
            context.Customers = customers;
        }

        /// <summary>
        /// Ищет модели клиентов, которые удовлетворяют условию
        /// </summary>
        /// <param name="predicate">функция для фильрации</param>
        /// <returns></returns>
        public IEnumerable<Customer> Find(Func<Customer, bool> predicate)
        {
            return context.Customers.Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список моделей клиентов, которые записаны в файл</returns>
        public IEnumerable<Customer> GetAll()
        {
            return context.Customers;
        }

        /// <summary>
        /// Обнавляет запись о клиенте в файле
        /// </summary>
        /// <param name="item">обнавленная запись о клиенте</param>
        public void Updata(Customer item)
        {
            List<Customer> customers = context.Customers;
            customers[customers.FindIndex(m => m.UID == item.UID)] = item;
            context.Customers = customers;
        }
    }
}
