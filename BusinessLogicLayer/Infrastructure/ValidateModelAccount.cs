using System;
using Exceptions;

namespace BusinessLogicLayer.Infrastructure
{
    /// <summary>
    /// Собирает методы для проверки полей счетов модели на корректность
    /// </summary>
    internal class ValidateModelAccount
    {
        /// <summary>
        /// Проверяет название счета на null и корректность написания.
        /// </summary>
        /// <param name="property">Название аккаунта</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MinLenghtNameAccountException"></exception>
        public void ValidateName(string? property) 
        {
            if (string.IsNullOrWhiteSpace(property)) { throw new ArgumentNullException("Поле \"Имя\" не должно быть пустым"); }
            else if (property.Length != 16) { throw new MinLenghtNameAccountException("Длина поля \"Имя\" должно быть равно 16 символам"); }
        }
    }
}
