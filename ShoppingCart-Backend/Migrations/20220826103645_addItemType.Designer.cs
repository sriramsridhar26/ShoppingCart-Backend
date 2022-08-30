﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingCart_Backend.Data;

#nullable disable

namespace ShoppingCart_Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220826103645_addItemType")]
    partial class addItemType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ShoppingCart_Backend.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("cost")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imglink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("itemType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("items");
                });

            modelBuilder.Entity("ShoppingCart_Backend.Model.Order", b =>
                {
                    b.Property<int>("orderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orderId"), 1L, 1);

                    b.Property<int>("cost")
                        .HasColumnType("int");

                    b.Property<string>("emailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("itemId")
                        .HasColumnType("int");

                    b.Property<string>("itemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("purchaseDT")
                        .HasColumnType("datetime2");

                    b.Property<bool>("purchased")
                        .HasColumnType("bit");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("orderId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("ShoppingCart_Backend.Model.User", b =>
                {
                    b.Property<string>("emailId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("customerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("emailId");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}