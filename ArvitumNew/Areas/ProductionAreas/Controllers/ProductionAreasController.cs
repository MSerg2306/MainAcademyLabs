using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArvitumNew.Areas.ProductionAreas.Models;
using ArvitumNew.Helper;
using ArvitumNew.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArvitumNew.Areas.ProductionAreas.Controllers
{
    [Area("ProductionAreas")]
    [Authorize(Roles = "admin, Производство")]
    public class ProductionAreasController : Controller
    {
        private UserManager<User> _userManager;
        private ApplicationDbContext db;
        private string userId;
        private const int MinLimitStatusView = 5;
        private const int MaxLimitStatusView = 8;
        private IQueryable<string> nameWorkPlace;

        public ProductionAreasController(UserManager<User> userManager, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            db = context;

            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            nameWorkPlace = from p in db.WorkPlaces select p.NameWorkPlace;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ExaminationList(int? workPlace, int? examinationStatus, DateTime dateExamination, string firstName, int page = 1, ExaminationSortState sortOrder = ExaminationSortState.DateExaminationDesc)
        {
            int pageSize = 10;

            IQueryable<Examination> examinations;
            examinations = db.Examinations.Where(e => e.Activ == true);

            examinations = examinations.Include(p => p.Сustomer);
            examinations = examinations.Include(p => p.Сustomer.WorkPlace);
            examinations = examinations.Include(p => p.ExaminationStatus);
            examinations = examinations.Where(e => (e.ExaminationStatusId >= MinLimitStatusView && e.ExaminationStatusId <= MaxLimitStatusView));

            if (examinationStatus != null && examinationStatus != 0)
            {
                examinations = examinations.Where(e => e.ExaminationStatusId == examinationStatus);
            }
            if (workPlace != null && workPlace != 0)
            {
                examinations = examinations.Where(s => s.Сustomer.WorkPlaceId == workPlace);
            }
            if (dateExamination != null && dateExamination != new DateTime())
            {
                examinations = examinations.Where(s => s.DateExamination.Value.Date == dateExamination.Date);
            }
            if (!String.IsNullOrEmpty(firstName))
            {
                examinations = examinations.Where(p => p.Сustomer.FirstName.Contains(firstName));
            }

            // сортировка
            switch (sortOrder)
            {
                case ExaminationSortState.DateExaminationAsc:
                    examinations = examinations.OrderBy(s => s.DateExamination);
                    break;
                default:
                    examinations = examinations.OrderByDescending(s => s.DateExamination);
                    break;
            }
            // пагинация
            var count = await examinations.CountAsync();
            var items = await examinations.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            ExaminationListViewModel viewModel = new ExaminationListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new ExaminationSortViewModel(sortOrder),
                FilterViewModel = new ExaminationFilterViewModel(WorkPlace.WorkPlaceList(db), workPlace,
                                                                    ExaminationStatus.ExaminationStatusList(db), examinationStatus,
                                                                    dateExamination, firstName),
                Examinations = items
            };

            return View(viewModel);
        }


        public async Task<IActionResult> ExaminationChangeStatus(int examinationId, int statusId)
        {
            Examination examination = await db.Examinations.FirstOrDefaultAsync(e => e.ExaminationId == examinationId);
            if (examination != null)
            {
                examination.ExaminationStatusId = statusId + 1;
                await db.SaveChangesAsync();

                ExaminationHistory examinationHistory = new ExaminationHistory
                {
                    DateChangeStatus = DateTime.Now,
                    ExaminationId = examinationId,
                    ExaminationStatusId = statusId + 1,
                    UserId = userId,
                };
                db.ExaminationHistorys.Add(examinationHistory);
                await db.SaveChangesAsync();
            }
            return Redirect($"~/ProductionAreas/ProductionAreas/ExaminationList");
        }

    }
}