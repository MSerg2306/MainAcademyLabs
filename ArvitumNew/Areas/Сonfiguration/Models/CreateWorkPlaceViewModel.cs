using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class CreateWorkPlaceViewModel
    {
        [Required(ErrorMessage = "Не указано название мед.центра")]
        public string NameWorkPlace { get; set; }
        [Required(ErrorMessage = "Не указан адрес")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Не указано название города")]
        public string NameCity { get; set; }
        [Required(ErrorMessage = "Не указан тип мед.центра")]
        public string NameTypeWorkPlace { get; set; }
        [Required(ErrorMessage = "Не указан путь к dll")]
        public string LocalPathDll { get; set; }
        [Required(ErrorMessage = "Не путь к временной папке для фото")]
        public string LacalPathImage { get; set; }

        public CreateWorkPlaceViewModel()
        {
            LocalPathDll = @"C:\Windows\Temp\";
            LacalPathImage = @"C:\Windows\Temp\";
        }
    }
}
