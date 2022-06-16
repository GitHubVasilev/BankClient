using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using LoggerLayer.Services;
using LoggerLayer.Interfaces;
using Ninject.Modules;

namespace WpfApp1.Infrastructure
{
    /// <summary>
    /// Добавление внедрение зависимости
    /// </summary>
    internal class IocConfiguration : NinjectModule
    {
        /// <summary>
        /// Сопоставляет интерфейс с конкретной реализацией
        /// </summary>
        public override void Load()
        {
            Bind<IConsultant>().To<Consultant>();
            Bind<IManager>().To<Manager>();
            Bind<ILoggerService>().To<LoggerService>().InSingletonScope();
        }
    }
}
