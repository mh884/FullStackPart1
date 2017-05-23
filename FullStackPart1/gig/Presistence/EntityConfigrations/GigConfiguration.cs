
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using GigHub.Core.Models;

namespace GigHub.Presistence.EntityConfigrations
{
    public class GigConfiguration : EntityTypeConfiguration<gig>
    {

        public GigConfiguration()
        {
            Property(g => g.ArtistId)
                .IsRequired();

            Property(g => g.Venue)
                .IsRequired()
                .HasMaxLength(250);

            Property(g => g.GenreId)
                .IsRequired();



            HasMany(g => g.Attendance)
                .WithRequired(a => a.Gig)
            .WillCascadeOnDelete(false);
        }


    }
}