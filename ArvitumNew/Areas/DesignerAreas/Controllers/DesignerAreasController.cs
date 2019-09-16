using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArvitumNew.Areas.DesignerAreas.Models;
using ArvitumNew.Helper;
using ArvitumNew.Models;
using ArvitumNew.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ArvitumNew.Areas.DesignerAreas.Controllers
{
    [Area("DesignerAreas")]
    [Authorize(Roles = "admin, Модельер")]
    public class DesignerAreasController : Controller
    {
        private UserManager<User> _userManager;
        private ApplicationDbContext db;
        private string userId;
        private const int LimitStatusView = 4;
        private IQueryable<string> nameWorkPlace;
        private IQueryable<string> shoesTypeName;
        private IQueryable<decimal> size;
        private IQueryable<string> coatingTypeName;
        private IQueryable<string> coatingThicknessName;

        public DesignerAreasController(UserManager<User> userManager, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            db = context;

            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            nameWorkPlace = from p in db.WorkPlaces select p.NameWorkPlace;
            shoesTypeName = from p in db.ShoesTypes select p.ShoesTypeName;
            size = from p in db.ShoesSizes select p.Size;
            coatingTypeName = from p in db.CoatingTypes select p.CoatingTypeName;
            coatingThicknessName = from p in db.CoatingThicknesss select p.CoatingThicknessName;
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
            examinations = examinations.Where(e => e.ExaminationStatusId <= LimitStatusView);

            if (examinationStatus!= null && examinationStatus != 0)
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

        public async Task<IActionResult> ExaminationEdit(int examinationId)
        {
            ExaminationCreateViewModel model = new ExaminationCreateViewModel();

            if (examinationId != 0)
            {
                Customer CurrentCustomer = await db.Customers.FirstOrDefaultAsync(p => p.CustomerId == (db.Examinations.FirstOrDefault(e => e.ExaminationId == examinationId).CustomerId));
                ViewBag.CurrentCustomer = CurrentCustomer;

                Examination examination = await db.Examinations.FirstOrDefaultAsync(p => p.ExaminationId == examinationId);

                model = new ExaminationCreateViewModel
                {
                    ExaminationId = examination.ExaminationId,
                    CustomerId = examination.CustomerId,
                    DateExamination = examination.DateExamination,
                    UserId = examination.UserId,
                    Reason = examination.Reason,
                    Diagnosis = examination.Diagnosis,
                    Note = examination.Note,
                    ShoesTypeName = db.ShoesTypes.Where(s => s.ShoesTypeId == examination.ShoesTypeId).FirstOrDefaultAsync().Result.ShoesTypeName,
                    Size = db.ShoesSizes.Where(s => s.ShoesSizeId == examination.ShoesSizeId).FirstOrDefaultAsync().Result.Size,
                    CoatingTypeName = db.CoatingTypes.Where(s => s.CoatingTypeId == examination.CoatingTypeId).FirstOrDefaultAsync().Result.CoatingTypeName,
                    CoatingThicknessName = db.CoatingThicknesss.Where(s => s.CoatingThicknessId == examination.CoatingThicknessId).FirstOrDefaultAsync().Result.CoatingThicknessName,
                    Activ = examination.Activ,
                    Express = examination.Express,
                };
            }
            ViewBag.ShoesTypeName = new SelectList(shoesTypeName);
            ViewBag.Size = new SelectList(size);
            ViewBag.CoatingTypeName = new SelectList(coatingTypeName);
            ViewBag.CoatingThicknessName = new SelectList(coatingThicknessName);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ExaminationEdit(ExaminationCreateViewModel model, string submit)
        {
            Customer CurrentCustomer = await db.Customers.FirstOrDefaultAsync(p => p.CustomerId == model.CustomerId);

            switch (submit)
            {
                case "Cansel":
                    {
                        return Redirect("~/DesignerAreas/DesignerAreas/ExaminationList");
                    }
                case "Save":
                    {
                        Examination examination;
                        if (ModelState.IsValid)
                        {
                            examination = await db.Examinations.FirstOrDefaultAsync(p => p.ExaminationId == model.ExaminationId);
                            if (examination != null)
                            {
                                examination.UserId = model.UserId;
                                examination.CustomerId = model.CustomerId;
                                examination.Reason = model.Reason;
                                examination.Diagnosis = model.Diagnosis;
                                examination.Note = model.Note;
                                examination.ShoesSizeId = db.ShoesSizes.Where(s => s.Size == model.Size).FirstOrDefaultAsync().Result.ShoesSizeId;
                                examination.ShoesTypeId = db.ShoesTypes.Where(s => s.ShoesTypeName == model.ShoesTypeName).FirstOrDefaultAsync().Result.ShoesTypeId;
                                examination.CoatingTypeId = db.CoatingTypes.Where(s => s.CoatingTypeName == model.CoatingTypeName).FirstOrDefaultAsync().Result.CoatingTypeId;
                                examination.CoatingThicknessId = db.CoatingThicknesss.Where(s => s.CoatingThicknessName == model.CoatingThicknessName).FirstOrDefaultAsync().Result.CoatingThicknessId;
                                examination.Express = model.Express;
                                examination.Activ = true;
                                await db.SaveChangesAsync();
                            }
                            return Redirect("~/DesignerAreas/DesignerAreas/ExaminationList");
                        }
                        ViewBag.CurrentCustomer = CurrentCustomer;
                        ViewBag.informationChannels = new SelectList(shoesTypeName);
                        ViewBag.informationChannels = new SelectList(size);
                        ViewBag.informationChannels = new SelectList(coatingTypeName);
                        ViewBag.informationChannels = new SelectList(coatingThicknessName);
                        return View(model);
                    }
                case "Foto":
                    {
                        ExaminationBottomPhoto examinationBottomPhoto = await db.ExaminationBottomPhotos.FirstOrDefaultAsync(p => p.ExaminationId == model.ExaminationId);
                        if (examinationBottomPhoto == null)
                        {
                            examinationBottomPhoto = new ExaminationBottomPhoto
                            {
                                ExaminationId = model.ExaminationId,
                                ShoesSizeLId = 1,
                                ShoesSizeRId = 1,
                                LLX = 10,
                                LLY = 320,
                                LRX = 220,
                                LRY = 320,
                                LTX = 110,
                                LTY = 10,
                                LDX = 110,
                                LDY = 620,
                                RLX = 10,
                                RLY = 320,
                                RRX = 220,
                                RRY = 320,
                                RTX = 110,
                                RTY = 10,
                                RDX = 110,
                                RDY = 620,
                            };
                            db.ExaminationBottomPhotos.Add(examinationBottomPhoto);
                            await db.SaveChangesAsync();
                        }
                        return Redirect($"~/DesignerAreas/DesignerAreas/ExaminationBottom?examinationId={model.ExaminationId}");
                    }
            }
            ViewBag.CurrentCustomer = CurrentCustomer;
            ViewBag.informationChannels = new SelectList(shoesTypeName);
            ViewBag.informationChannels = new SelectList(size);
            ViewBag.informationChannels = new SelectList(coatingTypeName);
            ViewBag.informationChannels = new SelectList(coatingThicknessName);
            return View(model);
        }

        public async Task<IActionResult> ExaminationBottom(int examinationId)
        {
            ExaminationBottomPhoto examinationBottomPhoto = await db.ExaminationBottomPhotos.FirstOrDefaultAsync(p => p.ExaminationId == examinationId);

            ExaminationBottomViewModel model = new ExaminationBottomViewModel
            {
                ExaminationId = examinationId,
                FileNameLeft = examinationBottomPhoto.FileNameLeft,
                TitleLeft = examinationBottomPhoto.TitleLeft,
                ImageDataLeft = examinationBottomPhoto.ImageDataLeft,
                FileNameRight = examinationBottomPhoto.FileNameRight,
                TitleRight = examinationBottomPhoto.TitleRight,
                ImageDataRight = examinationBottomPhoto.ImageDataRight,
                ShoesSizeLId = examinationBottomPhoto.ShoesSizeLId,
                ShoesSizeRId = examinationBottomPhoto.ShoesSizeRId,
                LLX = examinationBottomPhoto.LLX,
                LLY = examinationBottomPhoto.LLY,
                LRX = examinationBottomPhoto.LRX,
                LRY = examinationBottomPhoto.LRY,
                LTX = examinationBottomPhoto.LTX,
                LTY = examinationBottomPhoto.LTY,
                LDX = examinationBottomPhoto.LDX,
                LDY = examinationBottomPhoto.LDY,
                RLX = examinationBottomPhoto.RLX,
                RLY = examinationBottomPhoto.RLY,
                RRX = examinationBottomPhoto.RRX,
                RRY = examinationBottomPhoto.RRY,
                RTX = examinationBottomPhoto.RTX,
                RTY = examinationBottomPhoto.RTY,
                RDX = examinationBottomPhoto.RDX,
                RDY = examinationBottomPhoto.RDY,
            };
            ViewBag.ShoesSizeLName = db.ShoesSizes.Where(s => s.ShoesSizeId == model.ShoesSizeLId).FirstOrDefault().Size;
            ViewBag.ShoesSizeRName = db.ShoesSizes.Where(s => s.ShoesSizeId == model.ShoesSizeRId).FirstOrDefault().Size;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ExaminationBottom(ExaminationBottomViewModel model, IFormFile FormFileStreamLeft, IFormFile FormFileStreamRight, string submit)
        {
            decimal LeghtL, WidthL, LeghtR, WidthR;

            switch (submit)
            {
                case "Calc":
                    {
                        model.ImageDataLeft = db.ExaminationBottomPhotos.FirstOrDefaultAsync(e => e.ExaminationId == model.ExaminationId).Result.ImageDataLeft;
                        model.ImageDataRight = db.ExaminationBottomPhotos.FirstOrDefaultAsync(e => e.ExaminationId == model.ExaminationId).Result.ImageDataRight;
                        ImageInfo.CalcDistance(db, model, out LeghtL, out WidthL, out LeghtR, out WidthR);

                        IQueryable<ShoesSize> shoesSizeL = db.ShoesSizes.Where(s => s.FootLength >= MyMath.Round(LeghtL));
                        if (await shoesSizeL.CountAsync() > 0)
                        {
                            var items = await shoesSizeL.ToListAsync();
                            model.ShoesSizeLId = items.FirstOrDefault().ShoesSizeId;

                            ExaminationBottomPhoto examinationBottomPhoto = await db.ExaminationBottomPhotos.FirstOrDefaultAsync(p => p.ExaminationId == model.ExaminationId);
                            if (examinationBottomPhoto != null)
                            {
                                examinationBottomPhoto.ShoesSizeLId = model.ShoesSizeLId;
                                await db.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            model.ShoesSizeLId = 1;
                        }
                        IQueryable<ShoesSize> shoesSizeR = db.ShoesSizes.Where(s => s.FootLength >= MyMath.Round(LeghtR));
                        if (await shoesSizeR.CountAsync() > 0)
                        {
                            var items = await shoesSizeR.ToListAsync();
                            model.ShoesSizeRId = items.FirstOrDefault().ShoesSizeId;

                            ExaminationBottomPhoto examinationBottomPhoto = await db.ExaminationBottomPhotos.FirstOrDefaultAsync(p => p.ExaminationId == model.ExaminationId);
                            if (examinationBottomPhoto != null)
                            {
                                examinationBottomPhoto.ShoesSizeRId = model.ShoesSizeRId;
                                await db.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            model.ShoesSizeRId = 1;
                        }

                        ViewBag.ShoesSizeLName = db.ShoesSizes.Where(s => s.ShoesSizeId == model.ShoesSizeLId).FirstOrDefault().Size;
                        ViewBag.ShoesSizeRName = db.ShoesSizes.Where(s => s.ShoesSizeId == model.ShoesSizeRId).FirstOrDefault().Size;
                        ViewBag.LeghtL = LeghtL;
                        ViewBag.WidthL = WidthL;
                        ViewBag.LeghtR = LeghtR;
                        ViewBag.WidthR = WidthR;
                        return View(model);
                    }
                case "Save":
                    {
                        if (ModelState.IsValid)
                        {
                            ExaminationBottomPhoto examinationBottomPhoto = await db.ExaminationBottomPhotos.FirstOrDefaultAsync(p => p.ExaminationId == model.ExaminationId);
                            if (examinationBottomPhoto != null)
                            {
                                examinationBottomPhoto.LLX = model.LLX;
                                examinationBottomPhoto.LLY = model.LLY;
                                examinationBottomPhoto.LRX = model.LRX;
                                examinationBottomPhoto.LRY = model.LRY;
                                examinationBottomPhoto.LTX = model.LTX;
                                examinationBottomPhoto.LTY = model.LTY;
                                examinationBottomPhoto.LDX = model.LDX;
                                examinationBottomPhoto.LDY = model.LDY;
                                examinationBottomPhoto.RLX = model.RLX;
                                examinationBottomPhoto.RLY = model.RLY;
                                examinationBottomPhoto.RRX = model.RRX;
                                examinationBottomPhoto.RRY = model.RRY;
                                examinationBottomPhoto.RTX = model.RTX;
                                examinationBottomPhoto.RTY = model.RTY;
                                examinationBottomPhoto.RDX = model.RDX;
                                examinationBottomPhoto.RDY = model.RDY;

                                await db.SaveChangesAsync();

                                return Redirect($"~/DesignerAreas/DesignerAreas/ExaminationEdit?examinationId={model.ExaminationId}");
                            }
                        }
                        return View(model);
                    }
                case "Cansel":
                    {
                        return Redirect($"~/DesignerAreas/DesignerAreas/ExaminationEdit?examinationId={model.ExaminationId}");
                    }
            }
            return View(model);
        }

        public async Task<IActionResult> ExaminationDownload(int examinationId)
        {
            Examination examination = await db.Examinations.FirstOrDefaultAsync(e => e.ExaminationId == examinationId);
            if (examination != null)
            {
                if(examination.ExaminationStatusId==3)
                {
                    if (Download.DownloadExamination(db, examinationId) == true)
                    {
                        examination.ExaminationStatusId = 4;
                        await db.SaveChangesAsync();

                        ExaminationHistory examinationHistory = new ExaminationHistory
                        {
                            DateChangeStatus = DateTime.Now,
                            ExaminationId = examinationId,
                            ExaminationStatusId = 4,
                            UserId = userId,
                        };
                        db.ExaminationHistorys.Add(examinationHistory);
                        await db.SaveChangesAsync();
                    }
                }
            }
            return Redirect($"~/DesignerAreas/DesignerAreas/ExaminationList");
        }

        public async Task<IActionResult> ExaminationMake(int examinationId)
        {
            Examination examination = await db.Examinations.FirstOrDefaultAsync(e => e.ExaminationId == examinationId);
            if (examination != null)
            {
                if (examination.ExaminationStatusId == 4)
                {
                    examination.ExaminationStatusId = 5;
                    await db.SaveChangesAsync();

                    ExaminationHistory examinationHistory = new ExaminationHistory
                    {
                        DateChangeStatus = DateTime.Now,
                        ExaminationId = examinationId,
                        ExaminationStatusId = 5,
                        UserId = userId,
                    };
                    db.ExaminationHistorys.Add(examinationHistory);
                    await db.SaveChangesAsync();
                }
            }
            return Redirect($"~/DesignerAreas/DesignerAreas/ExaminationList");
        }
    }
}