using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Presistence.Repository
{
    public class FollowingRepository : IFollowingRepository
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