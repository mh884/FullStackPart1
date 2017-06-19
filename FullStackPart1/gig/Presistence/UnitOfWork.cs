
using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Presistence.Repositories;
using GigHub.Presistence.Repository;

namespace GigHub.Presistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public IGigRespository Gigs { get; private set; }
        public IAttendanceRepository Attendance { get; private set; }
        public IFollowingRepository Following { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public INotificationRepository Notification { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRespository(_context);
            Attendance = new AttendanceRepository(_context);
            Following = new FollowingRepository(_context);
            Genre = new GenreRepository(_context);
            Notification = new NotificationRepository(_context);

        }
        public void Complate()
        {
            _context.SaveChanges();
        }
    }
}