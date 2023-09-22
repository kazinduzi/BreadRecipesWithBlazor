﻿// <auto-generated />
using System;
using AccountOwnerServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AccountOwnerServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200919000835_Added_Recipe_Duration")]
    partial class Added_Recipe_Duration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AccountOwnerServer.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("UnityMesure")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("AccountOwnerServer.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("int");

                    b.Property<int>("HealthyStatus")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("AccountOwnerServer.Models.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("AccountOwnerServer.Models.RecipeIngredient", b =>
                {
                    b.HasOne("AccountOwnerServer.Models.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountOwnerServer.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
