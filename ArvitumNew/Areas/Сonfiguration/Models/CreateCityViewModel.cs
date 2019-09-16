using System.ComponentModel.DataAnnotations;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class CreateCityViewModel
    {
        [Required(ErrorMessage = "Не указано название города")]
        public string NameCity { get; set; }
    }
}
