using System;
using System.ComponentModel.DataAnnotations;

namespace ArvitumNew.Areas.CustomerAreas.Models
{
    public class CustomerCreateViewModel
    {
        [Required(ErrorMessage = "Не указана фамилия")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указано имя отчество")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Не указан телефон")]
        [Phone]
        public string Phone { get; set; }

        public DateTime DateRegistration { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан Мед.центр")]
        public string NameWorkPlace { get; set; }
        public string InformationChannelName { get; set; }
        public Boolean Activ { get; set; }

        public CustomerCreateViewModel()
        {
            Activ = true;
        }
    }
}
