using System.Data.Entity;
using GigHub.Core.Models;

namespace GigHub.Presistence
{
    public interface IApplicationDbContext
    {
        DbSet<Gigs> Gigs { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Following> Followings { get; set; }
        DbSet<Notification> Notification { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }
}