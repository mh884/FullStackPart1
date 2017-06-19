using System.Linq;
using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Presistence;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            var any = _unitOfWork.Attendance.GetAttendance(userId, dto.GigId).Any();
            if (any)
                return BadRequest("The attendance already exists");

            var attendance = new Attendance { gigId = dto.GigId, AttendeeId = userId };
            _unitOfWork.Attendance.Add(attendance);
            _unitOfWork.Complate();

            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult RemoveAttend(int id)
        {
            var userId = User.Identity.GetUserId();
            var attend = _unitOfWork.Attendance.GetAttendance(userId, id).SingleOrDefault();
            if (attend == null)
            {
                return NotFound();
            }

            _unitOfWork.Attendance.Remove(attend);
            _unitOfWork.Complate();
            return Ok();


        }
    }
}
