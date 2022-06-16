using System;

namespace BusinessLogicLayer.Infrastructure
{
    /// <summary>
    /// Собирает методы для проверки полей модели на корректность
    /// </summary>
    public class ValidateModel
    {
        /// <summary>
        /// Проверяет имя на корректность
        /// </summary>
        /// <param name="property">имя для провеки</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ValideteFirstName(string? property) 
        {
            if (string.IsNullOrWhiteSpace(property)) 
            {
                throw new ArgumentNullException(property, "поле \"Имя\" не должно быть пустым");
            }
        }

        /// <summary>
        /// Проверяет фамилию на корректность
        /// </summary>
        /// <param name="property">имя для провеки</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ValideteLastName(string? property)
        {
            if (string.IsNullOrWhiteSpace(property))
            {
                throw new ArgumentNullException(property, "поле \"Фамилия\" не должно быть пустым");
            }
        }

        /// <summary>
        /// Проверяет отчество на корректность
        /// </summary>
        /// <param name="property">имя для провеки</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ValidetePatronymic(string? property)
        {
            if (string.IsNullOrWhiteSpace(property))
            {
                throw new ArgumentNullException(property, "поле \"Отчество\" не должно быть пустым");
            }
        }

        /// <summary>
        /// Проверяет паспорт на корректность
        /// </summary>
        /// <param name="property">имя для провеки</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ValidetePassport(string? property)
        {
            if (string.IsNullOrWhiteSpace(property))
            {
                throw new ArgumentNullException(property, "поле \"Паспорт\" не должно быть пустым");
            }
        }

        /// <summary>
        /// Проверяет телефон на корректность
        /// </summary>
        /// <param name="property">имя для провеки</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ValideteTelephone(string? property)
        {
            if (string.IsNullOrWhiteSpace(property))
            {
                throw new ArgumentNullException(property, "поле \"Телефон\" не должно быть пустым");
            }
        }
    }
}
