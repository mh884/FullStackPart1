using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public partial class FollowingsController : ApiController
    {

        public ApplicationDbContext _context { get; set; }

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }


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
