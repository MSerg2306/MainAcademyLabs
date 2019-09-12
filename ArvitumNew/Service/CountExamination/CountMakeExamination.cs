using ArvitumNew.Models;
using System.Linq;

namespace ArvitumNew.Service.CountExamination
{
    public class CountMakeExamination
    {
        public int Count { get; }
        public string Color { get; }
        private const int _examinationStatusId = 5;

        public CountMakeExamination(ApplicationDbContext db)
        {
            IQueryable<Examination> examinations;
            examinations = db.Examinations.Where(e => e.Activ == true);
            int? count = examinations.Where(e => e.ExaminationStatusId == _examinationStatusId).Count();

            string color="";
            if (count == null || count == 0)
            { color = "badge"; }
            else if(count >= 1 && count <= 5)
            { color = "badge badge-success"; }
            else if(count > 5 && count <= 10)
            { color = "badge badge-warning"; }
            else if (count >  10)
            { color = "badge badge-important"; }

            Count = count ?? 0;
            Color = color;
        }
    }
}
