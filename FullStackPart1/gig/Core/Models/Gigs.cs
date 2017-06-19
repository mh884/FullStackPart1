using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gigs
    {
        public int id { get; set; }

        public bool Iscanceled { get; private set; }

        public ApplicationUser Artist { get; set; }


        public string ArtistId { get; set; }
        public DateTime DateTime { get; set; }


        public string Venue { get; set; }

        public Genre Genre { get; set; }


        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendance { get; private set; }


        public Gigs()
        {
            Attendance = new Collection<Attendance>();
        }

        public void Cancel()
        {
            Iscanceled = true;

            var notification = Notification.GigCancel(this);

            foreach (var attendee in Attendance.Select(a => a.Attendee))
            {
                attendee.Notify(notification);

            }
        }


        public void Modify(DateTime dateTime, string venue, byte genre)
        {

            var notification = Notification.GigUpdate(this, DateTime, Venue);
            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;

            foreach (var attendee in Attendance.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }


    }
}