using System;
using System.ComponentModel.DataAnnotations;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class ShoesTypeEditViewModel
    {
        public int ShoesTypeId { get; set; }

        [Required(ErrorMessage = "Не указано название типа")]
        public string ShoesTypeName { get; set; }
        [Display(Name = "Активен")]
        public Boolean Activ { get; set; }
    }
}
