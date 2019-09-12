namespace ArvitumNew.Areas.CustomerAreas.Models
{
    public class ShoesTypeSortViewModel
    {
        public СustomerSortState FirstNameSort { get; private set; } // значение для сортировки по имени
        public СustomerSortState Current { get; private set; }     // текущее значение сортировки

        public ShoesTypeSortViewModel(СustomerSortState sortOrder)
        {
            FirstNameSort = sortOrder == СustomerSortState.FullNameAsc ? СustomerSortState.FullNameDesc : СustomerSortState.FullNameAsc;
            Current = sortOrder;
        }
    }
}
