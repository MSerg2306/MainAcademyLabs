using ArvitumNew.Areas.Сonfiguration.Models;
using ArvitumNew.Helper;
using ArvitumNew.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArvitumNew.Areas.Сonfiguration.Controllers
{
    [Area("Сonfiguration")]
    [Authorize(Roles = "admin")]
    public class CityWorkPlaceController : Controller
    {
        UserManager<User> _userManager;
        ApplicationDbContext db;
        public CityWorkPlaceController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            db = context;
        }

        public async Task<IActionResult> Index(int? city, string name, int page = 1, SortState sortOrder = SortState.WorkPlaceNameAsc)
        {
            int pageSize = 10;
            IQueryable<WorkPlace> workPlaces = db.WorkPlaces.Include(p => p.City);

            //фильтрация
            if (city != null && city != 0)
            {
                workPlaces = workPlaces.Where(s => s.CityId == city);
            }
            if (!String.IsNullOrEmpty(name))
            {
                workPlaces = workPlaces.Where(p => p.NameWorkPlace.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.WorkPlaceNameDesc:
                    workPlaces = workPlaces.OrderByDescending(s => s.NameWorkPlace);
                    break;
                default:
                    workPlaces = workPlaces.OrderBy(s => s.NameWorkPlace);
                    break;
            }
            // пагинация
            var count = await workPlaces.CountAsync();
            var items = await workPlaces.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            CityWorkPlaceViewModel viewModel = new CityWorkPlaceViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(City.CityList(db), city, name),
                WorkPlaces = items
            };

            return View(viewModel);
        }

        public IActionResult CreateCity()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCity(CreateCityViewModel model)
        {
            if (ModelState.IsValid)
            {
                City city = new City { NameCity = model.NameCity };
                db.Citys.Add(city);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);

        }

        public IActionResult CreateWorkPlace()
        {
            var nameCity = from p in db.Citys select p.NameCity;
            ViewBag.citys = new SelectList(nameCity);

            var nameTypeWorkPlace = from p in db.TypeWorkPlaces select p.NameTypeWorkPlace;
            ViewBag.typeWorkPlace = new SelectList(nameTypeWorkPlace);

            CreateWorkPlaceViewModel model = new CreateWorkPlaceViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWorkPlace(CreateWorkPlaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                WorkPlace workPlace = new WorkPlace
                {
                    NameWorkPlace = model.NameWorkPlace,
                    Address = model.Address,
                    CityId = db.Citys.Where(b => b.NameCity == model.NameCity).FirstOrDefault().CityId,
                    TypeWorkPlaceId = db.TypeWorkPlaces.Where(b => b.NameTypeWorkPlace == model.NameTypeWorkPlace).FirstOrDefault().TypeWorkPlaceId,
                    LocalPathDll = model.LocalPathDll,
                    LacalPathImage = model.LacalPathImage,
                };
                db.WorkPlaces.Add(workPlace);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var nameCity = from p in db.Citys select p.NameCity;
            ViewBag.citys = new SelectList(nameCity);

            var nameTypeWorkPlace = from p in db.TypeWorkPlaces select p.NameTypeWorkPlace;
            ViewBag.typeWorkPlace = new SelectList(nameTypeWorkPlace);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                WorkPlace workPlace = await db.WorkPlaces.FirstOrDefaultAsync(p => p.WorkPlaceId == id);
                if (workPlace != null)
                {
                    db.WorkPlaces.Remove(workPlace);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}