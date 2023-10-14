﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace RockPaperScissorsDA.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231014212403_1Migration")]
    partial class _1Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AnonUserEntity", b =>
                {
                    b.Property<string>("AnonUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TotalDraws")
                        .HasColumnType("int");

                    b.Property<int?>("TotalGamesPlayed")
                        .HasColumnType("int");

                    b.Property<int?>("TotalLosses")
                        .HasColumnType("int");

                    b.Property<int?>("TotalWins")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("AnonUserId");

                    b.ToTable("anon_user", (string)null);
                });

            modelBuilder.Entity("GameEntity", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameId"), 1L, 1);

                    b.Property<string>("AnonUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ComputerChoice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserChoice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameId");

                    b.HasIndex("AnonUserId");

                    b.ToTable("game", (string)null);
                });

            modelBuilder.Entity("GameEntity", b =>
                {
                    b.HasOne("AnonUserEntity", "AnonUserEntity")
                        .WithMany("GameEntities")
                        .HasForeignKey("AnonUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AnonUserEntity");
                });

            modelBuilder.Entity("AnonUserEntity", b =>
                {
                    b.Navigation("GameEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
