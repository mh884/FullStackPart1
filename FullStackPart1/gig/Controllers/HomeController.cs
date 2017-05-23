using System;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Presistence;
using GigHub.Presistence.Repository;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitOfWork.Gig.UpConmingGigs(query);



            var userID = User.Identity.GetUserId();
            var attendance = _unitOfWork.Attendance.GetFutureAttendances(userID).ToLookup(a => a.gigId);

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