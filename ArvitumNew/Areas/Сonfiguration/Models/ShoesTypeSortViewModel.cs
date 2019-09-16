namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class ShoesTypeSortViewModel
    {
        public ShoesTypeSortState FirstNameSort { get; private set; } // значение для сортировки по имени
        public ShoesTypeSortState Current { get; private set; }     // текущее значение сортировки

        public ShoesTypeSortViewModel(ShoesTypeSortState sortOrder)
        {
            FirstNameSort = sortOrder == ShoesTypeSortState.ShoesTypeNameAsc ? ShoesTypeSortState.ShoesTypeNameDesc : ShoesTypeSortState.ShoesTypeNameAsc;
            Current = sortOrder;
        }
    }
}
