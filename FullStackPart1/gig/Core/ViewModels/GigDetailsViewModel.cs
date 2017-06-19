using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigDetailsViewModel
    {
        public Gigs Gigs { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsAttending { get; set; }
    }
}