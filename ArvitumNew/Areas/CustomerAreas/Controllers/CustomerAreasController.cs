using System;
using System.Linq;
using System.Threading.Tasks;
using ArvitumNew.Models;
using ArvitumNew.Areas.CustomerAreas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArvitumNew.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ArvitumNew.Areas.CustomerAreas.Controllers
{
    [Area("CustomerAreas")]
    [Authorize(Roles = "admin, Дилер")]
    public class CustomerAreasController : Controller
    {
        private UserManager<User> _userManager;
        private ApplicationDbContext db;
        private IQueryable<string> nameWorkPlace;
        private IQueryable<string> informationChannelName;
        private int idWP;
        public CustomerAreasController(UserManager<User> userManager, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            db = context;

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            idWP = db.Users.Where(p => p.Id == userId).FirstOrDefault().WorkPlaceId;

            //фильтрация доступных мед.центров по пользователю
            if (idWP == 1)
            {
                nameWorkPlace = from p in db.WorkPlaces select p.NameWorkPlace;
            }
            else
            {
                nameWorkPlace = from p in db.WorkPlaces where p.WorkPlaceId == idWP select p.NameWorkPlace;
            }

            informationChannelName = from p in db.InformationChannels select p.InformationChannelName;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CustomerList(int? workPlace, int? customerId, string fullName, int page = 1, СustomerSortState sortOrder = СustomerSortState.FullNameAsc)
        {
            int pageSize = 10;
            IQueryable<Customer> customers;

            //фильтрация глобального списка клиентов по пользователю
            if (idWP == 1) //admin
            {
                customers = db.Customers.Include(p => p.WorkPlace);
            }
            else
            {
                customers = db.Customers.Where(p => p.Activ == true);
                customers = customers.Where(p => p.WorkPlaceId == idWP).Include(p => p.WorkPlace);
            }

            //фильтрация
            if (workPlace != null && workPlace != 0)
            {
                customers = customers.Where(s => s.WorkPlaceId == workPlace);
            }
            if (customerId != null && customerId != 0)
            {
                customers = customers.Where(s => s.CustomerId == customerId);
            }
            if (!String.IsNullOrEmpty(fullName))
            {
                customers = customers.Where(p => p.FirstName.Contains(fullName));
            }

            // сортировка
            switch (sortOrder)
            {
                case СustomerSortState.FullNameDesc:
                    customers = customers.OrderByDescending(s => s.FullName);
                    break;
                default:
                    customers = customers.OrderBy(s => s.FullName);
                    break;
            }
            // пагинация
            var count = await customers.CountAsync();
            var items = await customers.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            CustomerListViewModel viewModel = new CustomerListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new ShoesTypeSortViewModel(sortOrder),
                FilterViewModel = new ShoesTypeFilterViewModel(WorkPlace.WorkPlaceList(db), workPlace, customerId, fullName),
                Customers = items
            };

            return View(viewModel);
        }

        public IActionResult CustomerCreate()
        {
            ViewBag.workPlaces = new SelectList(nameWorkPlace);
            ViewBag.informationChannels = new SelectList(informationChannelName);
            CustomerCreateViewModel model = new CustomerCreateViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CustomerCreate(CustomerCreateViewModel model)
        {
                if (ModelState.IsValid)
                {
                    try
                    { 
                        Customer customer = new Customer
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Birthday = model.Birthday,
                            Phone = model.Phone,
                            Email = model.Email,
                            Activ = true,
                            WorkPlaceId = db.WorkPlaces.Where(b => b.NameWorkPlace == model.NameWorkPlace).FirstOrDefault().WorkPlaceId,
                            InformationChannelId = db.InformationChannels.Where(b => b.InformationChannelName == model.InformationChannelName).FirstOrDefault().InformationChannelId,
                        };
                        db.Customers.Add(customer);
                        await db.SaveChangesAsync();
                        return RedirectToAction("CustomerList");
                    }
                    catch (Exception ex)
                    {
                        string e = ex.InnerException.Message;
                        if (e.IndexOf("Повторяющееся") != 0)
                        {
                            e = e.Substring(e.IndexOf("Повторяющееся"));
                        }

                        ModelState.AddModelError("All", e);
                    }
                }

            ViewBag.workPlaces = new SelectList(nameWorkPlace);
            ViewBag.informationChannels = new SelectList(informationChannelName);
            return View(model);
        }
        public async Task<IActionResult> CustomerEdit (int? id)
        {
            if (id != null)
            {
                Customer customer = await db.Customers.FirstOrDefaultAsync(p => p.CustomerId == id);
                if (customer != null)
                { 
                    ViewBag.workPlaces = new SelectList(nameWorkPlace);
                    ViewBag.informationChannels = new SelectList(informationChannelName);
                    CustomerEditViewModel model = new CustomerEditViewModel {
                        Id = customer.CustomerId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Birthday = customer.Birthday ?? new DateTime(),
                        Phone = customer.Phone,
                        Email = customer.Email,
                        Activ = customer.Activ,
                        NameWorkPlace = db.WorkPlaces.Where(p => p.WorkPlaceId == customer.WorkPlaceId).FirstOrDefault().NameWorkPlace,
                        InformationChannelName = db.InformationChannels.Where(p => p.InformationChannelId == customer.InformationChannelId).FirstOrDefault().InformationChannelName
                    };
                    return View(model);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CustomerEdit(CustomerEditViewModel model, string submit)
        {
            switch (submit)
            { 
                case "Edit":
                    {
                        if (ModelState.IsValid)
                        {
                            Customer customer = await db.Customers.FirstOrDefaultAsync(p => p.CustomerId == model.Id);
                            if (customer != null)
                            {
                                customer.FirstName = model.FirstName;
                                customer.LastName = model.LastName;
                                customer.Birthday = model.Birthday;
                                customer.Phone = model.Phone;
                                customer.Email = model.Email;
                                customer.Activ = model.Activ;
                                customer.WorkPlaceId = db.WorkPlaces.Where(b => b.NameWorkPlace == model.NameWorkPlace).FirstOrDefault().WorkPlaceId;
                                customer.InformationChannelId = db.InformationChannels.Where(b => b.InformationChannelName == model.InformationChannelName).FirstOrDefault().InformationChannelId;

                                var result = await db.SaveChangesAsync();
                                if (result > 0)
                                {
                                    return RedirectToAction("CustomerList");
                                }
                                else
                                {
                                    ViewBag.workPlaces = new SelectList(nameWorkPlace);
                                    ViewBag.informationChannels = new SelectList(informationChannelName);
                                    return View(model);
                                }
                            }
                        }
                        ViewBag.workPlaces = new SelectList(nameWorkPlace);
                        ViewBag.informationChannels = new SelectList(informationChannelName);
                        return View(model);
                    }
                case "Delete":
                    {
                        if (ModelState.IsValid)
                        {
                            Customer customer = await db.Customers.FirstOrDefaultAsync(p => p.CustomerId == model.Id);
                            if (customer != null)
                            {
                                customer.Activ=false;

                                var result = await db.SaveChangesAsync();
                                if (result > 0)
                                {
                                    return RedirectToAction("CustomerList");
                                }
                                else
                                {
                                    ViewBag.workPlaces = new SelectList(nameWorkPlace);
                                    ViewBag.informationChannels = new SelectList(informationChannelName);
                                    return View(model);
                                }
                            }
                        }
                        ViewBag.workPlaces = new SelectList(nameWorkPlace);
                        ViewBag.informationChannels = new SelectList(informationChannelName);
                        return View(model);
                    }
                default:
                    {
                        return RedirectToAction("CustomerList");
                    }
            }
        }
    }
}
