using AutoMapper;
using BusinessLogicLayer.DTO.Accounts;
using WpfApp1.Infrastructure.Converters;
using WpfApp1.ViewModel.Accounts;

namespace WpfApp1.Infrastructure.MapperConfiguration
{
    /// <summary> 
    /// Конфигурационный класс для automapper. Для преобразования <see cref="DepositeAccountVM"/> в <see cref="DepositeAccountDTO"/>
    /// </summary>
    public class MapperConfigurationToAccountDTO : Profile
    {
        public MapperConfigurationToAccountDTO()
        {
            CreateMap<DepositeAccountVM, DepositeAccountDTO>()
                .ConvertUsing<ConverterVMToDepositeAccountDTO>();
            CreateMap<NoDepositeAccountVM, NoDepositeAccountDTO>()
                .ConvertUsing<ConverterVMToNoDepositeAccountDTO>();
        }
    }
}
