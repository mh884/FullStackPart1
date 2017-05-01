using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var gig = _context.Gigs
                .Include(g => g.Attendance.Select(a => a.Attendee))
                .Single(g => g.id == ID && g.ArtistID == UserID);

            if (gig.Iscanceled)
            {
                return NotFound();
            }

            gig.cancel();

            _context.SaveChanges();

            return Ok();

        }


    }
}
