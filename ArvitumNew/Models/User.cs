using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ArvitumNew.Models
{
    public partial class User : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Examination = new HashSet<Examination>();
        }

        public int WorkPlaceId { get; set; }
    
        public virtual WorkPlace WorkPlace { get; set; }

        public virtual ICollection<Examination> Examination { get; set; }
        public virtual ICollection<ExaminationHistory> ExaminationHistory { get; set; }

    }
}
