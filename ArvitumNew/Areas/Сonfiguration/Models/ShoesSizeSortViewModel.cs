using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class ShoesSizeSortViewModel
    {
        public ShoesSizeSortState FootLengthSort { get; private set; } // значение для сортировки по имени
        public ShoesSizeSortState SizeSort { get; private set; } // значение для сортировки по имени
        public ShoesSizeSortState Current { get; private set; }     // текущее значение сортировки

        public ShoesSizeSortViewModel(ShoesSizeSortState sortOrder)
        {
            FootLengthSort = sortOrder == ShoesSizeSortState.FootLengthAsc ? ShoesSizeSortState.FootLengthDesc : ShoesSizeSortState.FootLengthAsc;
            SizeSort = sortOrder == ShoesSizeSortState.SizeAsc ? ShoesSizeSortState.SizeDesc : ShoesSizeSortState.SizeAsc;
            Current = sortOrder;
        }
    }
}
