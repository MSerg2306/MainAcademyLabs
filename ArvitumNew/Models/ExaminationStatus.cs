using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Models
{
    public class ExaminationStatus : ExaminationStatusForXML
    {
        public virtual ICollection<Examination> Examination { get; set; }
        //public virtual ICollection<ExaminationHistory> ExaminationHistory { get; set; }
        public virtual ICollection<PayType> PayType { get; set; }


        public static List<ExaminationStatus> ExaminationStatusList(ApplicationDbContext db)
        {
            List<ExaminationStatus> examinationStatuss = new List<ExaminationStatus> { };
            examinationStatuss.Insert(0, new ExaminationStatus { ExaminationStatusName = "Все", ExaminationStatusId = 0 });
            int i = 1;
            foreach (var g in db.ExaminationStatuss.ToList())
            {
                examinationStatuss.Insert(i, new ExaminationStatus { ExaminationStatusName = g.ExaminationStatusName, ExaminationStatusId = g.ExaminationStatusId });
                i++;
            }
            return examinationStatuss;
        }
    }

    [Serializable]
    public class ExaminationStatusForXML
    {
        public int ExaminationStatusId { set; get; }
        public string ExaminationStatusName { set; get; }

        public ExaminationStatusForXML()
        { }
        public ExaminationStatusForXML(int examinationStatusId, string examinationStatusName)
        {
            ExaminationStatusId = examinationStatusId;
            ExaminationStatusName = examinationStatusName;
        }
    }
}
