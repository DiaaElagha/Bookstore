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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookstore.Areas.Admin.Controllers
{
    [Authorize]
    public class BooksController : BaseController
    {
        public BooksController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper) : base(context, userManager, roleManager, mapper)
        {
        }

        [HttpPost]
        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var query = await _context.Book
                .Include(x => x.BookAuthor)
                .Include(x => x.BookCategory)
                .Include(x => x.BookPublishHouse)
                .Where(x => (d.SearchKey == null 
                    || x.Title.Contains(d.SearchKey) 
                    || x.Description.Contains(d.SearchKey)
                    || x.YearRelease.Year.ToString().Contains(d.SearchKey)))
                .OrderBy(x => x.CreateAt)
                .ToListAsync();

            int totalCount = query.Count();

            var items = query.Select(x => new
            {
                x.Id,
                x.Title,
                BookCategoryName = x.BookCategory.Name,
                BookAuthorName = x.BookAuthor.Name,
                BookPublishHouseName = x.BookPublishHouse.Name,
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

        private async Task GetLists()
        {
            ViewData["ListBookCategories"] = new SelectList(await _context.BookCategory.ToListAsync() , nameof(BookCategory.Id), nameof(BookCategory.Name));
            ViewData["ListBookAuthors"] = new SelectList(await _context.BookAuthor.ToListAsync() , nameof(BookAuthor.Id), nameof(BookAuthor.Name));
            ViewData["ListBookPublishHouses"] = new SelectList(await _context.BookPublishHouse.ToListAsync() , nameof(BookPublishHouse.Id), nameof(BookPublishHouse.Name));
        }

        // GET: Admin/Area/Create
        public async Task<IActionResult> Create()
        {
            await GetLists();
            return View();
        }

        // POST: Admin/Area/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookVM model)
        {
            await GetLists();
            if (ModelState.IsValid)
            {
                var newEntity = _mapper.Map<Book>(model);
                var result = await _context.Book.AddAsync(newEntity);
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
            await GetLists();
            if (id == null)
            {
                return NotFound();
            }
            var model = await _context.Book.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var newEntity = _mapper.Map<BookVM>(model);
            return View(newEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookVM model)
        {
            await GetLists();
            if (ModelState.IsValid)
            {
                try
                {
                    var baseEntoty = await _context.Book.FindAsync(id);
                    PropertyCopy.Copy(model, baseEntoty);
                    _context.Book.Update(baseEntoty);
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
            var model = await _context.Book.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var result = _context.Book.Remove(model);
            await _context.SaveChangesAsync();
            if (result != null)
                return Content(ShowMessage.DeleteSuccessResult(), "application/json");
            return Content(ShowMessage.FailedResult(), "application/json");
        }


        private async Task<bool> AreaExists(int id)
        {
            return await _context.Book.AnyAsync(e => e.Id == id);
        }
    }
}
