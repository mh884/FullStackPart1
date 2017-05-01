using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GigHub.ViewModels;

namespace GigHub.Models
{
    public class gig
    {
        public int id { get; set; }

        public bool Iscanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistID { get; set; }
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }
        [Required]

        public byte GenreID { get; set; }

        public ICollection<Attendance> Attendance { get; private set; }


        public gig()
        {
            Attendance = new Collection<Attendance>();
        }

        public void cancel()
        {
            Iscanceled = true;

            var notification = Notification.GigCancel(this);

            foreach (var attendee in Attendance.Select(a => a.Attendee))
            {
                attendee.Notify(notification);

            }
        }


        public void update(GigFormViewModel viewModel)
        {

            var notification = Notification.GigUpdate(this, DateTime, Venue);
            Venue = viewModel.Venue;
            DateTime = viewModel.GetDateTime();
            GenreID = viewModel.Genre;

            foreach (var attendee in Attendance.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}