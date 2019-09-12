using Microsoft.AspNetCore.Mvc.Rendering;
using ArvitumNew.Models;
using System.Collections.Generic;
using ArvitumNew.Util;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class ActivFilterViewModel
    {
        public ActivFilterViewModel(List<Activ> activs, int? activ)
        {
            Activs = new SelectList(activs, "ActivStutys", "ActivName", activ);
            SelectedActiv = activ;
        }
        public SelectList Activs { get; private set; } // список компаний
        public int? SelectedActiv { get; private set; }   // выбранная компания
    }
}
