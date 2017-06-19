using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Presistence.EntityConfigrations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {

        public UserNotificationConfiguration()
        {
            HasKey(u => u.UserId)
                .Property(u => u.UserId)
                .HasColumnOrder(columnOrder: 1);

            HasKey(u => u.NotificationId)
                .Property(u => u.NotificationId)
                .HasColumnOrder(columnOrder: 2);
        }
    }
}