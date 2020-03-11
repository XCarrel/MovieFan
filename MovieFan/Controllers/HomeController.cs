using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieFan.Models;

namespace MovieFan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly moviefanContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, moviefanContext db, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _db = db;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Users user = Helpers.LoggedInUser(_db, _signInManager);
            if (user != null && !user.ProfileOk) return Redirect($"User/Details/{user.Id}");
            return View();
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
