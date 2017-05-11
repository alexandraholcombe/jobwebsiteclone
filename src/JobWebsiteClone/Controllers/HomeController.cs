using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobWebsiteClone.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace JobWebsiteClone.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            EnvironmentVariables newKey = new EnvironmentVariables();
            ViewBag.PlacesKey = newKey.PlacesKey;
            return View();
        }
    }
}
