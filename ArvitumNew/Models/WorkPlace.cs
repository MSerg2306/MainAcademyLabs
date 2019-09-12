using System.Collections.Generic;
using System.Linq;

namespace ArvitumNew.Models
{
    public partial class WorkPlace
    {
        public int WorkPlaceId { get; set; }
        public int CityId { get; set; }
        public int TypeWorkPlaceId { get; set; }
        public string NameWorkPlace { get; set; }
        public string Address { get; set; }
        public string LocalPathDll { get; set; }
        public string LacalPathImage { get; set; }

        public virtual City City { get; set; }
        public virtual TypeWorkPlace TypeWorkPlace { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }

        public static List<WorkPlace> WorkPlaceList(ApplicationDbContext db)
        {
            List<WorkPlace> workPlaces = new List<WorkPlace> { };
            workPlaces.Insert(0, new WorkPlace { NameWorkPlace = "Все", WorkPlaceId = 0 });
            int i = 1;
            foreach (var g in db.WorkPlaces.ToList())
            {
                workPlaces.Insert(i, new WorkPlace { NameWorkPlace = g.NameWorkPlace, WorkPlaceId = g.WorkPlaceId });
                i++;
            }
            return workPlaces;
        }
    }
}
