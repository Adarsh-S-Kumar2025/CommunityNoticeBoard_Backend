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
    public class CommunityConfiguration : IEntityTypeConfiguration<Community>
    {
        public void Configure(EntityTypeBuilder<Community> builder)
        {
            // Map to "Communities" table
            builder.ToTable("Communities");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.Name)
                .IsRequired()           // Name cannot be null
                .HasMaxLength(150);     // Maximum length 150 chars

            builder.Property(c => c.Description)
                .HasMaxLength(500);     // Optional description

            // Relationships


            // Community -> UserCommunities
            builder.HasMany(c => c.Members)
                   .WithOne(uc => uc.Community)
                   .HasForeignKey(uc => uc.CommunityId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Community -> Posts
            builder.HasMany(c => c.Posts)
                   .WithOne(p => p.Community)
                   .HasForeignKey(p => p.CommunityId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}