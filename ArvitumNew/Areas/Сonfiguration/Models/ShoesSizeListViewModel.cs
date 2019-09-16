using ArvitumNew.Helper;
using ArvitumNew.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class ShoesSizeListViewModel
    {
        public IEnumerable<ShoesSize> ShoesSizes { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public ActivFilterViewModel FilterViewModel { get; set; }
        public ShoesSizeSortViewModel SortViewModel { get; set; }
        public SelectList SelectedListViev { get; set; }
    }
}
