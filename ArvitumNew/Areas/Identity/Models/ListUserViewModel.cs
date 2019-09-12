using ArvitumNew.Helper;
using ArvitumNew.Models;
using System.Collections.Generic;

namespace ArvitumNew.Areas.Identity.Models
{
    public class ListUserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
