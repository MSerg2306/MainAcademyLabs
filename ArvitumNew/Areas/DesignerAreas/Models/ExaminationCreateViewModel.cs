using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Areas.DesignerAreas.Models
{
    public class ExaminationCreateViewModel
    {
        public int ExaminationId { get; set; }
        public int CustomerId { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> DateExamination { get; set; }
        [MinLength(0)]
        [MaxLength(1024)]
        public string Reason { get; set; }
        [MinLength(0)]
        [MaxLength(1024)]
        public string Diagnosis { get; set; }
        [MinLength(0)]
        [MaxLength(1024)]
        public string Note { get; set; }
        public string ShoesTypeName { get; set; }
        public decimal Size { get; set; }
        public string CoatingTypeName { get; set; }
        public string CoatingThicknessName { get; set; }
        [Display(Name = "Срочный заказ")]
        public Boolean Express { get; set; }
        public Boolean Activ { get; set; }
    }
}
