using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Models
{
    public partial class CoatingThickness : CoatingThicknessForXML
    {
        public virtual ICollection<Examination> Examination { get; set; }
    }
    public partial class CoatingThicknessForXML
    {
        public int CoatingThicknessId { get; set; }
        public string CoatingThicknessName { get; set; }
        public Boolean Activ { get; set; }

        public CoatingThicknessForXML()
        { }
        public CoatingThicknessForXML(int coatingThicknessId, string coatingThicknessName)
        {
            CoatingThicknessId = coatingThicknessId;
            CoatingThicknessName = coatingThicknessName;
        }
    }
}
