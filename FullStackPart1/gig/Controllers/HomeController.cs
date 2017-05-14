using System;
using System.Web.Mvc;
using GigHub.Models;
using System.Data.Entity;
using System.Linq;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs.
                Include(g => g.Artist).
                Include(g => g.Genre).
                Where(g => g.DateTime > DateTime.Now && !g.Iscanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g =>
                    g.Artist.Name.Contains(query) || g.Genre.Name.Contains(query) || g.Venue.Contains(query));
            }

            string userID = User.Identity.GetUserId();
            var attendance = _context.Attendances.Where(a => a.AttendeeId == userID).ToList().ToLookup(a => a.gigId);

            var viewModel = new GigsViewModel
            {
                UpComingGigs = upcomingGigs,
                ShowAction = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendance = attendance
            };

            return View("Gigs", viewModel);
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