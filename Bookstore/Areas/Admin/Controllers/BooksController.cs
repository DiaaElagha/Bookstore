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
using Microsoft.AspNetCore.Http;
using Bookstore.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Bookstore.Areas.Admin.Controllers
{
    [Authorize]
    public class BooksController : BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public BooksController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment) : base(context, userManager, roleManager, mapper)
        {
            this._hostingEnvironment = hostingEnvironment;
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
                imageLink = "files/Books/" + x.ImagePath
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
            ViewData["ListBookCategories"] = new SelectList(await _context.BookCategory.ToListAsync(), nameof(BookCategory.Id), nameof(BookCategory.Name));
            ViewData["ListBookAuthors"] = new SelectList(await _context.BookAuthor.ToListAsync(), nameof(BookAuthor.Id), nameof(BookAuthor.Name));
            ViewData["ListBookPublishHouses"] = new SelectList(await _context.BookPublishHouse.ToListAsync(), nameof(BookPublishHouse.Id), nameof(BookPublishHouse.Name));
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
        public async Task<IActionResult> Create(BookVM model, IFormFile file)
        {
            await GetLists();
            if (ModelState.IsValid)
            {
                try
                {
                    bool resultIsBookDuplication = await IsBookDuplication(model);
                    if (resultIsBookDuplication)
                    {
                        return Content(ShowMessage.AddSuccessResult("w: Sorry! This book already exists!"), "application/json");
                    }

                    var fileName = await ImageHelper.UploadImage(file, _hostingEnvironment, "Files/Books");
                    var newEntity = _mapper.Map<Book>(model);
                    newEntity.ImagePath = fileName;
                    var result = await _context.Book.AddAsync(newEntity);
                    await _context.SaveChangesAsync();
                    if (result != null)
                        return Content(ShowMessage.AddSuccessResult(), "application/json");
                }
                catch (Exception ex)
                {
                }
                return Content(ShowMessage.FailedResult(), "application/json");
            }
            return View(model);
        }

        private async Task<bool> IsBookDuplication(BookVM model)
        {
            bool isBookDuplication = await _context.Book.AnyAsync(x =>
                x.Title.Trim().Equals(model.Title.Trim()) &&
                x.YearRelease.Date.Year == model.YearRelease.Date.Year);
            return isBookDuplication;
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
        public async Task<IActionResult> Edit(int id, BookVM model, IFormFile file)
        {
            await GetLists();
            if (ModelState.IsValid)
            {
                try
                {
                    var baseEntoty = await _context.Book.FindAsync(id);
                    if (await _context.Book.AnyAsync(x => x.Title.Trim().Equals(baseEntoty.Title.Trim()) 
                        && x.YearRelease.Date.Year == baseEntoty.YearRelease.Date.Year))
                    {
                        return Content(ShowMessage.AddSuccessResult("w: Sorry! This book already exists!"), "application/json");
                    }
                    string baseImagePath = "";
                    baseImagePath = baseEntoty.ImagePath;
                    PropertyCopy.Copy(model, baseEntoty);
                    baseEntoty.ImagePath = baseImagePath;
                    if (file != null)
                    {
                        var fileName = await ImageHelper.UploadImage(file, _hostingEnvironment, "Files/Books");
                        baseEntoty.ImagePath = fileName;
                    }
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
