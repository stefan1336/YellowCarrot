﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YellowCarrotStefanJohansson.Data;

#nullable disable

namespace YellowCarrotStefanJohansson.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221214082809_AddedVar")]
    partial class AddedVar
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YellowCarrotStefanJohansson.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            IngredientId = 1,
                            Name = "Makaroner",
                            Quantity = "4 dl",
                            RecipeId = 1
                        },
                        new
                        {
                            IngredientId = 2,
                            Name = "Köttbullar",
                            Quantity = "1 kg",
                            RecipeId = 1
                        },
                        new
                        {
                            IngredientId = 3,
                            Name = "Fiskpinnar",
                            Quantity = "1 kg",
                            RecipeId = 2
                        },
                        new
                        {
                            IngredientId = 4,
                            Name = "Linsgryta",
                            Quantity = "2 kg",
                            RecipeId = 3
                        },
                        new
                        {
                            IngredientId = 5,
                            Name = "Makaroner",
                            Quantity = "2 dl",
                            RecipeId = 3
                        });
                });

            modelBuilder.Entity("YellowCarrotStefanJohansson.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<TimeSpan>("RecipeTime")
                        .HasColumnType("time");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId");

                    b.HasIndex("TagId");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            RecipeId = 1,
                            RecipeName = "Köttbullar och makaroner",
                            RecipeTime = new TimeSpan(0, 0, 10, 0, 0),
                            TagId = 1
                        },
                        new
                        {
                            RecipeId = 2,
                            RecipeName = "Fiskpinnar och makaroner",
                            RecipeTime = new TimeSpan(0, 0, 20, 0, 0),
                            TagId = 2
                        },
                        new
                        {
                            RecipeId = 3,
                            RecipeName = "Linsgryta",
                            RecipeTime = new TimeSpan(0, 0, 30, 0, 0),
                            TagId = 3
                        });
                });

            modelBuilder.Entity("YellowCarrotStefanJohansson.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("Categories")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            Categories = "Kötträtt"
                        },
                        new
                        {
                            TagId = 2,
                            Categories = "Fiskrätt"
                        },
                        new
                        {
                            TagId = 3,
                            Categories = "Vegetarisk"
                        });
                });

            modelBuilder.Entity("YellowCarrotStefanJohansson.Models.Ingredient", b =>
                {
                    b.HasOne("YellowCarrotStefanJohansson.Models.Recipe", "Recipe")
                        .WithMany("Ingridients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("YellowCarrotStefanJohansson.Models.Recipe", b =>
                {
                    b.HasOne("YellowCarrotStefanJohansson.Models.Tag", "Tag")
                        .WithMany("Recipes")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("YellowCarrotStefanJohansson.Models.Recipe", b =>
                {
                    b.Navigation("Ingridients");
                });

            modelBuilder.Entity("YellowCarrotStefanJohansson.Models.Tag", b =>
                {
                    b.Navigation("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}
