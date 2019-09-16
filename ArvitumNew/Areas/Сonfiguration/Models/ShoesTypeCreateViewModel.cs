using System.ComponentModel.DataAnnotations;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class ShoesTypeCreateViewModel
    {
        [Required(ErrorMessage = "Не указано название типа")]
        public string ShoesTypeName { get; set; }
    }
}
