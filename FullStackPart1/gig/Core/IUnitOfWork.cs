﻿using GigHub.Core.Repositories;
using GigHub.Presistence.Repository;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRespository Gigs { get; }
        IAttendanceRepository Attendance { get; }
        IFollowingRepository Following { get; }
        IGenreRepository Genre { get; }

        INotificationRepository Notification { get; }

        void Complate();
    }
}