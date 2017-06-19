using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Notification
    {
        private DateTime _dateTime;

        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }


        public NotificationType Type { get; private set; }
        public DateTime? OrginalDateTime { get; private set; }

        public string OrginalVenue { get; private set; }

        [Required]
        public Gigs Gigs { get; set; }


        public Notification(Gigs gigs, NotificationType notificationType)
        {
            DateTime = DateTime.Now;
            Gigs = gigs ?? throw new ArgumentNullException(nameof(gigs));
            Type = notificationType;
        }

        private Notification(Gigs gigs, NotificationType notificationType, DateTime orginalDateTime, string orginalVenue)
        {
            DateTime = DateTime.Now;
            Gigs = gigs ?? throw new ArgumentNullException(nameof(gigs));
            OrginalDateTime = orginalDateTime;
            OrginalVenue = orginalVenue;
            Type = notificationType;
        }

        public static Notification GigCreate(Gigs gigs)
        {
            return new Notification(gigs, NotificationType.GigCreate);

        }
        public static Notification GigUpdate(Gigs newgig, DateTime orginalDateTime, string orginalVenue)
        {
            var notification = new Notification(newgig, NotificationType.GigCreate)
            {
                OrginalDateTime = orginalDateTime,
                OrginalVenue = orginalVenue
            };
            return notification;
        }

        public static Notification GigCancel(Gigs gigs)
        {
            return new Notification(gigs, NotificationType.GigCanceled);

        }
    }
}