﻿// <auto-generated />
using System;
using Blogs.Persistency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blogs.Persistency.Migrations
{
    [DbContext(typeof(BlogsDbContext))]
    [Migration("20200602193955_Comments")]
    partial class Comments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("Blogs.Domain.Blog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OwnerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Blogs.Domain.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("PostID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("PostID");

                    b.HasIndex("UserID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Blogs.Domain.Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BlogID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("OwnerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("BlogID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Blogs.Domain.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ApiSecret")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ApiKey = "189c2743-f7d7-4c26-b975-3d8a6add943a",
                            ApiSecret = "d5943b9b-4e04-42e9-a3e3-b5555ef91ee4",
                            Name = "Bruno"
                        },
                        new
                        {
                            ID = 2,
                            ApiKey = "b29798c0-9311-4ac9-84e9-35151ee6c0f1",
                            ApiSecret = "8be748c3-c64c-41e3-b998-dfc05f9e8025",
                            Name = "Gabi"
                        },
                        new
                        {
                            ID = 3,
                            ApiKey = "4524c37a-7122-4e6e-b5f5-dfe04214e72a",
                            ApiSecret = "c9c3d85e-aacf-4d53-aa03-c0efba81d8fc",
                            Name = "Nohan"
                        },
                        new
                        {
                            ID = 4,
                            ApiKey = "d685f9f3-9626-47b9-b12d-192255b82900",
                            ApiSecret = "9cf43693-7cb7-478e-b168-044b93da28a0",
                            Name = "Ricardo"
                        });
                });

            modelBuilder.Entity("Blogs.Domain.Blog", b =>
                {
                    b.HasOne("Blogs.Domain.User", "Owner")
                        .WithMany("Blogs")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogs.Domain.Comment", b =>
                {
                    b.HasOne("Blogs.Domain.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blogs.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogs.Domain.Post", b =>
                {
                    b.HasOne("Blogs.Domain.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blogs.Domain.User", "Owner")
                        .WithMany("Posts")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}