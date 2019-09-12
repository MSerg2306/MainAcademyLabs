using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Areas.Identity.Models
{
    public class ChangePersonPasswordViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required (ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Не указан email")]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        //[Required (ErrorMessage = "Не указан год рождения")]
        //public int Year { get; set; }

        [Required (ErrorMessage = "Не указан старый пароль")]
        public string OldPassword { get; set; }

        [Required (ErrorMessage = "Не указан новый пароль")]
        public string NewPassword { get; set; }

    }
}
