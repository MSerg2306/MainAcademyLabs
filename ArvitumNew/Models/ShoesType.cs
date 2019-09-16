using System;
using System.Collections.Generic;

namespace ArvitumNew.Models
{    
    public partial class ShoesType : ShoesTypeForXML
    {
        public virtual ICollection<Examination> Examination { get; set; }
    }

    [Serializable]
    public partial class ShoesTypeForXML
    {
        public int ShoesTypeId { get; set; }
        public string ShoesTypeName { get; set; }
        public Boolean Activ { get; set; }

        public ShoesTypeForXML()
        { }
        public ShoesTypeForXML(int shoesTypeId, string shoesTypeName)
        {
            ShoesTypeId = shoesTypeId;
            ShoesTypeName = shoesTypeName;
        }
    }
}
