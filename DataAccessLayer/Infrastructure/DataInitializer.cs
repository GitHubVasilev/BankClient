using DataAccessLayer.DataAccess;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure
{
    /// <summary>
    /// Первоначальная заполнение базы данных
    /// </summary>
    public static class DataInitializer
    {
        /// <summary>
        /// Заполняет базу данных начальными значениями если она пуста
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task InitializerAsync(IServiceProvider serviceProvider)
        {

            IServiceScope scope = serviceProvider.CreateScope();
            await using ApplicationDbContext context = scope.ServiceProvider.GetService<DbContext>() 
                                                        as ApplicationDbContext;
            bool t = context.Database.CanConnect();

            if (!context!.Accounts.Any()) 
            {
                await context.AddRangeAsync(GenerateCustomers());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Customer> GenerateCustomers() 
        {
            Random rnd = new Random();
            List<Customer> customers = new();

            for (int i = 0; i < 15; i++)
            {
                customers.Add(
                    new Customer()
                    {
                        UID = Guid.NewGuid(),
                        FirstName = $"Имя_{rnd.Next(1, 1000)}",
                        LastName = $"Фамилия_{rnd.Next(1, 1000)}",
                        Patronymic = $"Отчество_{rnd.Next(1, 1000)}",
                        Telephone = $"+7(9{rnd.Next(10, 99)}){rnd.Next(100, 999)}-{rnd.Next(10, 99)}-{rnd.Next(10, 99)}",
                        Passport = $"{rnd.Next(10, 99)} {rnd.Next(10, 99)} {rnd.Next(100000, 999999)}",
                        DateChange = DateTime.Now.Ticks,
                        FieldChanged = 0,
                        TypeChanged = 0,
                        ChangingWorker = "Админ"
                    }
                    );
            }
            return customers;
        }
    }
}
