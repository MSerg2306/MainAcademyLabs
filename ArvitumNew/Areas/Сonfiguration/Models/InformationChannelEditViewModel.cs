using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class InformationChannelEditViewModel
    {
        public int InformationChannelId { get; set; }

        [Required(ErrorMessage = "Не указано название канала информации")]
        public string InformationChannelName { get; set; }
        [Display(Name = "Активен")]
        public Boolean Activ { get; set; }

    }
}
