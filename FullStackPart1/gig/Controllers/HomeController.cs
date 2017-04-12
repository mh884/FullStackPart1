﻿using System;
using System.Web.Mvc;
using GigHub.Models;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingGigs = _context.Gigs.
                Include(g => g.Artist).
                Where(g => g.DateTime > DateTime.Now);
            return View(upcomingGigs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}