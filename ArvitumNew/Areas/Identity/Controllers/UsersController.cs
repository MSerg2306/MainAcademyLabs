using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ArvitumNew.Areas.Identity.Models;
using ArvitumNew.Models;
using ArvitumNew.Util;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using ArvitumNew.Helper;

namespace ArvitumNew.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        ApplicationDbContext db;
        public UsersController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            db = context;
        }

        //[Authorize(Roles = "admin")]
        //public IActionResult Index()
        //{
        //    IQueryable<User> users = db.Users.Include(p => p.WorkPlace);
        //    return View(users);
        //}

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index(int? workPlace, string name, int page = 1, CustomerSortState sortOrder = CustomerSortState.UserNameAsc)
        {
            int pageSize = 10;
            IQueryable<User> users = db.Users.Include(p => p.WorkPlace);

            //фильтрация
            if (workPlace != null && workPlace != 0)
            {
                users = users.Where(s => s.WorkPlaceId == workPlace);
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.UserName.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case CustomerSortState.UserNameDesc:
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }
            // пагинация
            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            ListUserViewModel viewModel = new ListUserViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(WorkPlace.WorkPlaceList(db), workPlace, name),
                Users = items
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var nameWorkPlace = from p in db.WorkPlaces
                           select p.NameWorkPlace;
            ViewBag.workPlaces = new SelectList(nameWorkPlace);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Name,
                    WorkPlaceId = db.WorkPlaces.Where(b => b.NameWorkPlace == model.WorkPlace).FirstOrDefault().WorkPlaceId
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            var nameWorkPlace = from p in db.WorkPlaces
                                select p.NameWorkPlace;
            ViewBag.workPlaces = new SelectList(nameWorkPlace);
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var nameWorkPlace = from p in db.WorkPlaces
                                select p.NameWorkPlace;
            ViewBag.workPlaces = new SelectList(nameWorkPlace);

            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Name = user.UserName, Email = user.Email, WorkPlace = db.WorkPlaces.Where(p => p.WorkPlaceId == user.WorkPlaceId).FirstOrDefault().NameWorkPlace };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Name;
                    user.WorkPlaceId = db.WorkPlaces.Where(p => p.NameWorkPlace == model.WorkPlace).FirstOrDefault().WorkPlaceId;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Name = user.UserName, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ChangePersonPassword(string name)
        {
            User user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return NotFound();
            }
            //ChangePersonPasswordViewModel model = new ChangePersonPasswordViewModel { Id = user.Id, Name = user.UserName, Email = user.Email, EmailConfirmed = user.EmailConfirmed, Year = user.Year };
            ChangePersonPasswordViewModel model = new ChangePersonPasswordViewModel { Id = user.Id, Name = user.UserName, Email = user.Email, EmailConfirmed = user.EmailConfirmed };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePersonPassword(ChangePersonPasswordViewModel model, string ActionType, string outputEmail = null)
        {
            switch (ActionType)
            {
                case "Сохранить" :
                    {
                        if (ModelState.IsValid)
                        {
                            User user = await _userManager.FindByIdAsync(model.Id);
                            if (user != null)
                            {
                                user.Email = model.Email;
                                user.UserName = model.Name;
                                //user.Year = model.Year;
                                var result1 = await _userManager.UpdateAsync(user);

                                IdentityResult result2 =
                                    await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                                if (result1.Succeeded | result2.Succeeded)
                                {
                                    return LocalRedirect("~/Home/Index");
                                }
                                else
                                {
                                    foreach (var error in result1.Errors)
                                    {
                                        ModelState.AddModelError(string.Empty, error.Description);
                                    }
                                    foreach (var error in result2.Errors)
                                    {
                                        ModelState.AddModelError(string.Empty, error.Description);
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Пользователь не найден");
                            }
                        }
                        return View(model);
                    }
                case "Отправить" :
                    {
                        User user = await _userManager.FindByIdAsync(model.Id);

                        // генерация токена для пользователя
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action(
                            "ConfirmEmail",
                            "Users",
                            new { userId = user.Id, code = code },
                            protocol: HttpContext.Request.Scheme);
                        EmailService emailService = new EmailService();
                        await emailService.SendEmailAsync(model.Email, "Confirm your account",
                            $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                        return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                    }
                default:
                    {
                        return View(model);
                    }
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }
    }
}