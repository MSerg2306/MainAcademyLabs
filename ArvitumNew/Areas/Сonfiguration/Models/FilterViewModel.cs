using Microsoft.AspNetCore.Mvc.Rendering;
using ArvitumNew.Models;
using System.Collections.Generic;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<City> citys, int? city, string nameCity)
        {
            Citys = new SelectList(citys, "CityId", "NameCity", city);
            SelectedCity = city;
            SelectedNameCity = nameCity;
        }
        public SelectList Citys { get; private set; } // список компаний
        public int? SelectedCity { get; private set; }   // выбранная компания
        public string SelectedNameCity { get; private set; }    // введенное имя
    }
}
