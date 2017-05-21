using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;
using GigHub.Models.Repository;

namespace GigHub.Presistence
{
    public class UnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public readonly GigRespository Gig;
        public readonly AttendanceRepository Attendance;
        public readonly FollowingRepository Following;
        public readonly GenreRepository Genre;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gig = new GigRespository(_context);
            Attendance = new AttendanceRepository(_context);
            Following = new FollowingRepository(_context);
            Genre = new GenreRepository(_context);
        }
        public void Complate()
        {
            _context.SaveChanges();
        }
    }
}