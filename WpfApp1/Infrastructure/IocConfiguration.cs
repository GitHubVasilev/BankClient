using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using LoggerLayer.Services;
using LoggerLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Infrastructure;
using BusinessLogicLayer.Infrastructure;
using WpfApp1.ViewModel;

namespace WpfApp1.Infrastructure
{
    /// <summary>
    /// Добавление внедрение зависимости
    /// </summary>
    internal class IocConfiguration 
    {
        /// <summary>
        /// Сопоставляет интерфейс с конкретной реализацией
        /// </summary>
        public ServiceProvider Load()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddUnitOfWork();
            services.AddServices();
            services.AddScoped<IConsultant, Consultant>();
            services.AddScoped<IManager, Manager>();
            services.AddScoped<ILoggerService, LoggerService>();

            services.AddScoped<MainViewModel>();
            
            return services.BuildServiceProvider();
        }
    }
}
