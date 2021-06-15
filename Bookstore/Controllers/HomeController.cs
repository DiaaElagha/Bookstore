using AutoMapper;
using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM();
            var listBooks = await _context.Book.Include(x => x.BookCategory).Include(x => x.BookAuthor).OrderByDescending(x => x.CreateAt.Value).Take(8).ToListAsync();
            vm.listBooks = listBooks;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> SearchBook(String v)
        {
            HomeVM vm = new HomeVM();

            var listBooks = await _context.Book.Include(x => x.BookCategory).Include(x => x.BookAuthor)
                .Where(x => 
                    x.Description.Contains(v) || 
                    x.Title.Contains(v) || 
                    x.YearRelease.Date.Year.ToString().Equals(v))
                .OrderByDescending(x => x.CreateAt.Value).ToListAsync();

            vm.listBooks = listBooks;
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
