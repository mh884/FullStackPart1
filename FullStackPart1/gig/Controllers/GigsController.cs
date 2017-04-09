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
        public ActionResult Create(GigFormViewModel ViewModel)
        {
            var gig = new gig
            {
                ArtistID = User.Identity.GetUserId(),
                GenreID = ViewModel.Genre,
                Venue = ViewModel.Venue,
                DateTime = ViewModel.DateTime
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");


        }
    }
}