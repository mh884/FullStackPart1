using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Models.Repository
{
    public class GigRespository
    {
        private readonly ApplicationDbContext _context;

        public GigRespository(ApplicationDbContext context)
        {
            _context = context;
        }


        public gig GetGigWithAttendees(int gigid)
        {
            return _context.Gigs
                 .Include(u => u.Attendance.Select(a => a.Attendee))
                 .SingleOrDefault(g => g.id == gigid);


        }

        public gig GetGigs(int id)
        {
            return _context.Gigs
              .Include(a => a.Artist)
              .Include(g => g.Genre)
              .SingleOrDefault(a => a.id == id);
        }
        public IEnumerable<gig> GetGigsUserAttending(string userid)
        {
            return _context.Attendances.Where(a => a.AttendeeId == userid)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }


        public object GetUpComingGigsByArtist(string userId)
        {
            return _context.Gigs.Where((g => g.ArtistID == userId
                                             && g.DateTime > DateTime.Now
                                             && !g.Iscanceled)).Include(g => g.Genre)
                .ToList();
        }
    }
}