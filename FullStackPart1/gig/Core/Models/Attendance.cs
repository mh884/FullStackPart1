using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class Attendance
    {
        public Gigs Gigs { get; set; }
        public ApplicationUser Attendee { get; set; }

        public int gigId { get; set; }


        public string AttendeeId { get; set; }

    }
}