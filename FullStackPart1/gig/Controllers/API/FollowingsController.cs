using System.Linq;
using System.Web.Http;
using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class FollowingsController : ApiController
    {

        public ApplicationDbContext _context { get; set; }

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult unFollow(string id)
        {

            var userid = User.Identity.GetUserId();

            var follower =
                _context.Followings.SingleOrDefault(f => f.FollowerId == userid && f.FolloweeId == id);
            if (follower == null)
            {
                return NotFound();
            }
            _context.Followings.Remove(follower);
            _context.SaveChanges();
            return Ok();

        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto Dto)
        {
            var userid = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userid && f.FolloweeId == Dto.FolloweeId))
                return BadRequest("Following already exists.");

            var following = new Following { FolloweeId = Dto.FolloweeId, FollowerId = userid };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();



        }
    }
}
