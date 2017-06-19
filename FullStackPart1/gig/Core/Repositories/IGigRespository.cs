using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRespository
    {
        Gigs GetGigWithAttendees(int gigid);
        Gigs GetGigs(int id);
        IEnumerable<Gigs> GetGigsUserAttending(string userid);
        IEnumerable<object> GetUpComingGigsByArtist(string userId);
        void Add(Gigs gigs);
        IEnumerable<Gigs> UpConmingGigs(string query = null);
    }
}