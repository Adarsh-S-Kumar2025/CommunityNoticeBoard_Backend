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
    public class CommunityInviteConfiguration
        : IEntityTypeConfiguration<CommunityInvite>
    {
        public void Configure(EntityTypeBuilder<CommunityInvite> builder)
        {
            builder.ToTable("CommunityInvites");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.InvitedEmail)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(ci => ci.Status)
                   .IsRequired();

            builder.HasOne(ci => ci.Community)
                   .WithMany()
                   .HasForeignKey(ci => ci.CommunityId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.InvitedUser)
                   .WithMany()
                   .HasForeignKey(ci => ci.InvitedUserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ci => ci.InvitedByUser)
                   .WithMany()
                   .HasForeignKey(ci => ci.InvitedByUserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}