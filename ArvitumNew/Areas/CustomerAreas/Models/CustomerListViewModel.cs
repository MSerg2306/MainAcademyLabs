using ArvitumNew.Helper;
using ArvitumNew.Models;
using System.Collections.Generic;

namespace ArvitumNew.Areas.CustomerAreas.Models
{
    public class CustomerListViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public ShoesTypeFilterViewModel FilterViewModel { get; set; }
        public ShoesTypeSortViewModel SortViewModel { get; set; }
    }
}
