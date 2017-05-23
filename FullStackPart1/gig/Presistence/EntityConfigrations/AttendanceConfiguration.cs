
using System.Data.Entity.ModelConfiguration;

using GigHub.Core.Models;

namespace GigHub.Presistence.EntityConfigrations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {

            HasKey(a => a.gigId).
            Property(a => a.gigId).HasColumnOrder(columnOrder: 1);

            HasKey(a => a.AttendeeId).
            Property(a => a.AttendeeId).HasColumnOrder(columnOrder: 2);

        }
    }
}