using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.ApplicationInsights.Web;
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

        [Authorize]
        public ActionResult Mine()
        {
            var userID = User.Identity.GetUserId();
            var gigs = _context.Gigs.Where((g => g.ArtistID == userID && g.DateTime > DateTime.Now)).Include(g => g.Genre)
                .ToList();


            return View(gigs);

        }


        // GET: Gig
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]

        public ActionResult Edit(int id)
        {
            string userid = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.id == id && g.ArtistID == userid);
            var viewModel = new GigFormViewModel
            {
                id = gig.id,
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreID,
                Venue = gig.Venue,
                Heading = "Edit a Gig"
            };
            return View("GigForm", viewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewModel.Genres = _context.Genres.ToList();
                return View("GigForm", ViewModel);
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

            return RedirectToAction("Mine", "Gigs");


        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewModel.Genres = _context.Genres.ToList();
                return View("GigForm", ViewModel);
            }
            var userid = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.id == ViewModel.id && g.ArtistID == userid);

            gig.Venue = ViewModel.Venue;
            gig.DateTime = ViewModel.GetDateTime();
            gig.GenreID = ViewModel.Genre;



            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");


        }


    }
}