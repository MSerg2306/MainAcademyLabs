using System;
using System.Collections.Generic;

namespace ArvitumNew.Models
{
    public partial class ShoesSize : ShoesSizeForXML
    {
        public virtual ICollection<Examination> Examination { get; set; }
        public virtual ICollection<ExaminationBottomPhoto> ExaminationBottomPhoto { get; set; }
    }

    [Serializable]
    public partial class ShoesSizeForXML
    {
        public int ShoesSizeId { get; set; }
        public decimal FootLength { get; set; }
        public decimal Size { get; set; }
        public Boolean Activ { get; set; }

        public ShoesSizeForXML()
        { }
        public ShoesSizeForXML(int shoesSizeId, decimal footLength, decimal size, bool activ)
        {
            ShoesSizeId = shoesSizeId;
            FootLength = footLength;
            Size = size;
            Activ = activ;
        }
    }
}
