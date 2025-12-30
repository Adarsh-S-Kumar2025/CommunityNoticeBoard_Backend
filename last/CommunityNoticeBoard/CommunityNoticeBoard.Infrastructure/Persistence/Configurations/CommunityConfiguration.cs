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

            builder.ToTable("Communities");

            builder.HasKey(c => c.Id);


            builder.Property(c => c.Name)
                .IsRequired()           
                .HasMaxLength(150);     

            builder.Property(c => c.Description)
                .HasMaxLength(500);     



            builder.HasMany(c => c.Members)
                   .WithOne(uc => uc.Community)
                   .HasForeignKey(uc => uc.CommunityId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Posts)
                   .WithOne(p => p.Community)
                   .HasForeignKey(p => p.CommunityId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}