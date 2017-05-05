using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GigHub.Dto;
using GigHub.Migrations;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNotifications()
        {
            string userId = User.Identity.GetUserId();
            var notification = _context.UserNotifications.Where(u => u.UserID == userId && !u.IsRead)
                .Select(n => n.Notification)
                .Include(a => a.Gig.Artist)
                .ToList();


            return notification.Select(Mapper.Map<Notification, NotificationDto>);
        }


    }
}
