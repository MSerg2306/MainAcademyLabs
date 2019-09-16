using System.Collections.Generic;
using System.Linq;

namespace ArvitumNew.Models
{
    public partial class City
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public City()
        {
            this.WorkPlace = new HashSet<WorkPlace>();
        }
    
        public int CityId { get; set; }
        public string NameCity { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkPlace> WorkPlace { get; set; }

        public static List<City> CityList(ApplicationDbContext db)
        {
            List<City> citys = new List<City> { };
            citys.Insert(0, new City { NameCity = "Все", CityId = 0 });
            int i = 1;
            foreach (var g in db.Citys.ToList())
            {
                citys.Insert(i, new City { NameCity = g.NameCity, CityId = g.CityId });
                i++;
            }
            return citys;
        }

    }
}
