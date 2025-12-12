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
    public class PostImageConfiguration : IEntityTypeConfiguration<PostImage>
    {
        public void Configure(EntityTypeBuilder<PostImage> builder)
        {
            // Map to "PostImages" table
            builder.ToTable("PostImages");

            // Primary Key
            builder.HasKey(i => i.Id);

            // Properties
            builder.Property(i => i.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            // Relationship with Post
            builder.HasOne(i => i.Post)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
