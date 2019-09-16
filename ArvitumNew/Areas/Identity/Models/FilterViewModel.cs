using Microsoft.AspNetCore.Mvc.Rendering;
using ArvitumNew.Models;
using System.Collections.Generic;

namespace ArvitumNew.Areas.Identity.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<WorkPlace> workPlaces, int? workPlace, string userName)
        {
            WorkPlaces = new SelectList(workPlaces, "WorkPlaceId", "NameWorkPlace", workPlace);
            SelectedWorkPlace = workPlace;
            SelectedUserName = userName;
        }
        public SelectList WorkPlaces { get; private set; } // список компаний
        public int? SelectedWorkPlace { get; private set; }   // выбранная компания
        public string SelectedUserName { get; private set; }    // введенное имя
    }
}
