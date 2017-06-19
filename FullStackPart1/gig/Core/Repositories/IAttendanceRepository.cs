using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userid);
        IEnumerable<Attendance> GetAttendance(string userid, int gigId);
        void Add(Attendance attendance);
        void Remove(Attendance attend);
    }
}