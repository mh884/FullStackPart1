using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Presistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Notification> GetNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(u => u.UserId == userId && !u.IsRead)
                .Select(n => n.Notification)
                .Include(a => a.Gig.Artist)
                .ToList();

        }

        public IEnumerable<UserNotification> GetUserNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(u => u.UserId == userId && !u.IsRead).ToList();
        }
    }


}