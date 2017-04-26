using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<gig> UpComingGigs { get; set; }
        public bool ShowAction { get; set; }
        public string Heading { get; set; }
    }
}