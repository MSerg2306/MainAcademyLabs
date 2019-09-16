using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArvitumNew.Areas.ExaminationAreas.Models;
using ArvitumNew.Helper;
using ArvitumNew.Models;
using ArvitumNew.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ArvitumNew.Areas.ExaminationAreas.Controllers
{
    [Area("ExaminationAreas")]
    [Authorize(Roles = "admin, Дилер")]
    public class ExaminationAreasController : Controller
    {
        private UserManager<User> _userManager;
        private ApplicationDbContext db;
        private IQueryable<string> nameWorkPlace;
        private string userId;
        private int idWP;
        private int typeWorkPlase;
        private IQueryable<string> shoesTypeName;
        private IQueryable<decimal> size;
        private IQueryable<string> coatingTypeName;
        private IQueryable<string> coatingThicknessName;
        private IQueryable<ShoesSize> shoesSize;

        public ExaminationAreasController(UserManager<User> userManager, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            db = context;

            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            idWP = db.Users.Where(p => p.Id == userId).FirstOrDefault().WorkPlaceId;
            typeWorkPlase = db.WorkPlaces.Where(w => w.WorkPlaceId == idWP).FirstOrDefault().TypeWorkPlaceId;
            
            //фильтрация доступных мед.центров по пользователю
            if (idWP == 1)
            {
                nameWorkPlace = from p in db.WorkPlaces select p.NameWorkPlace;
            }
            else
            {
                nameWorkPlace = from p in db.WorkPlaces where p.WorkPlaceId == idWP select p.NameWorkPlace;
            }

            shoesTypeName = from p in db.ShoesTypes select p.ShoesTypeName;
            size = from p in db.ShoesSizes where p.Activ == true select p.Size;
            coatingTypeName = from p in db.CoatingTypes select p.CoatingTypeName;
            coatingThicknessName = from p in db.CoatingThicknesss select p.CoatingThicknessName;

            shoesSize = db.ShoesSizes.Where(s => s.Activ == true);

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ExaminationList(int? currentCustomerId, int? examinationStatus, int? workPlace, int? customerId, DateTime dateExamination, string fullName, int page = 1, ExaminationSortState sortOrder = ExaminationSortState.DateExaminationDesc)
        {
            int pageSize = 10;

            string sSQL = "SELECT dbo.WorkPlaces.WorkPlaceId, dbo.WorkPlaces.NameWorkPlace, dbo.Customers.FirstName, dbo.AspNetUsers.Id, dbo.AspNetUsers.UserName, dbo.Examinations.* ";
            sSQL += "FROM dbo.Customers INNER JOIN ";
            sSQL += "dbo.Examinations ON dbo.Customers.CustomerId = dbo.Examinations.CustomerId INNER JOIN ";
            sSQL += "dbo.WorkPlaces ON dbo.Customers.WorkPlaceId = dbo.WorkPlaces.WorkPlaceId INNER JOIN ";
            sSQL += "dbo.AspNetUsers ON dbo.Examinations.UserId = dbo.AspNetUsers.Id ";
            sSQL += "WHERE(dbo.Customers.Activ = 1) AND (dbo.Examinations.Activ = 1)";

            IQueryable<Examination> examinations;
            examinations = db.Examinations.Include(p => p.Сustomer);
            examinations = examinations.Include(p => p.Сustomer.WorkPlace);
            examinations = examinations.Include(p => p.User);
            examinations = examinations.Include(p => p.ExaminationStatus);


            if (currentCustomerId != null && currentCustomerId != 0)
                examinations = examinations.Where(p => p.Сustomer.CustomerId == currentCustomerId);

            //фильтрация глобального списка обследований по пользователю
            if (idWP != 1) //admin
            {
                examinations = examinations.Where(p => p.Сustomer.WorkPlaceId == idWP);
                examinations = examinations.Where(p => p.Activ == true);
            }

            //фильтрация
            if (examinationStatus != null && examinationStatus != 0)
            {
                examinations = examinations.Where(e => e.ExaminationStatusId == examinationStatus);
            }
            if (workPlace != null && workPlace != 0)
            {
                examinations = examinations.Where(s => s.Сustomer.WorkPlaceId == workPlace);
            }
            if (customerId != null && customerId != 0)
            {
                examinations = examinations.Where(s => s.Сustomer.CustomerId == customerId);
            }
            if (dateExamination != null && dateExamination != new DateTime())
            {
                examinations = examinations.Where(s => s.DateExamination.Value.Date == dateExamination.Date);
            }
            if (!String.IsNullOrEmpty(fullName))
            {
                examinations = examinations.Where(p => p.Сustomer.FullName.Contains(fullName));
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
                CurrentCustomerId = currentCustomerId ?? 0,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new ExaminationSortViewModel(sortOrder),
                FilterViewModel = new ExaminationFilterViewModel(WorkPlace.WorkPlaceList(db), workPlace, customerId, dateExamination, fullName),
                Examinations = items
            };

            ViewBag.currentCustomerId = currentCustomerId;
            return View(viewModel);
        }

        public async Task<IActionResult> ExaminationCreate(int? currentCustomerId, int? examinationId)
        {
            ExaminationCreateViewModel model = new ExaminationCreateViewModel();

            if (currentCustomerId != null && currentCustomerId != 0)
            { 
                Customer CurrentCustomer = await db.Customers.FirstOrDefaultAsync(p => p.CustomerId == currentCustomerId);
                ViewBag.CurrentCustomer = CurrentCustomer;

                model = new ExaminationCreateViewModel
                {
                    CustomerId = currentCustomerId ?? 0,
                    UserId = userId,
                    ShoesTypeName = db.ShoesTypes.Where(s=>s.ShoesTypeId==1).FirstOrDefaultAsync().Result.ShoesTypeName,
                    Size = 0,
                    CoatingTypeName = db.CoatingTypes.Where(s => s.CoatingTypeId == 1).FirstOrDefaultAsync().Result.CoatingTypeName,
                    CoatingThicknessName = db.CoatingThicknesss.Where(s => s.CoatingThicknessId == 1).FirstOrDefaultAsync().Result.CoatingThicknessName,
                    Express = false,
                    Activ = true,
                };
            }
            if (examinationId != null && examinationId != 0)
            {
                Customer CurrentCustomer = await db.Customers.FirstOrDefaultAsync(p => p.CustomerId == (db.Examinations.FirstOrDefault(e => e.ExaminationId == examinationId).CustomerId));
                ViewBag.CurrentCustomer = CurrentCustomer;

                Examination examination = await db.Examinations.FirstOrDefaultAsync(p => p.ExaminationId == (examinationId ?? 0));

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
            ViewBag.typeWorkPlase = typeWorkPlase;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ExaminationCreate(ExaminationCreateViewModel model, string submit)
        {
            switch (submit)
            { 
                case "Cansel":
                    {
                        return Redirect("~/ExaminationAreas/ExaminationAreas/ExaminationList");
                    }
                case "Delete":
                    {
                        if (ModelState.IsValid)
                        {
                            Examination examination1 = await db.Examinations.FirstOrDefaultAsync(p => p.ExaminationId == model.ExaminationId);
                            if (examination1 != null)
                            {
                                if(examination1.ExaminationStatusId==1)
                                { 
                                    examination1.Activ = false;
                                    var result = await db.SaveChangesAsync();
                                    if (result > 0)
                                    {
                                        return RedirectToAction("ExaminationList");
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("All", "Неизвестная ошибка удаления обследования...");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("All", "Удалять можно только не оплаченные обследования...");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("All", "Неверные данные текущего обследования...");
                            }
                        }
                        Customer CurrentCustomer1 = await db.Customers.FirstOrDefaultAsync(p => p.CustomerId == model.CustomerId);
                        ViewBag.CurrentCustomer = CurrentCustomer1;
                        ViewBag.informationChannels = new SelectList(shoesTypeName);
                        ViewBag.informationChannels = new SelectList(size);
                        ViewBag.informationChannels = new SelectList(coatingTypeName);
                        ViewBag.informationChannels = new SelectList(coatingThicknessName);
                        return View(model);
                    }
            }

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
                else
                {
                    examination = new Examination
                    {
                        CustomerId = model.CustomerId,
                        UserId = model.UserId,
                        Reason = model.Reason,
                        Diagnosis = model.Diagnosis,
                        Note = model.Note,
                        ShoesSizeId = db.ShoesSizes.Where(s => s.Size == model.Size).FirstOrDefaultAsync().Result.ShoesSizeId,
                        ShoesTypeId = db.ShoesTypes.Where(s => s.ShoesTypeName == model.ShoesTypeName).FirstOrDefaultAsync().Result.ShoesTypeId,
                        CoatingTypeId = db.CoatingTypes.Where(s => s.CoatingTypeName == model.CoatingTypeName).FirstOrDefaultAsync().Result.CoatingTypeId,
                        CoatingThicknessId = db.CoatingThicknesss.Where(s => s.CoatingThicknessName == model.CoatingThicknessName).FirstOrDefaultAsync().Result.CoatingThicknessId,
                        Express = false,
                        Activ = true,
                    };
                    db.Examinations.Add(examination);
                    await db.SaveChangesAsync();
                    model.ExaminationId = examination.ExaminationId;
                }

                switch (submit)
                    {
                    case "Pay":
                        {
                            examination = await db.Examinations.FirstOrDefaultAsync(p => p.ExaminationId == model.ExaminationId);
                            if (examination != null)
                            {
                                IQueryable<WorkPlace> workPlace;
                                workPlace = db.WorkPlaces.Include(w => w.TypeWorkPlace);
                                workPlace = workPlace.Include(p => p.TypeWorkPlace.PayType);
                                var examinationStatusId = workPlace.FirstOrDefaultAsync(w => w.WorkPlaceId == idWP).Result.TypeWorkPlace.PayType.ExaminationStatusId;
                                examination.ExaminationStatusId = examinationStatusId;
                                await db.SaveChangesAsync();

                                ExaminationHistory examinationHistory = new ExaminationHistory
                                {
                                    DateChangeStatus = DateTime.Now,
                                    ExaminationId = model.ExaminationId,
                                    ExaminationStatusId = examinationStatusId,
                                    UserId = userId,
                                };
                                db.ExaminationHistorys.Add(examinationHistory);
                                await db.SaveChangesAsync();
                            }
                            return Redirect("~/ExaminationAreas/ExaminationAreas/ExaminationList");
                        }
                    case "Save":
                        {
                            return Redirect("~/ExaminationAreas/ExaminationAreas/ExaminationList");
                        }
                    case "Foto":
                        {
                            ExaminationBottomPhoto examinationBottomPhoto = await db.ExaminationBottomPhotos.FirstOrDefaultAsync(p => p.ExaminationId == model.ExaminationId);
                            if (examinationBottomPhoto == null)
                            {
                                examinationBottomPhoto = new ExaminationBottomPhoto
                                {
                                        ExaminationId = model.ExaminationId,
                                        ShoesSizeLId=1,
                                        ShoesSizeRId=1,
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
                            return Redirect($"~/ExaminationAreas/ExaminationAreas/ExaminationBottom?examinationId={model.ExaminationId}");
                        }
                }
            }
            Customer CurrentCustomer = await db.Customers.FirstOrDefaultAsync(p => p.CustomerId == model.CustomerId);
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
                FileNameLeft= examinationBottomPhoto.FileNameLeft,
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
                case "UploadFoto":
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            if (FormFileStreamLeft != null)
                            {
                                await FormFileStreamLeft.CopyToAsync(memoryStream);
                                model.ImageDataLeft = memoryStream.ToArray();

                                ExaminationBottomPhoto examinationBottomPhoto = await db.ExaminationBottomPhotos.FirstOrDefaultAsync(p => p.ExaminationId == model.ExaminationId);
                                if (examinationBottomPhoto != null)
                                {
                                    examinationBottomPhoto.ImageDataLeft = model.ImageDataLeft;
                                    await db.SaveChangesAsync();
                                }
                            }
                        }
                        using (var memoryStream = new MemoryStream())
                        {
                            if (FormFileStreamRight != null)
                            {
                                await FormFileStreamRight.CopyToAsync(memoryStream);
                                model.ImageDataRight = memoryStream.ToArray();

                                ExaminationBottomPhoto examinationBottomPhoto = await db.ExaminationBottomPhotos.FirstOrDefaultAsync(p => p.ExaminationId == model.ExaminationId);
                                if (examinationBottomPhoto != null)
                                {
                                    examinationBottomPhoto.ImageDataRight = model.ImageDataRight;
                                    await db.SaveChangesAsync();
                                }
                            }
                        }
                        return View(model);
                    }
                case "Calc":
                    {
                        model.ImageDataLeft = db.ExaminationBottomPhotos.FirstOrDefaultAsync(e => e.ExaminationId == model.ExaminationId).Result.ImageDataLeft;
                        model.ImageDataRight = db.ExaminationBottomPhotos.FirstOrDefaultAsync(e => e.ExaminationId == model.ExaminationId).Result.ImageDataRight;
                        ImageInfo.CalcDistance(db, model, out LeghtL, out WidthL, out LeghtR, out WidthR);

                        IQueryable<ShoesSize> shoesSizeL = shoesSize.Where(s => s.FootLength >= MyMath.Round(LeghtL));
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
                        IQueryable<ShoesSize> shoesSizeR = shoesSize.Where(s => s.FootLength >= MyMath.Round(LeghtR));
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
                                //examinationBottomPhoto.FileNameLeft = model.FileNameLeft;
                                //examinationBottomPhoto.TitleLeft = model.TitleLeft;
                                //examinationBottomPhoto.ImageDataLeft = model.ImageDataLeft;
                                //examinationBottomPhoto.FileNameRight = model.FileNameRight;
                                //examinationBottomPhoto.TitleRight = model.TitleRight;
                                //examinationBottomPhoto.ImageDataRight = model.ImageDataRight;
                                //examinationBottomPhoto.ShoesSizeLId = model.ShoesSizeLId;
                                //examinationBottomPhoto.ShoesSizeRId = model.ShoesSizeRId;
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

                                return Redirect($"~/ExaminationAreas/ExaminationAreas/ExaminationCreate?examinationId={model.ExaminationId}");
                            }
                        }
                        return View(model);
                    }
                case "Cansel":
                    {
                        return Redirect($"~/ExaminationAreas/ExaminationAreas/ExaminationCreate?examinationId={model.ExaminationId}");
                    }
            }
            return View(model);
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
            return Redirect($"~/ExaminationAreas/ExaminationAreas/ExaminationList");
        }
    }
}