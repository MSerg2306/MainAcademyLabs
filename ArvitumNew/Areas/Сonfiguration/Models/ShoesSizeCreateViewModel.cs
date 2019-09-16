using System.ComponentModel.DataAnnotations;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class ShoesSizeCreateViewModel
    {
        [Required(ErrorMessage = "Не указана длина стопы")]
        public decimal FootLength { get; set; }
        [Required(ErrorMessage = "Не указан размер")]
        public decimal Size { get; set; }
    }
}
