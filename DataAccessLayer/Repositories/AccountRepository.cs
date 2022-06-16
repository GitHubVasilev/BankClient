using DataAccessLayer.DataAccess;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Интерфейс взаимодействия со списком счетов в источнике данных
    /// </summary>
    public class AccountRepository : IRepository<Account>
    {
        private readonly FileAccountContext _context;

        public AccountRepository(FileAccountContext accountContext)
        {
            _context = accountContext;
        }

        /// <summary>
        /// Добавляет модель нового счета в список
        /// </summary>
        /// <param name="item">новый счет</param>
        public void Create(Account item)
        {
            List<Account> accounts = _context.GetAccounts();
            accounts.Add(item);
            _context.SetAccount(accounts);
        }

        /// <summary>
        /// Ищет модель счета в списке
        /// </summary>
        /// <param name="predicate">условия для поиска</param>
        /// <returns>Список найденых счетов</returns>
        public IEnumerable<Account> Find(Func<Account, bool> predicate)
        {
            return _context.GetAccounts().Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список счетов</returns>
        public IEnumerable<Account> GetAll()
        {
            IEnumerable<Account> e = _context.GetAccounts();
            return e;
        }

        /// <summary>
        /// Обновляет данные о счете
        /// </summary>
        /// <param name="item">Новые данные о счете</param>
        public void Updata(Account item)
        {
            List<Account> accounts = _context.GetAccounts();
            accounts[accounts.FindIndex(m => m.UID == item.UID)] = item;
            _context.SetAccount(accounts);
        }
    }
}
