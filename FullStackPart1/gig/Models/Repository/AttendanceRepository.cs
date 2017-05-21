using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Models.Repository
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Attendance> GetFutureAttendances(string userid)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userid)
                .ToList();
        }
        public IEnumerable<Attendance> GetAttendance(string userid, int gigId)
        {
            return _context.Attendances.Where(a => a.gigId == gigId && a.AttendeeId == userid);
            ;
        }
    }
}