namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class SortViewModel
    {
        public SortState WorkPlaceNameSort { get; private set; } // значение для сортировки по имени
        public SortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            WorkPlaceNameSort = sortOrder == SortState.WorkPlaceNameAsc ? SortState.WorkPlaceNameDesc : SortState.WorkPlaceNameAsc;
            Current = sortOrder;
        }
    }
}
