using System.Linq;
using System.Web.Http;
using GigHub.Dto;
using GigHub.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();

        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto Dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.gigId == Dto.GigId))
                return BadRequest("The attendance already exists");

            var attendance = new Attendance { gigId = Dto.GigId, AttendeeId = userId };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult RemoveAttend(int id)
        {
            var userID = User.Identity.GetUserId();
            var attend = _context.Attendances.SingleOrDefault(a => a.gigId == id && a.AttendeeId == userID);
            if (attend == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attend);
            _context.SaveChanges();
            return Ok();


        }
    }
}
