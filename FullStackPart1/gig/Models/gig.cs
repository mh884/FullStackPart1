using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class gig
    {
        public int id { get; set; }


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

    }
}