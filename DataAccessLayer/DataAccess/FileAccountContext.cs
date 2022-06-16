using CsvHelper;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataAccessLayer.DataAccess
{
    public class FileAccountContext
    {
        private readonly string fileName = $"{Directory.GetCurrentDirectory()}\\accounts.csv";

        /// <summary>
        /// Читает из файла и создает список счетов
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAccounts()
        {
            List<Account> accounts = new();
            try
            {
                using StreamReader sr = new(fileName);
                using (CsvReader csv = new CsvReader(sr, CultureInfo.CurrentCulture))
                {
                    accounts = csv.GetRecords<Account>().ToList();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"EXCEPTION READ: {e.Message}");
                return new List<Account>();
            }
            return accounts;
        }

        /// <summary>
        /// Записывает список счетов в csv файл
        /// </summary>
        /// <param name="accounts">список считов для записи</param>
        public void SetAccount(List<Account> accounts)
        {
            try
            {
                using StreamWriter sr = new(fileName);
                using (CsvWriter csv = new(sr, CultureInfo.CurrentCulture))
                {
                    csv.WriteRecords(accounts);
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"EXCEPTION WRITE: {e.Message}");
            }
        }
    }
}
