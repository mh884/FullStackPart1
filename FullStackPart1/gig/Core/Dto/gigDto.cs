using System;

namespace GigHub.Core.Dto
{
    public class gigDto
    {
        public int id { get; set; }

        public bool Iscanceled { get; set; }

        public UserDto Artist { get; set; }


        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public GenreDto Genre { get; set; }



    }
}