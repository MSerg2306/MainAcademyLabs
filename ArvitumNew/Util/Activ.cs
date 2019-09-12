using System.Collections.Generic;

namespace ArvitumNew.Util
{
    public class Activ
    {
        public int ActivStutys { get; set; }
        public string ActivName { get; set; }
        public Activ() { }

        public static List<Activ> ActivList()
        {
            List<Activ> activ = new List<Activ>() { };
            activ.Insert(0, new Activ { ActivName = "Все", ActivStutys = 0 });
            activ.Insert(1, new Activ { ActivName = "Скрытый", ActivStutys = -1 });
            activ.Insert(2, new Activ { ActivName = "Активный", ActivStutys = 1 });

            return activ;
        }
    }
}
