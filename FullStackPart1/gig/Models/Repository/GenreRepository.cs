using System.Collections.Generic;
using System.Linq;

namespace GigHub.Models.Repository
{
    public class GenreRepository
    {
        private readonly ApplicationDbContext _context;


        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenre()
        {
            return _context.Genres.ToList(); ;
        }

        public void Add(gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}