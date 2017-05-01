using System;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;

namespace GigHub.Models
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

        private Notification(gig gig, NotificationType notificationType, DateTime ODateTime, string OVenue)
        {
            DateTime = DateTime.Now;
            Gig = gig ?? throw new ArgumentNullException(nameof(gig));
            OrginalDateTime = ODateTime;
            OrginalVenue = OVenue;
            Type = notificationType;
        }

        public static Notification GigCreate(gig gig)
        {
            return new Notification(gig, NotificationType.GigCreate);

        }
        public static Notification GigUpdate(gig Newgig, DateTime OrginalDateTime, string OrginalVenue)
        {
            var notification = new Notification(Newgig, NotificationType.GigCreate)
            {
                OrginalDateTime = OrginalDateTime,
                OrginalVenue = OrginalVenue
            };
            return notification;
        }

        public static Notification GigCancel(gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);

        }
    }
}