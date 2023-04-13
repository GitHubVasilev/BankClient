using BusinessLogicLayer.DTO.Accounts;
using BusinessLogicLayer.Interfaces.Accounts;
using BusinessLogicLayer.Services.AccountServices;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer.Infrastructure
{
    public static class ServiceModulesExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IAccountHeandler<NoDepositeAccountDTO>, NoDepositeAccountService>();
            services.AddScoped<IAccountHeandler<DepositeAccountDTO>, DepositeAccountService>();
            services.AddScoped<IPutAndWithdrawMoney<NoDepositeAccountDTO>, NoDepositeAccountService>();
            services.AddScoped<IPutAndWithdrawMoney<DepositeAccountDTO>, DepositeAccountService>();


            return services;
        }
    }
}
