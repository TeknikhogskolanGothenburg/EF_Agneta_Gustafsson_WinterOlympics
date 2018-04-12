﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OlympicApp.Data;
using System;

namespace OlympicApp.Data.Migrations
{
    [DbContext(typeof(OlympicContext))]
    [Migration("20180328124937_Contestant")]
    partial class Contestant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OlympicApp.Domain.Contest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContestName");

                    b.Property<int>("SportId");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Contest");
                });

            modelBuilder.Entity("OlympicApp.Domain.Contestant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<int>("CountryId");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<int>("SportId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("SportId");

                    b.ToTable("Contestant");
                });

            modelBuilder.Entity("OlympicApp.Domain.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryName");

                    b.Property<int>("MedalId");

                    b.HasKey("Id");

                    b.HasIndex("MedalId")
                        .IsUnique();

                    b.ToTable("Country");
                });

            modelBuilder.Entity("OlympicApp.Domain.Medal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Bronze");

                    b.Property<int?>("Gold");

                    b.Property<int?>("Silver");

                    b.HasKey("Id");

                    b.ToTable("Medal");
                });

            modelBuilder.Entity("OlympicApp.Domain.Referee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Referee");
                });

            modelBuilder.Entity("OlympicApp.Domain.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SportName");

                    b.HasKey("Id");

                    b.ToTable("Sport");
                });

            modelBuilder.Entity("OlympicApp.Domain.Contest", b =>
                {
                    b.HasOne("OlympicApp.Domain.Sport", "Sport")
                        .WithMany("Contests")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OlympicApp.Domain.Contestant", b =>
                {
                    b.HasOne("OlympicApp.Domain.Country", "Country")
                        .WithMany("Contestants")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OlympicApp.Domain.Sport", "Sport")
                        .WithMany("Contestants")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OlympicApp.Domain.Country", b =>
                {
                    b.HasOne("OlympicApp.Domain.Medal", "Medal")
                        .WithOne("Country")
                        .HasForeignKey("OlympicApp.Domain.Country", "MedalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OlympicApp.Domain.Referee", b =>
                {
                    b.HasOne("OlympicApp.Domain.Country", "Country")
                        .WithMany("Referees")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}