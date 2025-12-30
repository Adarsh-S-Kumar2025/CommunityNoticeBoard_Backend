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
    public class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLike>
    {
        public void Configure(EntityTypeBuilder<CommentLike> builder)
        {
            builder.ToTable("CommentLikes");

            builder.HasKey(cl => cl.Id);

          
            builder.HasIndex(cl => new { cl.CommentId, cl.UserId })
                   .IsUnique();

            
            builder.HasOne(cl => cl.Comment)
                   .WithMany(c => c.Likes)
                   .HasForeignKey(cl => cl.CommentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cl => cl.User)
                   .WithMany()
                   .HasForeignKey(cl => cl.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}