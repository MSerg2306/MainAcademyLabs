using Microsoft.AspNetCore.Mvc.Rendering;
using ArvitumNew.Models;
using System.Collections.Generic;
using System;

namespace ArvitumNew.Areas.ExaminationAreas.Models
{
    public class ExaminationFilterViewModel
    {
        public ExaminationFilterViewModel(List<WorkPlace> workPlaces, int? workPlace, int? customerId,
                                            DateTime dateExamination, string nameFullName)
        {
            WorkPlaces = new SelectList(workPlaces, "WorkPlaceId", "NameWorkPlace", workPlace);
            SelectedWorkPlace = workPlace;
            SelectedDateExamination = dateExamination;
            SelectedFullName = nameFullName;
            SelectedCustomerId = customerId;
        }
        public SelectList WorkPlaces { get; private set; } // список компаний
        public int? SelectedWorkPlace { get; private set; }   // выбранная компания
        public DateTime SelectedDateExamination { get; private set; }
        public string SelectedFullName { get; private set; }    // введенное имя
        public int? SelectedCustomerId { get; private set; }    // введенное имя
    }
}
