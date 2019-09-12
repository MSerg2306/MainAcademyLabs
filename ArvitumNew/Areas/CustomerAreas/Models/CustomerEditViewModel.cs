using System;
using System.ComponentModel.DataAnnotations;

namespace ArvitumNew.Areas.CustomerAreas.Models
{
    public class CustomerEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указано имя отчество")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Не указана дата рождения")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Не указан телефон")]
        public string Phone { get; set; }
        public DateTime DateRegistration { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        [Display(Name = "Активен")]
        public Boolean Activ { get; set; }
        public string NameWorkPlace { get; set; }
        public string InformationChannelName { get; set; }

    }
}
