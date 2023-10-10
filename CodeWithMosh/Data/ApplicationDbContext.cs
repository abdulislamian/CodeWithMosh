using CodeWithMosh.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CodeWithMosh.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var membershipType = new List<MembershipType>
            {
                new MembershipType
                {
                    Id = 1,
                    Name="Free",
                    signUpFee = 0,
                    DurationInMonths = 0,
                    DiscountRate = 0
                },
                new MembershipType
                {
                    Id = 2,
                    Name="Monthly",
                    signUpFee = 30,
                    DurationInMonths = 1,
                    DiscountRate = 10
                },
                new MembershipType
                {
                    Id = 3,
                    Name="Annualy",
                    signUpFee = 90,
                    DurationInMonths = 3,
                    DiscountRate = 15
                }

            };
            builder.Entity<MembershipType>().HasData(membershipType);

            // Configure the Genre entity
            builder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Drama" }
            );


            builder.Entity<Movies>().HasData(
                new Movies { Id = 1, Name = "John Wick", GenreId = 1 },
                new Movies { Id = 2, Name = "3 Idiots", GenreId = 2 }
            );

        }
    }

}
