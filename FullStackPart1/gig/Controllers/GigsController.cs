using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Attending()
        {

            var userid = User.Identity.GetUserId();
            var gig = _context.Attendances.Where(a => a.AttendeeId == userid)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var gigs = new GigsViewModel
            {
                ShowAction = User.Identity.IsAuthenticated
                ,
                UpComingGigs = gig
                ,
                Heading = "Gigs I'm Attending"

            };

            return View("Gigs", gigs);
        }


        // GET: Gig
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewModel.Genres = _context.Genres.ToList();
                return View("Create", ViewModel);
            }
            var gig = new gig
            {
                ArtistID = User.Identity.GetUserId(),
                GenreID = ViewModel.Genre,
                Venue = ViewModel.Venue,
                DateTime = ViewModel.GetDateTime()
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");


        }
    }
}