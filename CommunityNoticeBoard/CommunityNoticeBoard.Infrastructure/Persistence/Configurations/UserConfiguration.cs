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
    public class UserConfiguration : IEntityTypeConfiguration<CommunityNoticeBoard.Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Map this entity to "Users" table
            builder.ToTable("Users");

            // Set primary key
            builder.HasKey(u => u.Id);

            // Configure Name property
            builder.Property(u => u.Name)
                .IsRequired()          // Name cannot be null
                .HasMaxLength(100);    // Maximum length 100 chars

            // Configure Email property
            builder.Property(u => u.Email)
                .IsRequired()          // Email cannot be null
                .HasMaxLength(150);    // Maximum length 150 chars

            // Make Email unique
            builder.HasIndex(u => u.Email)
                .IsUnique();           // No two users can have same email

            // Configure PasswordHash property
            builder.Property(u => u.PasswordHash)
                .IsRequired()          // Cannot be null
                .HasMaxLength(500);    // Maximum length 500 chars

            // User -> UserCommunities
            builder.HasMany(u => u.UserCommunities)
                   .WithOne(uc => uc.User)
                   .HasForeignKey(uc => uc.UserId)
                   .OnDelete(DeleteBehavior.NoAction); // Break multiple cascade paths

            // Configure one-to-many relationship with Posts
            builder.HasMany(u => u.Posts)
                .WithOne(p => p.User)                      // Each Post references User
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);        // Delete posts if user is deleted

            // User -> Comments
            builder.HasMany(u => u.Comments)
                   .WithOne(c => c.User)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.NoAction); // Break multiple cascade paths

            // User -> SavedPosts
            builder.HasMany(u => u.SavedPosts)
                   .WithOne(sp => sp.User)
                   .HasForeignKey(sp => sp.UserId)
                   .OnDelete(DeleteBehavior.NoAction); // Break multiple cascade paths       // Delete saved posts if user is deleted
        }

    }
}