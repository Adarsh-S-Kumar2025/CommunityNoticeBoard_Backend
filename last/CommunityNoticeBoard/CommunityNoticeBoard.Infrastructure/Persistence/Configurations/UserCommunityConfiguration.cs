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

            builder.ToTable("UserCommunities");


            builder.HasKey(uc => uc.Id);


            builder.Property(uc => uc.Role)
                   .IsRequired();


            builder.HasOne(uc => uc.User)
                   .WithMany(u => u.UserCommunities)
                   .HasForeignKey(uc => uc.UserId)
                   .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(uc => uc.Community)
                   .WithMany(c => c.Members)
                   .HasForeignKey(uc => uc.CommunityId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}