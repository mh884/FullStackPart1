using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext(); ;
        }
        [HttpDelete]
        public IHttpActionResult Cancel(int ID)
        {
            var UserID = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.id == ID && g.ArtistID == UserID);

            if (gig.Iscanceled)
            {
                return NotFound();
            }

            gig.Iscanceled = true;

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Gig = gig,
                Type = NotificationType.GigCanceled

            };

            var attendees = _context.Attendances
                .Where(a => a.gigId == gig.id)
                .Select(a => a.Attendee)
                .ToList();


            foreach (var attendee in attendees)
            {
                var userNotification = new UserNotification { User = attendee, Notification = notification };
                _context.UserNotifications.Add(userNotification);
            }

            _context.SaveChanges();

            return Ok();

        }


    }
}
