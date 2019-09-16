using ArvitumNew.Helper;
using ArvitumNew.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class ShoesTypeListViewModel
    {
        public IEnumerable<ShoesType> ShoesTypes { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public ActivFilterViewModel FilterViewModel { get; set; }
        public ShoesTypeSortViewModel SortViewModel { get; set; }
        public SelectList SelectedListViev { get; set; }
    }
}
