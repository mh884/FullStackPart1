using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Models.Repository
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Following> GetFollowing(string userid, string artistID)
        {
            return _context.Followings.Where(a => a.FollowerId == artistID && a.FollowerId == userid);
        }
    }
}