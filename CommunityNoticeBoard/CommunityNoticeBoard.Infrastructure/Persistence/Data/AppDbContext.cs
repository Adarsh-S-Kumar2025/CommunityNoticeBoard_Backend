using CommunityNoticeBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Infrastructure.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=CommunityNoticeDb;User Id=sa;Password=12345678Aa;TrustServerCertificate=True;");

        }

        // ---------------------------
        // DbSets (Tables)
        // ---------------------------
        public DbSet<User> Users => Set<User>();
        public DbSet<Community> Communities => Set<Community>();
        public DbSet<UserCommunity> UserCommunities => Set<UserCommunity>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<PostImage> PostImages => Set<PostImage>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    
    }
}