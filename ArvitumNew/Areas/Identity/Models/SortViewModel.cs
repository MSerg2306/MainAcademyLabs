namespace ArvitumNew.Areas.Identity.Models
{
    public class SortViewModel
    {
        public CustomerSortState UserNameSort { get; private set; } // значение для сортировки по имени
        public CustomerSortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(CustomerSortState sortOrder)
        {
            UserNameSort = sortOrder == CustomerSortState.UserNameAsc ? CustomerSortState.UserNameDesc : CustomerSortState.UserNameAsc;
            Current = sortOrder;
        }
    }
}
