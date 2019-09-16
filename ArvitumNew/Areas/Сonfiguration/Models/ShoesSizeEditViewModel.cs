using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class ShoesSizeEditViewModel
    {
        public int ShoesSizeId { get; set; }

        [Required(ErrorMessage = "Не указана длина стопы")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FootLength { get; set; }
        [Required(ErrorMessage = "Не указан размер")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Size { get; set; }
        public Boolean Activ { get; set; }
    }
}
