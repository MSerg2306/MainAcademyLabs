namespace ArvitumNew.Areas.ExaminationAreas.Models
{
    public class ExaminationSortViewModel
    {
        public ExaminationSortState DateExaminationSort { get; private set; } // значение для сортировки по имени
        public ExaminationSortState Current { get; private set; }     // текущее значение сортировки

        public ExaminationSortViewModel(ExaminationSortState sortOrder)
        {
            DateExaminationSort = sortOrder == ExaminationSortState.DateExaminationAsc ? ExaminationSortState.DateExaminationDesc : ExaminationSortState.DateExaminationAsc;
            Current = sortOrder;
        }
    }
}
