using System;
using System.Collections.Generic;

namespace ArvitumNew.Models
{
    public partial class Examination
    {
        public int ExaminationId { get; set; }
        public int CustomerId { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> DateExamination { get; set; }
        public string Reason { get; set; }
        public string Diagnosis { get; set; }
        public string Note { get; set; }
        public int ShoesTypeId { get; set; }
        public int ShoesSizeId { get; set; }
        public int CoatingTypeId { get; set; }
        public int CoatingThicknessId { get; set; }
        public Boolean Express { get; set; }
        public Boolean Activ { get; set; }
        public int ExaminationStatusId { set; get; }

        public virtual Customer Сustomer { get; set; }
        public virtual User User { get; set; }
        public virtual ExaminationBackPhoto ExaminationBackPhoto { get; set; }
        public virtual ExaminationBottomPhoto ExaminationBottomPhoto { get; set; }
        public virtual ShoesType ShoesType { get; set; }
        public virtual ShoesSize ShoesSize { get; set; }
        public virtual CoatingType CoatingType { get; set; }
        public virtual CoatingThickness CoatingThickness { get; set; }
        public virtual ExaminationStatus ExaminationStatus { get; set; }

        public virtual ICollection<ExaminationHistory> ExaminationHistory { get; set; }

    }
}
