using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Presistence.EntityConfigrations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {

        public FollowingConfiguration()
        {
            HasKey(f => f.FollowerId)
                .Property(f => f.FollowerId)
                .HasColumnOrder(columnOrder: 1);

            HasKey(f => f.FolloweeId)
                .Property(f => f.FolloweeId)
                .HasColumnOrder(columnOrder: 2);
        }
    }
}