using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Models
{
    public partial class TypeWorkPlace
    {
        public int TypeWorkPlaceId { get; set; }
        public string NameTypeWorkPlace { get; set; }
        public int PayTypeId { get; set; }

        public virtual PayType PayType { get; set; }

        public virtual ICollection<WorkPlace> WorkPlace { get; set; }
    }
}
