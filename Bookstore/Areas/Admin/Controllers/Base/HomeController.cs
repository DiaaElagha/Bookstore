using AutoMapper;
using Bookstore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Areas.Admin.Controllers.Base
{
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper) : base(context, userManager, roleManager, mapper)
        {
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
