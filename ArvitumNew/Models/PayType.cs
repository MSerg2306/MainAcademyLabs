using System.Collections.Generic;

namespace ArvitumNew.Models
{
    public class PayType
    {
        public int PayTypeId { get; set; }
        public string PayTypeName { get; set; }
        public int ExaminationStatusId { get; set; }

        public virtual ExaminationStatus ExaminationStatus { get; set; }
        public virtual ICollection<TypeWorkPlace> TypeWorkPlace { get; set; }
    }
}
