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
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()          
                .HasMaxLength(100);    

            builder.Property(u => u.Email)
                .IsRequired()         
                .HasMaxLength(150);    

 
            builder.HasIndex(u => u.Email)
                .IsUnique();           

            builder.Property(u => u.PasswordHash)
                .IsRequired()         
                .HasMaxLength(500);   

            builder.HasMany(u => u.UserCommunities)
                   .WithOne(uc => uc.User)
                   .HasForeignKey(uc => uc.UserId)
                   .OnDelete(DeleteBehavior.NoAction); 

            builder.HasMany(u => u.Posts)
                .WithOne(p => p.User)                      
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);        


            builder.HasMany(u => u.Comments)
                   .WithOne(c => c.User)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.NoAction); 

 
            builder.HasMany(u => u.SavedPosts)
                   .WithOne(sp => sp.User)
                   .HasForeignKey(sp => sp.UserId)
                   .OnDelete(DeleteBehavior.NoAction); 
        }

    }
}