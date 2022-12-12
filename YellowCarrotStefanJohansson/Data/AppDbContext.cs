using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrotStefanJohansson.Models;

namespace YellowCarrotStefanJohansson.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrotDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 1,
                Name = "Makaroner",
                Quantity = "4 dl",
                RecipeId = 1               
            },
            new Ingredient()
            {
                IngredientId = 2,
                Name = "Köttbullar",
                Quantity = "1 kg",
                RecipeId = 1
            },
            new Ingredient()
            {
                IngredientId = 3,
                Name = "Fiskpinnar",
                Quantity = "1 kg",
                RecipeId = 2
            },
            new Ingredient()
            {
                IngredientId = 4,
                Name = "Linsgryta",
                Quantity = "2 kg",
                RecipeId = 3
            },
            new Ingredient()
            {
                IngredientId = 5,
                Name = "Makaroner",
                Quantity = "2 dl",
                RecipeId = 3
            });


            modelbuilder.Entity<Tag>().HasData(new Tag()
            {
                TagId = 1,
                Categories = "Kötträtt",

            },
            new Tag()
            {
                TagId = 2,
                Categories = "Fiskrätt"
            },
            new Tag()
            {
                TagId = 3,
                Categories = "Vegetarisk"
            });

            modelbuilder.Entity<Recipe>().HasData(new Recipe()
            {
                RecipeId = 1,
                RecipeName = "Köttbullar och makaroner",
                RecipeTime = TimeSpan.FromMinutes(10),
                TagId = 1
            },
            new Recipe()
            {
                RecipeId = 2,
                RecipeName = "Fiskpinnar och makaroner",
                RecipeTime = TimeSpan.FromMinutes(20),
                TagId = 2

            },
            new Recipe()
            {
                RecipeId = 3,
                RecipeName = "Linsgryta",
                RecipeTime= TimeSpan.FromMinutes(30),
                TagId = 3
            }
            );
        }
    }
}
