﻿using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNotifications(string userId);
        IEnumerable<UserNotification> GetUserNotifications(string userId);
    }
}