using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Bookstore.Data;

namespace Bookstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Route("Admin/[controller]/[action]/{id?}")]
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<IdentityUser> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly IMapper _mapper;
        protected String UserId;
        protected String UserName;

        public BaseController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = ((ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName.ToString().ToLower();
            var controllerName = ((ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName.ToString().ToLower();
            if (User.Identity.IsAuthenticated)
            {
                base.OnActionExecuting(filterContext);
                try
                {
                    UserId = _userManager.GetUserId(HttpContext.User);
                    var user = _userManager.Users.SingleOrDefault(x => x.Id.Equals(UserId));
                    UserName = user.UserName;
                    ViewBag.UserId = UserId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public IdentityUser GetUser() 
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id.Equals(UserId));
            return user;
        }

    }
}
