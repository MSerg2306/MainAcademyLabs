using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Models
{
    public class ExaminationHistory
    {
        public int ExaminationHistoryId { set; get; }
        public int ExaminationId { get; set; }
        public int ExaminationStatusId { set; get; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> DateChangeStatus { get; set; }

        public virtual Examination Examination { get; set; }
        //public virtual ExaminationStatus ExaminationStatus { get; set; }
        public virtual User User { get; set; }

    }
}
