using ArvitumNew.Helper;
using ArvitumNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Areas.ExaminationAreas.Models
{
    public class ExaminationListViewModel
    {
        public int CurrentCustomerId { get; set; }
        public IEnumerable<Examination> Examinations { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public ExaminationFilterViewModel FilterViewModel { get; set; }
        public ExaminationSortViewModel SortViewModel { get; set; }

    }
}
