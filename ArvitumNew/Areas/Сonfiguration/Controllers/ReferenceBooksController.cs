using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArvitumNew.Areas.Сonfiguration.Models;
using ArvitumNew.Helper;
using ArvitumNew.Models;
using ArvitumNew.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ArvitumNew.Areas.Сonfiguration.Controllers
{
    [Area("Сonfiguration")]
    [Authorize(Roles = "admin")]
    public class ReferenceBooksController : Controller
    {
        UserManager<User> _userManager;
        ApplicationDbContext db;
        public ReferenceBooksController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            db = context;
        }

        public async Task<IActionResult> ShoesTypeList(int? activ, int page = 1, ShoesTypeSortState sortOrder = ShoesTypeSortState.ShoesTypeNameAsc)
        {
            int pageSize = 10;
            IQueryable<ShoesType> shoesTypes = db.ShoesTypes.Where(s=>s.ShoesTypeId!=1);

            //фильтрация
            if (activ != null && activ != 0)
            {
                if(activ < 0)
                    shoesTypes = shoesTypes.Where(s => s.Activ == false);
                else
                    shoesTypes = shoesTypes.Where(s => s.Activ == true);
            }

            // сортировка
            switch (sortOrder)
            {
                case ShoesTypeSortState.ShoesTypeNameDesc:
                    shoesTypes = shoesTypes.OrderByDescending(s => s.ShoesTypeName);
                    break;
                default:
                    shoesTypes = shoesTypes.OrderBy(s => s.ShoesTypeName);
                    break;
            }
            // пагинация
            var count = await shoesTypes.CountAsync();
            var items = await shoesTypes.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            ShoesTypeListViewModel viewModel = new ShoesTypeListViewModel
            {
                ShoesTypes = items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new ShoesTypeSortViewModel(sortOrder),
                FilterViewModel = new ActivFilterViewModel(Activ.ActivList(), activ),
                SelectedListViev = new SelectList(Activ.ActivList(), "ActivStutys", "ActivName")
            };

            return View(viewModel);
        }
        public IActionResult ShoesTypeCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ShoesTypeCreate(ShoesTypeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                ShoesType shoesType = new ShoesType { ShoesTypeName = model.ShoesTypeName, Activ = true };
                db.ShoesTypes.Add(shoesType);
                await db.SaveChangesAsync();
                return RedirectToAction("ShoesTypeList");
            }
            return View(model);
        }
        public async Task<IActionResult> ShoesTypeEdit(int? id)
        {
            if (id != null)
            {
                ShoesType shoesType = await db.ShoesTypes.FirstOrDefaultAsync(p => p.ShoesTypeId == id);
                if (shoesType != null)
                {
                    ShoesTypeEditViewModel model = new ShoesTypeEditViewModel
                    {
                        ShoesTypeId = shoesType.ShoesTypeId,
                        ShoesTypeName = shoesType.ShoesTypeName,
                        Activ = shoesType.Activ,
                    };
                    return View(model);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ShoesTypeEdit(ShoesTypeEditViewModel model, string submit)
        {
            switch (submit)
            {
                case "Edit":
                    {
                        if (ModelState.IsValid)
                        {
                            ShoesType shoesType = await db.ShoesTypes.FirstOrDefaultAsync(p => p.ShoesTypeId == model.ShoesTypeId);
                            if (shoesType != null)
                            {
                                shoesType.ShoesTypeName = model.ShoesTypeName;
                                shoesType.Activ = model.Activ;

                                var result = await db.SaveChangesAsync();
                                if (result > 0)
                                {
                                    return RedirectToAction("ShoesTypeList");
                                }
                                else
                                {
                                    return View(model);
                                }
                            }
                        }
                        return View(model);
                    }
                case "Delete":
                    {
                        if (ModelState.IsValid)
                        {
                            ShoesType shoesType = await db.ShoesTypes.FirstOrDefaultAsync(p => p.ShoesTypeId == model.ShoesTypeId);
                            if (shoesType != null)
                            {
                                shoesType.Activ = false;

                                var result = await db.SaveChangesAsync();
                                if (result > 0)
                                {
                                    return RedirectToAction("ShoesTypeList");
                                }
                                else
                                {
                                    return View(model);
                                }
                            }
                        }
                        return View(model);
                    }
                default:
                    {
                        return RedirectToAction("ShoesTypeList");
                    }
            }
        }

        public async Task<IActionResult> ShoesSizeList(int? activ, int page = 1, ShoesSizeSortState sortOrder = ShoesSizeSortState.FootLengthAsc)
        {
            int pageSize = 10;
            IQueryable<ShoesSize> shoesSize = db.ShoesSizes.Where(s => s.ShoesSizeId != 1);

            //фильтрация
            if (activ != null && activ != 0)
            {
                if (activ < 0)
                    shoesSize = shoesSize.Where(s => s.Activ == false);
                else
                    shoesSize = shoesSize.Where(s => s.Activ == true);
            }

            // сортировка
            switch (sortOrder)
            {
                case ShoesSizeSortState.FootLengthDesc:
                    shoesSize = shoesSize.OrderByDescending(s => s.FootLength);
                    break;
                case ShoesSizeSortState.SizeAsc:
                    shoesSize = shoesSize.OrderBy(s => s.Size);
                    break;
                case ShoesSizeSortState.SizeDesc:
                    shoesSize = shoesSize.OrderByDescending(s => s.Size);
                    break;
                default:
                    shoesSize = shoesSize.OrderBy(s => s.FootLength);
                    break;
            }
            // пагинация
            var count = await shoesSize.CountAsync();
            var items = await shoesSize.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            ShoesSizeListViewModel viewModel = new ShoesSizeListViewModel
            {
                ShoesSizes = items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new ShoesSizeSortViewModel(sortOrder),
                FilterViewModel = new ActivFilterViewModel(Activ.ActivList(), activ),
                SelectedListViev = new SelectList(Activ.ActivList(), "ActivStutys", "ActivName")
            };

            return View(viewModel);
        }
        public IActionResult ShoesSizeCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ShoesSizeCreate(ShoesSizeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                ShoesSize shoesSize = new ShoesSize {
                    FootLength = model.FootLength,
                    Size = model.Size,
                    Activ = true };
                db.ShoesSizes.Add(shoesSize);
                await db.SaveChangesAsync();
                return RedirectToAction("ShoesSizeList");
            }
            return View(model);
        }
        public async Task<IActionResult> ShoesSizeEdit(int? id)
        {
            if (id != null)
            {
                ShoesSize shoesSize = await db.ShoesSizes.FirstOrDefaultAsync(p => p.ShoesSizeId == id);
                if (shoesSize != null)
                {
                    ShoesSizeEditViewModel model = new ShoesSizeEditViewModel
                    {
                        ShoesSizeId = shoesSize.ShoesSizeId,
                        FootLength = shoesSize.FootLength,
                        Size = shoesSize.Size,
                        Activ = shoesSize.Activ,
                    };
                    return View(model);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ShoesSizeEdit(ShoesSizeEditViewModel model, string submit)
        {
            switch (submit)
            {
                case "Edit":
                    {
                        if (ModelState.IsValid)
                        {
                            ShoesSize shoesSize = await db.ShoesSizes.FirstOrDefaultAsync(p => p.ShoesSizeId == model.ShoesSizeId);
                            if (shoesSize != null)
                            {
                                shoesSize.FootLength = model.FootLength;
                                shoesSize.Size = model.Size;
                                shoesSize.Activ = model.Activ;

                                var result = await db.SaveChangesAsync();
                                if (result > 0)
                                {
                                    return RedirectToAction("ShoesSizeList");
                                }
                                else
                                {
                                    return View(model);
                                }
                            }
                        }
                        return View(model);
                    }
                case "Delete":
                    {
                        if (ModelState.IsValid)
                        {
                            ShoesSize shoesSize = await db.ShoesSizes.FirstOrDefaultAsync(p => p.ShoesSizeId == model.ShoesSizeId);
                            if (shoesSize != null)
                            {
                                shoesSize.Activ = false;

                                var result = await db.SaveChangesAsync();
                                if (result > 0)
                                {
                                    return RedirectToAction("ShoesSizeList");
                                }
                                else
                                {
                                    return View(model);
                                }
                            }
                        }
                        return View(model);
                    }
                default:
                    {
                        return RedirectToAction("ShoesSizeList");
                    }
            }
        }

        public async Task<IActionResult> InformationChannelList(int? activ, int page = 1, InformationChannelSortState sortOrder = InformationChannelSortState.InformationChannelNameAsc)
        {
            int pageSize = 10;
            IQueryable<InformationChannel> informationChannels = db.InformationChannels.Where(s => s.InformationChannelId != 1);

            //фильтрация
            if (activ != null && activ != 0)
            {
                if (activ < 0)
                    informationChannels = informationChannels.Where(s => s.Activ == false);
                else
                    informationChannels = informationChannels.Where(s => s.Activ == true);
            }

            // сортировка
            switch (sortOrder)
            {
                case InformationChannelSortState.InformationChannelNameDesc:
                    informationChannels = informationChannels.OrderByDescending(s => s.InformationChannelName);
                    break;
                default:
                    informationChannels = informationChannels.OrderBy(s => s.InformationChannelName);
                    break;
            }
            // пагинация
            var count = await informationChannels.CountAsync();
            var items = await informationChannels.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            InformationChannelListViewModel viewModel = new InformationChannelListViewModel
            {
                InformationChannels = items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new InformationChannelSortViewModel(sortOrder),
                FilterViewModel = new ActivFilterViewModel(Activ.ActivList(), activ),
                SelectedListViev = new SelectList(Activ.ActivList(), "ActivStutys", "ActivName")
            };

            return View(viewModel);
        }
        public IActionResult InformationChannelCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InformationChannelCreate(InformationChannelCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                InformationChannel informationChannel = new InformationChannel { InformationChannelName = model.InformationChannelName, Activ = true };
                db.InformationChannels.Add(informationChannel);
                await db.SaveChangesAsync();
                return RedirectToAction("InformationChannelList");
            }
            return View(model);
        }
        public async Task<IActionResult> InformationChannelEdit(int? id)
        {
            if (id != null)
            {
                InformationChannel informationChannel = await db.InformationChannels.FirstOrDefaultAsync(p => p.InformationChannelId == id);
                if (informationChannel != null)
                {
                    InformationChannelEditViewModel model = new InformationChannelEditViewModel
                    {
                        InformationChannelId = informationChannel.InformationChannelId,
                        InformationChannelName = informationChannel.InformationChannelName,
                        Activ = informationChannel.Activ,
                    };
                    return View(model);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> InformationChannelEdit(InformationChannelEditViewModel model, string submit)
        {
            switch (submit)
            {
                case "Edit":
                    {
                        if (ModelState.IsValid)
                        {
                            InformationChannel informationChannel = await db.InformationChannels.FirstOrDefaultAsync(p => p.InformationChannelId == model.InformationChannelId);
                            if (informationChannel != null)
                            {
                                informationChannel.InformationChannelName = model.InformationChannelName;
                                informationChannel.Activ = model.Activ;

                                var result = await db.SaveChangesAsync();
                                if (result > 0)
                                {
                                    return RedirectToAction("InformationChannelList");
                                }
                                else
                                {
                                    return View(model);
                                }
                            }
                        }
                        return View(model);
                    }
                case "Delete":
                    {
                        if (ModelState.IsValid)
                        {
                            InformationChannel informationChannel = await db.InformationChannels.FirstOrDefaultAsync(p => p.InformationChannelId == model.InformationChannelId);
                            if (informationChannel != null)
                            {
                                informationChannel.Activ = false;

                                var result = await db.SaveChangesAsync();
                                if (result > 0)
                                {
                                    return RedirectToAction("InformationChannelList");
                                }
                                else
                                {
                                    return View(model);
                                }
                            }
                        }
                        return View(model);
                    }
                default:
                    {
                        return RedirectToAction("InformationChannelList");
                    }
            }
        }

    }
}