using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Presistence.Repository
{
    public class GigRespository : IGigRespository
    {
        private readonly IApplicationDbContext _context;

        public GigRespository(IApplicationDbContext context)
        {
            _context = context;
        }


        public Gigs GetGigWithAttendees(int gigid)
        {
            return _context.Gigs
                 .Include(u => Enumerable.Select<Attendance, ApplicationUser>(u.Attendance, a => a.Attendee))
                 .SingleOrDefault(g => g.id == gigid);


        }


        public Gigs GetGigs(int id)
        {
            return _context.Gigs
              .Include(a => a.Artist)
              .Include(g => g.Genre)
              .SingleOrDefault(a => a.id == id);
        }
        public IEnumerable<Gigs> GetGigsUserAttending(string userid)
        {
            return _context.Attendances.Where(a => a.AttendeeId == userid)
                .Select(a => a.Gigs)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }


        public IEnumerable<object> GetUpComingGigsByArtist(string artistId)
        {
            return _context.Gigs.Where((g =>
                                                      g.ArtistId == artistId &&
                                              g.DateTime > DateTime.Now
                                          && !g.Iscanceled
                                             )
                                             ).Include(g => g.Genre)
                .ToList();
        }



        public void Add(Gigs gigs)
        {
            _context.Gigs.Add(gigs);
        }
        public IEnumerable<Gigs> UpConmingGigs(string query = null)
        {
            var upcomingGigs = _context.Gigs.
                Include(g => g.Artist).
                Include(g => g.Genre).
                Where(g => g.DateTime > DateTime.Now && !g.Iscanceled);
            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g =>
                    g.Artist.Name.Contains(query) ||
                    g.Genre.Name.Contains(query) || g.Venue.Contains(query));
            }
            return upcomingGigs;
        }
    }
}