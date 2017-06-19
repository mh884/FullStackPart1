using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Presistence.Repository
{
    public class GenreRepository : IGenreRepository
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

        public void Add(Gigs gigs)
        {
            _context.Gigs.Add(gigs);
        }
    }
}