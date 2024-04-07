using AnimesProtech.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimesProtech.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Anime> Animes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anime>().HasData(

                new Anime(
                    name: "One Piece",
                    summary: "Monkey D. Luffy and his swashbuckling crew in " +
                    "their search for the ultimate treasure, the One Piece.",
                    director: "Eiichiro Oda"
                ),

                new Anime(
                    name: "Solo Leveling",
                    summary: "A world where hunters exist to hunt monsters to " +
                    "protect the people. Jin-Woo is the weakest of the rank E " +
                    "hunters and barely stronger than a normal human.",
                    director: "Chugong"
                ),

                new Anime(
                    name: "Frieren: beyond journey's end",
                    summary: "Frieren is a member of the hero's party that " +
                    "defeated the demon king. Both a hero and a sage, " +
                    "she has lived for hundreds of years.",
                    director: "Kanehito Yamada"
                )

            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
