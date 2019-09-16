using System.ComponentModel.DataAnnotations;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class InformationChannelCreateViewModel
    {
        [Required(ErrorMessage = "Не указано название типа")]
        public string InformationChannelName { get; set; }
    }
}
