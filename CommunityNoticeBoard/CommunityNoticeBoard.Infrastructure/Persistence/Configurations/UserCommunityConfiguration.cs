using CommunityNoticeBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Infrastructure.Persistence.Configurations
{
    public class UserCommunityConfiguration : IEntityTypeConfiguration<UserCommunity>
    {
        public void Configure(EntityTypeBuilder<UserCommunity> builder)
        {
            // Map to table
            builder.ToTable("UserCommunities");

            // Primary Key
            builder.HasKey(uc => uc.Id);

            // Properties
            builder.Property(uc => uc.Role)
                   .IsRequired();

            // Relationships

            // User -> UserCommunities
            builder.HasOne(uc => uc.User)
                   .WithMany(u => u.UserCommunities)
                   .HasForeignKey(uc => uc.UserId)
                   .OnDelete(DeleteBehavior.NoAction); // Prevent multiple cascade paths

            // Community -> UserCommunities
            builder.HasOne(uc => uc.Community)
                   .WithMany(c => c.Members)
                   .HasForeignKey(uc => uc.CommunityId)
                   .OnDelete(DeleteBehavior.Cascade); // Safe: deleting a community deletes memberships
        }
    }
}