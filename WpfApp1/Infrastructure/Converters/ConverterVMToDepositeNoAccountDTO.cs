using AutoMapper;
using BusinessLogicLayer.DTO.Accounts;
using System;
using WpfApp1.ViewModel.Accounts;

namespace WpfApp1.Infrastructure.Converters
{
    /// <summary>
    /// Конвертер для Automapper. Конвертирует <see cref="NoDepositeAccountVM"/> в <see cref="NoDepositeAccountDTO/> 
    /// </summary>
    public class ConverterVMToNoDepositeAccountDTO : ITypeConverter<NoDepositeAccountVM, NoDepositeAccountDTO>
    {
        public NoDepositeAccountDTO Convert(NoDepositeAccountVM source, NoDepositeAccountDTO destination, ResolutionContext context)
        {
            NoDepositeAccountDTO accountDTO = new()
            {
                UID = source.UID == default ? Guid.NewGuid() : source.UID,
                Name = source.Name,
                UIDClient = source.UIDCustmer,
                DateOpen = source.DateOpen.Ticks,
                CountMonetaryUnit = source.CountMonetaryUnit,
                IsClose = false,
                IsLock = source.IsLook,
            };
            return accountDTO;
        }
    }
}
