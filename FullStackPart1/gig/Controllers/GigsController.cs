using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.Models.Repository;
using GigHub.Presistence;
using GigHub.ViewModels;
using Microsoft.ApplicationInsights.Web;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;




        private readonly UnitOfWork _unitOfWork;

        public GigsController(GenreRepository genreRepository)
        {

            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        public ActionResult Attending()
        {

            var userid = User.Identity.GetUserId();

            var gigs = new GigsViewModel
            {
                ShowAction = User.Identity.IsAuthenticated,
                UpComingGigs = _unitOfWork.Gig.GetGigsUserAttending(userid),
                Heading = "Gigs I'm Attending",
                Attendance = _unitOfWork.Attendance.GetFutureAttendances(userid)
                    .ToLookup(a => a.gigId)

            };

            return View("Gigs", gigs);
        }





        [Authorize]
        public ActionResult Mine()
        {
            var userID = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gig.GetUpComingGigsByArtist(userID);


            return View(gigs);

        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {

            return RedirectToAction("index", "Home", new { query = viewModel.SearchTerm });

        }

        // GET: Gig
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genre.GetGenre(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]

        public ActionResult Edit(int id)
        {

            var gig = _unitOfWork.Gig.GetGigs(id);

            if (IsArtistAUser(gig, out ActionResult actionResult)) return actionResult;

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
                ViewModel.Genres = _unitOfWork.Genre.GetGenre();
                return View("GigForm", ViewModel);
            }
            var gig = new gig
            {
                ArtistID = User.Identity.GetUserId(),
                GenreID = ViewModel.Genre,
                Venue = ViewModel.Venue,
                DateTime = ViewModel.GetDateTime()
            };

            _unitOfWork.Genre.Add(gig);

            _unitOfWork.Complate();

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


            var gig = _unitOfWork.Gig.GetGigWithAttendees(ViewModel.id);


            if (IsArtistAUser(gig, out ActionResult actionResult)) return actionResult;

            gig.Modify(ViewModel.GetDateTime(), ViewModel.Venue, ViewModel.Genre);
            _unitOfWork.Complate();

            return RedirectToAction("Mine", "Gigs");


        }

        private bool IsArtistAUser(gig gig, out ActionResult actionResult)
        {
            if (gig == null)
            {
                {
                    actionResult = HttpNotFound();
                    return true;
                }
            }


            if (gig.ArtistID != User.Identity.GetUserId())
            {
                {
                    actionResult = new HttpUnauthorizedResult();
                    return true;
                }
            }
            actionResult = null;
            return false;
        }


        public ActionResult Details(int id)
        {
            var userid = User.Identity.GetUserId();
            var gig = _unitOfWork.Gig.GetGigs(id);

            if (gig == null)
            {
                return HttpNotFound();
            }
            var viewModel = new GigDetailsViewModel
            {
                Gig = gig,

            };
            if (User.Identity.IsAuthenticated)
            {
                viewModel.IsFollowing =
                    _unitOfWork.Following.GetFollowing(userid, gig.ArtistID) != null;
                viewModel.IsAttending = _unitOfWork.Attendance.GetAttendance(userid, gig.id) != null;
            }
            return View("Details", viewModel);
        }
    }
}