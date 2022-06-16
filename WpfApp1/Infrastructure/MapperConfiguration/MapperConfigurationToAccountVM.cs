using AutoMapper;
using BusinessLogicLayer.DTO.Accounts;
using WpfApp1.ViewModel.Accounts;

namespace WpfApp1.Infrastructure.MapperConfiguration
{
    /// <summary> 
    /// Конфигурационный класс для automapper. Для преобразования <see cref="DepositeAccountDTO"/> в <see cref="DepositeAccountVM"/>
    /// </summary>
    public class MapperConfigurationToAccountVM : Profile
    {
        public MapperConfigurationToAccountVM()
        {
            CreateMap<DepositeAccountDTO, DepositeAccountVM>()
                .ForMember(dect => dect.Name, opt => opt.NullSubstitute(""))
                .ForMember(dect => dect.UIDCustmer, opt => opt.MapFrom(src => src.UIDClient));
            CreateMap<NoDepositeAccountDTO, NoDepositeAccountVM>()
                .ForMember(dect => dect.Name, opt => opt.NullSubstitute(""))
                .ForMember(dect => dect.UIDCustmer, opt => opt.MapFrom(src => src.UIDClient));
        }
    }
}
