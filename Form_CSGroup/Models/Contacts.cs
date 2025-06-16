using System.ComponentModel.DataAnnotations;

namespace Form_CSGroup.Models
{
    public class Contacts
    {
        [Required(ErrorMessage = "Забыли ввести ФИО")]
        [Display(Name = "Фамилия Имя Отчество")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите название организации")]
        [Display(Name = "Название организации")]
        public string Organization { get; set; }
        [Required(ErrorMessage = "Введите номер телефона")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Введите почту")]
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите свою должность")]
        [Display(Name = "Должность")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Опишите какой продукт вас интересует?")]
        [Display(Name = "Что вас интересует?")]
        public string Message { get; set; }



    }
}
