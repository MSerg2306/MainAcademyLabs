using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Models
{
    public partial class CoatingType : CoatingTypeForXML
    {
        public virtual ICollection<Examination> Examination { get; set; }
    }

    [Serializable]
    public partial class CoatingTypeForXML
    {
        public int CoatingTypeId { get; set; }
        public string CoatingTypeName { get; set; }
        public Boolean Activ { get; set; }

        public CoatingTypeForXML()
        { }
        public CoatingTypeForXML(int coatingTypeId, string coatingTypeName)
        {
            CoatingTypeId = coatingTypeId;
            CoatingTypeName = coatingTypeName;
        }
    }

}
