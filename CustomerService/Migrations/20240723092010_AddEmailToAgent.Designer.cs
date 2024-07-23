﻿// <auto-generated />
using System;
using CustomerService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomerService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240723092010_AddEmailToAgent")]
    partial class AddEmailToAgent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomerService.Entities.Agent", b =>
                {
                    b.Property<int>("AgentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AgentId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AgentId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Agents");

                    b.HasData(
                        new
                        {
                            AgentId = 1,
                            Email = "agent1@example.com",
                            Name = "Agent One",
                            Password = "AQAAAAIAAYagAAAAEJvatBHPIzmvf7ZVxlXbHvHG3pPyXL642+ezFnpiMsMToVUokDXfhOku1zwLkPZaWA==",
                            Username = "agent1"
                        },
                        new
                        {
                            AgentId = 2,
                            Email = "agent2@example.com",
                            Name = "Agent Two",
                            Password = "AQAAAAIAAYagAAAAEBx+XW0Q2DtWNUPQ6SwOLysLuWzF5pzcUzZbu8Qk4C4zdDeGjiKsZzHCC/x2dB29kw==",
                            Username = "agent2"
                        },
                        new
                        {
                            AgentId = 3,
                            Email = "agent3@example.com",
                            Name = "Agent Three",
                            Password = "AQAAAAIAAYagAAAAEBRvjTT9AFEtKtDikePxppt88YP7QvvqX5MEqj5erNc29Ae8GC+XfvOnYWidr0Yatw==",
                            Username = "agent3"
                        },
                        new
                        {
                            AgentId = 4,
                            Email = "agent4@example.com",
                            Name = "Agent Four",
                            Password = "AQAAAAIAAYagAAAAEMgmECIOwAPlD7NUN2h7JyBl418GxEtutDR1EKfoFx6inf34FZqSUxsGuWlsXhG/oQ==",
                            Username = "agent4"
                        },
                        new
                        {
                            AgentId = 5,
                            Email = "agent5@example.com",
                            Name = "Agent Five",
                            Password = "AQAAAAIAAYagAAAAEIEJMME7miVTJxY8MHHZIyKD5/nZS9cSIh1vG6Yl3rX3g5Lm2ia95i1XohXXhhDZww==",
                            Username = "agent5"
                        });
                });

            modelBuilder.Entity("CustomerService.Entities.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CouponEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CouponStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiscountCoupon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscountPercentage")
                        .HasColumnType("int");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("CustomerService.Entities.Discount", b =>
                {
                    b.HasOne("CustomerService.Entities.Agent", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agent");
                });
#pragma warning restore 612, 618
        }
    }
}