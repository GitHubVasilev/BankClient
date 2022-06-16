using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DataAccessLayer.DataAccess
{
    /// <summary>
    /// Класс для предостваления доступа к данным в файле
    /// </summary>
    public class FileContext
    {
        private readonly string fileName = $"{Directory.GetCurrentDirectory()}\\clients.txt";

        /// <summary>
        /// Список моделей, которые содержатся в файле. Если файл не найден, генерирует случайный набор данных
        /// </summary>
        public List<Customer> Customers
        {
            get => ReadFile() ?? RandomData();
            set => UpdataFile(value);
        }

        public FileContext()
        { }

        /// <summary>
        /// Создает случайный набор данных
        /// </summary>
        /// <returns>Случайный набор моделей</returns>
        private List<Customer> RandomData()
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

        /// <summary>
        /// Читает данные из файла
        /// </summary>
        /// <returns>Список моделей полученных из файла</returns>
        [STAThread]
        private List<Customer> ReadFile()
        {
            List<Customer> customers = new();
            try
            {
                using StreamReader sr = new(fileName);
                foreach (string line in sr.ReadToEnd().Split("\r\n"))
                {
                    if (string.IsNullOrEmpty(line)) { continue; }
                    string[] elements = line.Split("\t");
                    customers.Add(new Customer()
                    {
                        UID = new Guid(elements[0]),
                        FirstName = elements[1],
                        LastName = elements[2],
                        Patronymic = elements[3],
                        Telephone = elements[4],
                        Passport = elements[5],
                        DateChange = long.Parse(elements[6]),
                        TypeChanged = int.Parse(elements[7]),
                        FieldChanged = int.Parse(elements[8]),
                        ChangingWorker = elements[9]
                    });
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"EXCEPTION READ: {e.Message}");
                return null;
            }
            return customers;
        }

        /// <summary>
        /// Перезаписывает файл новыми данными
        /// </summary>
        /// <param name="customers">Список моделей для записи</param>
        [STAThread]
        private void UpdataFile(List<Customer> customers)
        {
            string emptyField = " ";
            try
            {
                using StreamWriter stream = new(fileName, append: false);
                foreach (Customer customer in customers)
                {
                    stream.WriteLine($"{customer.UID}" +
                        $"\t{(string.IsNullOrEmpty(customer.FirstName) ? emptyField : customer.FirstName)}" +
                        $"\t{(string.IsNullOrEmpty(customer.LastName) ? emptyField : customer.LastName)}" +
                        $"\t{(string.IsNullOrEmpty(customer.Patronymic) ? emptyField : customer.Patronymic)}" +
                        $"\t{(string.IsNullOrEmpty(customer.Telephone) ? emptyField : customer.Telephone)}" +
                        $"\t{(string.IsNullOrEmpty(customer.Passport) ? emptyField : customer.Passport)}" +
                        $"\t{customer.DateChange}" +
                        $"\t{customer.TypeChanged}" +
                        $"\t{customer.FieldChanged}" +
                        $"\t{(string.IsNullOrEmpty(customer.ChangingWorker) ? emptyField : customer.ChangingWorker)}");
                }
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"EXCEPTION WRITE: {e.Message}");
            }
        }
    }
}
