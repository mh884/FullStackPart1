using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRespository
    {
        gig GetGigWithAttendees(int gigid);
        gig GetGigs(int id);
        IEnumerable<gig> GetGigsUserAttending(string userid);
        object GetUpComingGigsByArtist(string userId);
        void Add(gig gig);
        IEnumerable<gig> UpConmingGigs(string query = null);
    }
}