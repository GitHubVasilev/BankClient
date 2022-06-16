using BusinessLogicLayer.DTO;
using System.ComponentModel.DataAnnotations;
using WpfApp1.ViewModel.Base;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// Модель предстваления для создания записи о клиенте
    /// </summary>
    public class CreateCustomerVM : ValidationBaseViewModel
    {
        public CreateCustomerVM()
        {
            FirstName = "";
            LastName = "";
            Passport = "";
            Patronymic = "";
            Telephone = "";
        }

        /// <summary>
        /// подель для передачи данных о клиенте
        /// </summary>
        public CustomerDTO BaseModel => new()
        {
            UID = System.Guid.NewGuid(),
            FirstName = FirstName,
            LastName = LastName,
            Patronymic = Patronymic,
            Telephone = Telephone,
            Passport = Passport
        };

        private string _firstName;
        /// <summary>
        /// Имя
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string FirstName 
        {
            get => _firstName;
            set => Set(ref _firstName, value, nameof(FirstName));
        }

        private string _lastName;
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string LastName 
        {
            get => _lastName;
            set => Set(ref _lastName, value, nameof(LastName));
        }

        private string _patronymic;
        /// <summary>
        /// Отчество
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Patronymic 
        {
            get => _patronymic;
            set => Set(ref _patronymic, value, nameof(Patronymic));
        }

        private string _telephone;
        /// <summary>
        /// Телефон
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Telephone
        {
            get => _telephone;
            set => Set(ref _telephone, value, nameof(Telephone));
        }

        private string _passport;
        /// <summary>
        /// Паспортные данные
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Passport
        {
            get => _passport;
            set => Set(ref _passport, value, nameof(Passport));
        }
    }
}
