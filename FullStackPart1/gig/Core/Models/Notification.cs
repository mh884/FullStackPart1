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
        public gig Gig { get; set; }


        private Notification()
        {

        }

        private Notification(gig gig, NotificationType notificationType)
        {
            DateTime = DateTime.Now;
            Gig = gig ?? throw new ArgumentNullException(nameof(gig));
            Type = notificationType;
        }

        private Notification(gig gig, NotificationType notificationType, DateTime orginalDateTime, string orginalVenue)
        {
            DateTime = DateTime.Now;
            Gig = gig ?? throw new ArgumentNullException(nameof(gig));
            OrginalDateTime = orginalDateTime;
            OrginalVenue = orginalVenue;
            Type = notificationType;
        }

        public static Notification GigCreate(gig gig)
        {
            return new Notification(gig, NotificationType.GigCreate);

        }
        public static Notification GigUpdate(gig newgig, DateTime orginalDateTime, string orginalVenue)
        {
            var notification = new Notification(newgig, NotificationType.GigCreate)
            {
                OrginalDateTime = orginalDateTime,
                OrginalVenue = orginalVenue
            };
            return notification;
        }

        public static Notification GigCancel(gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);

        }
    }
}