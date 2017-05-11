using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobWebsiteClone.Models;
using JobWebsiteClone.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace JobWebsiteClone.Controllers
{
    public class JobController : Controller
    {
        private readonly JobSiteContext _db;

        public JobController(JobSiteContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Listing listing)
        {
            return View();
        }

        public IActionResult Details(int listingId)
        {
            Listing thisListing = _db.Listings.FirstOrDefault(i => i.Id == listingId);
            return View();
        }

        public IActionResult Search(IndexViewModel model)
        {
            var Matches = _db.Listings.Where(l => l.JobTitle.Contains(model.Keyword)&&l.Location == model.Location).ToList();
            return View(Matches);
        }
    }
}
