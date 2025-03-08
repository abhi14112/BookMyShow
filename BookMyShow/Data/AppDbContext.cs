﻿using BookMyShow.Models;
using Microsoft.EntityFrameworkCore;
namespace BookMyShow.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<UserSeats> UserSeats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.PersonalDetails)
                .WithOne(pd => pd.User)
                .HasForeignKey<PersonalDetails>(pd => pd.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<Movie>()
                .HasMany<Show>()
                .WithOne(s => s.Movie)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Show>()
                .HasMany(s => s.Seats)
                .WithOne(s => s.Show)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Seat>()
                .Property(s => s.SeatType)
                .HasConversion<string>();
            modelBuilder.Entity<PersonalDetails>()
                .Property(p => p.Gender)
                .HasConversion<string>();
            modelBuilder.Entity<UserSeats>()
               .HasOne(us => us.Cinema)
               .WithMany()
               .HasForeignKey(us => us.CinemaId);

            modelBuilder.Entity<UserSeats>()
                .HasOne(us => us.Movie)
                .WithMany()
                .HasForeignKey(us => us.MovieId);

            modelBuilder.Entity<UserSeats>()
                .HasOne(us => us.Show)
                .WithMany()
                .HasForeignKey(us => us.ShowId);
        }
    }
}