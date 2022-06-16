using DataAccessLayer.DataAccess;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Предоставляет доступ к репозиториям 
    /// </summary>
    public class FileUnitOfWork : IUnitOfWork
    {
        private readonly FileContext _customersContext;
        private readonly FileAccountContext _accountContext;
        private CustomerRepository _clientRepository;
        private AccountRepository _accountRepository;

        public FileUnitOfWork()
        {
            _customersContext = new FileContext();
            _accountContext = new();
        }

        /// <summary>
        /// Коллекция моделей Клиентов
        /// </summary>
        public IRepository<Customer> Customers
        {
            get
            {
                if (_clientRepository == null) 
                {
                    _clientRepository = new(_customersContext);
                }
                return _clientRepository;
            }
        }

        /// <summary>
        /// Коллекция моделей Счетов
        /// </summary>
        public IRepository<Account> Accounts 
        {
            get 
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new(_accountContext);
                }
                return _accountRepository;
            }
        }

        /// <summary>
        /// не реализовано
        /// </summary>
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// не реализовано
        /// </summary>
        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
