﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication.Data;

#nullable disable

namespace WebApplication.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220206232223_clientorder_migr")]
    partial class clientorder_migr
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplication.Data.Entity.Addish", b =>
                {
                    b.Property<int>("AddishId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AddishId"));

                    b.Property<string>("AddishName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("Mass")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<decimal?>("Price")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.HasKey("AddishId");

                    b.ToTable("Addishes");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.Client", b =>
                {
                    b.Property<long>("Telephone")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Telephone"));

                    b.Property<string>("ClientName")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Telephone");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.ClientOrder", b =>
                {
                    b.Property<int>("ClientOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClientOrderId"));

                    b.Property<long>("ClientTelephone")
                        .HasColumnType("bigint");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("LastMass")
                        .HasColumnType("integer");

                    b.Property<double>("LastPrice")
                        .HasColumnType("double precision");

                    b.Property<long?>("Telephone")
                        .HasColumnType("bigint");

                    b.Property<string>("TextOrder")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ClientOrderId");

                    b.HasIndex("ClientTelephone");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ClientOrders");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Mname")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int?>("PizzeriaId")
                        .HasColumnType("integer");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("EmployeeId");

                    b.HasIndex("PizzeriaId");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IngredientId"));

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("Mass")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<decimal?>("Price")
                        .IsRequired()
                        .HasColumnType("numeric");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.PizzaInMenu", b =>
                {
                    b.Property<int>("PizzaInMenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PizzaInMenuId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("PizzaInMenuName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PizzaInMenuId");

                    b.ToTable("Pizzas");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.PizzaSize", b =>
                {
                    b.Property<int>("PizzaSizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PizzaSizeId"));

                    b.Property<int>("Mass")
                        .HasColumnType("integer");

                    b.Property<int?>("PizzaInMenuId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("SizeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PizzaSizeId");

                    b.HasIndex("PizzaInMenuId");

                    b.ToTable("PizzaSizes");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.Pizzeria", b =>
                {
                    b.Property<int>("PizzeriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PizzeriaId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PizzeriaId");

                    b.ToTable("Pizzerias");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.ClientOrder", b =>
                {
                    b.HasOne("WebApplication.Data.Entity.Client", "Client")
                        .WithMany("ClientOrders")
                        .HasForeignKey("ClientTelephone")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication.Data.Entity.Employee", "Employee")
                        .WithMany("ClientOrders")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.Employee", b =>
                {
                    b.HasOne("WebApplication.Data.Entity.Pizzeria", "Pizzeria")
                        .WithMany("Employees")
                        .HasForeignKey("PizzeriaId");

                    b.HasOne("WebApplication.Data.Entity.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId");

                    b.Navigation("Pizzeria");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.PizzaSize", b =>
                {
                    b.HasOne("WebApplication.Data.Entity.PizzaInMenu", "PizzaInMenu")
                        .WithMany("PizzaSize")
                        .HasForeignKey("PizzaInMenuId");

                    b.Navigation("PizzaInMenu");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.Client", b =>
                {
                    b.Navigation("ClientOrders");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.Employee", b =>
                {
                    b.Navigation("ClientOrders");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.PizzaInMenu", b =>
                {
                    b.Navigation("PizzaSize");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.Pizzeria", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("WebApplication.Data.Entity.Role", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}