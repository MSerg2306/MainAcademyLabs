using ArvitumNew.Helper;
using ArvitumNew.Models;
using System.Collections.Generic;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class CityWorkPlaceViewModel
    {
        public IEnumerable<WorkPlace> WorkPlaces { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
