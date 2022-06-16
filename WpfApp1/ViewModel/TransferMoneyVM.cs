using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using BusinessLogicLayer.Services.AccountServices;
using LoggerLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WpfApp1.Dialogs;
using WpfApp1.Infrastructure;
using WpfApp1.Interfaces;
using WpfApp1.ViewModel.Accounts;
using WpfApp1.ViewModel.Base;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// Модель представления для перевода средств между счетами клиентов
    /// </summary>
    public class TransferMoneyVM : ValidationBaseViewModel
    {
        private CustomerVM _customer;

        public TransferMoneyVM(CustomerVM customerVM, IEnumerable<IAccountVM<IPutAndWithdrawMoney<BaseAccountDTO>, BaseAccountDTO>> listAllAccounts)
        {
            ListAllAccounts = listAllAccounts;
            ListAccountsForCustomer = listAllAccounts.Where(m => m.UIDCustmer == customerVM.UID);
            _customer = customerVM;
            OnPropertyChanged(nameof(SumTransfer));
        }

        /// <summary>
        /// Список всех счетов
        /// </summary>
        public IEnumerable<IAccountVM<IPutAndWithdrawMoney<BaseAccountDTO>, BaseAccountDTO>> ListAllAccounts { get; }
        /// <summary>
        /// Список счетов выбранного слиента
        /// </summary>
        public IEnumerable<IAccountVM<IPutAndWithdrawMoney<BaseAccountDTO>, BaseAccountDTO>> ListAccountsForCustomer { get; }

        private IAccountVM<IPutAndWithdrawMoney<BaseAccountDTO>, BaseAccountDTO> _fromAccount;
        /// <summary>
        /// Счет для снятия средств
        /// </summary>
        public IAccountVM<IPutAndWithdrawMoney<BaseAccountDTO>, BaseAccountDTO> FromAccount 
        {
            get => _fromAccount;
            set => Set(ref _fromAccount, value, nameof(FromAccount));
        }

        private IAccountVM<IPutAndWithdrawMoney<BaseAccountDTO>, BaseAccountDTO> _toAccount;
        /// <summary>
        /// Счет для пополнения
        /// </summary>
        public IAccountVM<IPutAndWithdrawMoney<BaseAccountDTO>, BaseAccountDTO> ToAccount 
        {
            get => _toAccount;
            set 
            {
                _toAccount = value;
                OnPropertyChanged(nameof(ToAccount));
            }
        }

        private decimal _sumTransfer;

        /// <summary>
        /// Сумма перевода
        /// </summary>
        [Range(1, 100000)]
        public decimal SumTransfer 
        {
            get => _sumTransfer;
            set => Set(ref _sumTransfer, value, nameof(SumTransfer));
        }

        private RelayCommand _transfer;
#nullable enable
        /// <summary>
        /// Команда для выполнения перевода
        /// </summary>
        public RelayCommand TransferMoney
        {
            get => _transfer ??= new RelayCommand(obj =>
                {
                    try
                    {
                        Transaction(FromAccount.PutAndWithdrawService, ToAccount.PutAndWithdrawService);
                    }
                    catch (Exception e) 
                    {
                        IDialogError dialog = new DialogError();
                        dialog.ShowDialog(e.Message);
                    }
                }, _ => FromAccount != null && ToAccount != null);
        }

        private void Transaction<T, K>(IPutAndWithdrawMoney<T> serviceT, IPutAndWithdrawMoney<K> serviceK)
            where T : BaseAccountDTO
            where K : BaseAccountDTO
        {
            ITransaction<T, K> _transaction = new Transaction<T, K>(serviceT, serviceK);
            try
            {
                T fromAccount = FromAccount.BaseModel as T ?? throw new Exception("Счет отправителя не найдет");
                K toAccount = ToAccount.BaseModel as K ?? throw new Exception("Счет получателя не найдет");
                ResultTransactionDTO result = _transaction.ToTransaction(fromAccount, toAccount, SumTransfer);
                FromAccount.CountMonetaryUnit = result.FromAccount.CountMonetaryUnit;
                ToAccount.CountMonetaryUnit = result.ToAccount.CountMonetaryUnit;
                _customer.CurrentWorker.Logger.WriteLog(_customer.CurrentWorker.Name, "Перевод денежных средств");
            }
            catch (Exception e)
            {
                IDialogError dialog = new DialogError();
                dialog.ShowDialog(e.Message);
            }
        }
#nullable restore
    }
}
