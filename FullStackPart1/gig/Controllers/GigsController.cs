
using System.Linq;

using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Presistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Attending()
        {

            var userid = User.Identity.GetUserId();

            var gigs = new GigsViewModel
            {
                ShowAction = User.Identity.IsAuthenticated,
                UpComingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userid),
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
            var gigs = _unitOfWork.Gigs.GetUpComingGigsByArtist(userID);


            return View(gigs);

        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {

            return RedirectToAction("index", "Home", new { query = viewModel.SearchTerm });

        }

        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genre.GetGenre(),
                Heading = "Add a Gigs"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]

        public ActionResult Edit(int id)
        {

            var gig = _unitOfWork.Gigs.GetGigs(id);

            if (IsArtistAUser(gig, out ActionResult actionResult)) return actionResult;

            var viewModel = new GigFormViewModel
            {
                id = gig.id,
                Genres = new ApplicationDbContext().Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gigs"
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
            var gig = new Gigs
            {
                ArtistId = User.Identity.GetUserId(),
                GenreId = ViewModel.Genre,
                Venue = ViewModel.Venue,
                DateTime = ViewModel.GetDateTime()
            };

            _unitOfWork.Gigs.Add(gig);

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
                ViewModel.Genres = new ApplicationDbContext().Genres.ToList();
                return View("GigForm", ViewModel);
            }


            var gig = _unitOfWork.Gigs.GetGigWithAttendees(ViewModel.id);


            if (IsArtistAUser(gig, out ActionResult actionResult)) return actionResult;

            gig.Modify(ViewModel.GetDateTime(), ViewModel.Venue, ViewModel.Genre);
            _unitOfWork.Complate();

            return RedirectToAction("Mine", "Gigs");


        }

        private bool IsArtistAUser(Gigs gigs, out ActionResult actionResult)
        {
            if (gigs == null)
            {
                {
                    actionResult = HttpNotFound();
                    return true;
                }
            }


            if (gigs.ArtistId != User.Identity.GetUserId())
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
            var gig = _unitOfWork.Gigs.GetGigs(id);

            if (gig == null)
            {
                return HttpNotFound();
            }
            var viewModel = new GigDetailsViewModel
            {
                Gigs = gig,

            };
            if (User.Identity.IsAuthenticated)
            {
                viewModel.IsFollowing =
                    _unitOfWork.Following.GetFollowing(userid, gig.ArtistId) != null;
                viewModel.IsAttending = _unitOfWork.Attendance.GetAttendance(userid, gig.id) != null;
            }
            return View("Details", viewModel);
        }
    }
}