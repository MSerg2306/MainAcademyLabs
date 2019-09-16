using Microsoft.AspNetCore.Mvc.Rendering;
using ArvitumNew.Models;
using System.Collections.Generic;
using System;

namespace ArvitumNew.Areas.ProductionAreas.Models
{
    public class ExaminationFilterViewModel
    {
        public ExaminationFilterViewModel(List<WorkPlace> workPlaces, int? workPlace,
                                            List<ExaminationStatus> examinationStatuss, int? examinationStatus,
                                            DateTime dateExamination, string nameFirstName)
        {
            WorkPlaces = new SelectList(workPlaces, "WorkPlaceId", "NameWorkPlace", workPlace);
            SelectedWorkPlace = workPlace;

            ExaminationStatuss = new SelectList(examinationStatuss, "ExaminationStatusId", "ExaminationStatusName", examinationStatus);
            SelectedExaminationStatus = examinationStatus;

            SelectedDateExamination = dateExamination;
            SelectedFirstName = nameFirstName;
        }
        public SelectList WorkPlaces { get; private set; } // список компаний
        public int? SelectedWorkPlace { get; private set; }   // выбранная компания

        public SelectList ExaminationStatuss { get; private set; } // список компаний
        public int? SelectedExaminationStatus { get; private set; }   // выбранная компания

        public DateTime SelectedDateExamination { get; private set; }
        public string SelectedFirstName { get; private set; }    // введенное имя
    }
}
