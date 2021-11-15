﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("passwordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("userEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("userName")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("userPasswordHash")
                        .HasColumnType("BLOB");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Entities.Folders", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AppUserid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("folderName")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("AppUserid");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Foldersid")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isMain")
                        .HasColumnType("INTEGER");

                    b.Property<string>("publicId")
                        .HasColumnType("TEXT");

                    b.Property<string>("url")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("Foldersid");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("API.Entities.Folders", b =>
                {
                    b.HasOne("API.Entities.AppUser", null)
                        .WithMany("folders")
                        .HasForeignKey("AppUserid");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.HasOne("API.Entities.AppUser", "appUser")
                        .WithMany("photos")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Folders", null)
                        .WithMany("photos")
                        .HasForeignKey("Foldersid");

                    b.Navigation("appUser");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("folders");

                    b.Navigation("photos");
                });

            modelBuilder.Entity("API.Entities.Folders", b =>
                {
                    b.Navigation("photos");
                });
#pragma warning restore 612, 618
        }
    }
}
