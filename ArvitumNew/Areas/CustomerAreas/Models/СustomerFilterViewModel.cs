using Microsoft.AspNetCore.Mvc.Rendering;
using ArvitumNew.Models;
using System.Collections.Generic;

namespace ArvitumNew.Areas.CustomerAreas.Models
{
    public class ShoesTypeFilterViewModel
    {
        public ShoesTypeFilterViewModel(List<WorkPlace> workPlaces, int? workPlace, int? customerId, string nameFullName)
        {
            WorkPlaces = new SelectList(workPlaces, "WorkPlaceId", "NameWorkPlace", workPlace);
            SelectedWorkPlace = workPlace;
            SelectedFullName = nameFullName;
            SelectedCustomerId = customerId;
        }
        public SelectList WorkPlaces { get; private set; } // список компаний
        public int? SelectedWorkPlace { get; private set; }   // выбранная компания
        public string SelectedFullName { get; private set; }    // введенное имя
        public int? SelectedCustomerId { get; private set; }    // введенное имя
    }
}
