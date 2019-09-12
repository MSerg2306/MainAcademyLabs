using System;
using System.Collections.Generic;
using System.Linq;

namespace ArvitumNew.Models
{
    public partial class InformationChannel : InformationChannelForXML
    {
        public virtual ICollection<User> User { get; set; }

        public static List<InformationChannel> InformationChannelList(ApplicationDbContext db)
        {
            List<InformationChannel> informationChannels = new List<InformationChannel> { };
            informationChannels.Insert(0, new InformationChannel { InformationChannelName = "Все", InformationChannelId = 0 });
            int i = 1;
            foreach (var g in db.InformationChannels.ToList())
            {
                informationChannels.Insert(i, new InformationChannel { InformationChannelName = g.InformationChannelName, InformationChannelId = g.InformationChannelId });
                i++;
            }
            return informationChannels;
        }
    }

    [Serializable]
    public partial class InformationChannelForXML
    {
        public int InformationChannelId { get; set; }
        public string InformationChannelName { get; set; }
        public Boolean Activ { get; set; }

        public InformationChannelForXML()
        { }
        public InformationChannelForXML(int informationChannelId, string informationChannelName)
        {
            InformationChannelId = informationChannelId;
            InformationChannelName = informationChannelName;
        }
    }
}
