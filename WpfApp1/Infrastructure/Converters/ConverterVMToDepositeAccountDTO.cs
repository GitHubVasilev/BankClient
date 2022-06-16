using AutoMapper;
using BusinessLogicLayer.DTO.Accounts;
using System;
using WpfApp1.ViewModel.Accounts;

namespace WpfApp1.Infrastructure.Converters
{
    /// <summary>
    /// Конвертер для Automapper. Конвертирует <see cref="DepositeAccountVM"/> в <see cref="DepositeAccountDTO/> 
    /// </summary>
    public class ConverterVMToDepositeAccountDTO : ITypeConverter<DepositeAccountVM, DepositeAccountDTO>
    {
        public DepositeAccountDTO Convert(DepositeAccountVM source, DepositeAccountDTO destination, ResolutionContext context)
        {
            DepositeAccountDTO depositeAccountDTO = new()
            {
                UID = source.UID == default ? Guid.NewGuid() : source.UID,
                Name = source.Name,
                UIDClient = source.UIDCustmer,
                DateOpen = source.DateOpen.Ticks,
                CountMonetaryUnit = source.CountMonetaryUnit,
                IsClose = false,
                IsLock = source.IsLook,
                Procent = source.Procent
            };
            return depositeAccountDTO;
        }
    }
}
