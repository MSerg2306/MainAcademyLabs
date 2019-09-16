namespace ArvitumNew.Areas.Сonfiguration.Models
{
    public class InformationChannelSortViewModel
    {
        public InformationChannelSortState NameSort { get; private set; } // значение для сортировки по имени
        public InformationChannelSortState Current { get; private set; }     // текущее значение сортировки

        public InformationChannelSortViewModel(InformationChannelSortState sortOrder)
        {
            NameSort = sortOrder == InformationChannelSortState.InformationChannelNameAsc ? InformationChannelSortState.InformationChannelNameDesc : InformationChannelSortState.InformationChannelNameAsc;
            Current = sortOrder;
        }
    }
}
