using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TayarDelivery.Helper;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Bookstore.Data;
using Bookstore.Helper;
using Bookstore.Models.ViewModels;
using Bookstore.Models.Entities;

namespace Bookstore.Areas.Admin.Controllers
{
    [Authorize]
    public class BookPublishHouseController : BaseController
    {
        public BookPublishHouseController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper) : base(context, userManager, roleManager, mapper)
        {
        }

        [HttpPost]
        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var query = await _context.BookPublishHouse
                .Where(x => (d.SearchKey == null || x.Name.Contains(d.SearchKey) || x.Address.Contains(d.SearchKey)))
                .OrderBy(x => x.CreateAt)
                .ToListAsync();

            int totalCount = query.Count();

            var items = query.Select(x => new
            {
                x.Id,
                x.Name,
                x.Address,
                createAt = x.CreateAt.Value.ToShortDateString(),
            }).Skip(d.Start).Take(d.Length).ToList();
            var result =
               new
               {
                   draw = d.Draw,
                   recordsTotal = totalCount,
                   recordsFiltered = totalCount,
                   data = items
               };
            return Json(result);
        }

        // GET: Admin/Area/Index
        public IActionResult Index()
        {
            return View();
        }


        // GET: Admin/Area/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Area/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookPublishHouseVM model)
        {
            if (ModelState.IsValid)
            {
                var newEntity = _mapper.Map<BookPublishHouse>(model);
                var result = await _context.BookPublishHouse.AddAsync(newEntity);
                await _context.SaveChangesAsync();
                if (result != null)
                    return Content(ShowMessage.AddSuccessResult(), "application/json");
                return Content(ShowMessage.FailedResult(), "application/json");
            }
            return View(model);
        }

        // GET: Admin/Area/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _context.BookPublishHouse.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var newEntity = _mapper.Map<BookPublishHouseVM>(model);
            return View(newEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookPublishHouseVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var baseEntoty = await _context.BookPublishHouse.FindAsync(id);
                    PropertyCopy.Copy(model, baseEntoty);
                    _context.BookPublishHouse.Update(baseEntoty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AreaExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Content(ShowMessage.EditSuccessResult(), "application/json");
            }
            return View(model);
        }

        // GET: Admin/Area/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _context.BookPublishHouse.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var result = _context.BookPublishHouse.Remove(model);
            await _context.SaveChangesAsync();
            if (result != null)
                return Content(ShowMessage.DeleteSuccessResult(), "application/json");
            return Content(ShowMessage.FailedResult(), "application/json");
        }


        private async Task<bool> AreaExists(int id)
        {
            return await _context.BookPublishHouse.AnyAsync(e => e.Id == id);
        }
    }
}
