using JWTAuth.Core.Helpers;
using JWTAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly Hashing _hashing;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, Hashing hashing) : base(options) { 
            _hashing = hashing;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User 
                {
                    Id = 1, 
                    Email = "admin@gmail.com" , 
                    PasswordHash = "$2a$11$E0TBI7UCPH6keoSl3lD2Y.BUOO9NcSPf19APOE9TfVL3QDsBKnze6" }
                );


            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>()
                        .HasMany(u => u.RefreshTokens)
                        .WithOne(r => r.User)
                        .HasForeignKey(r => r.UserId);
        }


    }
}
