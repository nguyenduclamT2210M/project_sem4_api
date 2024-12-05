﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using project_sem4_api.Context;

#nullable disable

namespace project_sem4_api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241101045512_f2")]
    partial class f2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("project_sem4_api.Entities.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Dish", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("categoryId")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("discount")
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<int>("statusId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("categoryId");

                    b.HasIndex("statusId");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Employee", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("roleId")
                        .HasColumnType("int");

                    b.Property<int>("timeEmployeeId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("roleId");

                    b.HasIndex("timeEmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Evaluate", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Evaluates");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Event", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("dayEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dayStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Notification", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("statusId")
                        .HasColumnType("int");

                    b.Property<int>("tableId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("statusId");

                    b.HasIndex("tableId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("billNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("employeeId")
                        .HasColumnType("int");

                    b.Property<int>("eventId")
                        .HasColumnType("int");

                    b.Property<int>("orderTypeId")
                        .HasColumnType("int");

                    b.Property<double>("originalPrice")
                        .HasColumnType("float");

                    b.Property<int>("paymentId")
                        .HasColumnType("int");

                    b.Property<int>("statusId")
                        .HasColumnType("int");

                    b.Property<int>("tableId")
                        .HasColumnType("int");

                    b.Property<double>("totalDiscount")
                        .HasColumnType("float");

                    b.Property<double>("totalPrice")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("employeeId");

                    b.HasIndex("eventId");

                    b.HasIndex("orderTypeId");

                    b.HasIndex("paymentId");

                    b.HasIndex("statusId");

                    b.HasIndex("tableId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("project_sem4_api.Entities.OrderItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("dishId")
                        .HasColumnType("int");

                    b.Property<string>("node")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("orderID")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("dishId");

                    b.HasIndex("orderID");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("project_sem4_api.Entities.OrderType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("OrderTypes");
                });

            modelBuilder.Entity("project_sem4_api.Entities.PaymentMethord", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("PaymentMethords");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Restaurant_Table", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("statusId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("statusId");

                    b.ToTable("Restaurant_Tables");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("name")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("project_sem4_api.Entities.StatusDish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatusDishes");
                });

            modelBuilder.Entity("project_sem4_api.Entities.StatusNotifacation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("StatusNotifacations");
                });

            modelBuilder.Entity("project_sem4_api.Entities.StatusOrder", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("StatusOrders");
                });

            modelBuilder.Entity("project_sem4_api.Entities.StatusTable", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("StatusTables");
                });

            modelBuilder.Entity("project_sem4_api.Entities.TimeEmployee", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("timeEnd")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("timeStart")
                        .HasColumnType("time");

                    b.HasKey("id");

                    b.ToTable("TimeEmployees");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Dish", b =>
                {
                    b.HasOne("project_sem4_api.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_sem4_api.Entities.StatusDish", "StatusDish")
                        .WithMany()
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("StatusDish");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Employee", b =>
                {
                    b.HasOne("project_sem4_api.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_sem4_api.Entities.TimeEmployee", "TimeEmployee")
                        .WithMany()
                        .HasForeignKey("timeEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("TimeEmployee");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Notification", b =>
                {
                    b.HasOne("project_sem4_api.Entities.StatusNotifacation", "StatusNotifacation")
                        .WithMany()
                        .HasForeignKey("statusId");

                    b.HasOne("project_sem4_api.Entities.Restaurant_Table", "Restaurant_Table")
                        .WithMany()
                        .HasForeignKey("tableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant_Table");

                    b.Navigation("StatusNotifacation");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Order", b =>
                {
                    b.HasOne("project_sem4_api.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_sem4_api.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("eventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_sem4_api.Entities.OrderType", "OrderType")
                        .WithMany()
                        .HasForeignKey("orderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_sem4_api.Entities.PaymentMethord", "PaymentMethord")
                        .WithMany()
                        .HasForeignKey("paymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_sem4_api.Entities.StatusOrder", "StatusOrder")
                        .WithMany()
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_sem4_api.Entities.Restaurant_Table", "Restaurant_Table")
                        .WithMany()
                        .HasForeignKey("tableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Event");

                    b.Navigation("OrderType");

                    b.Navigation("PaymentMethord");

                    b.Navigation("Restaurant_Table");

                    b.Navigation("StatusOrder");
                });

            modelBuilder.Entity("project_sem4_api.Entities.OrderItem", b =>
                {
                    b.HasOne("project_sem4_api.Entities.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("dishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_sem4_api.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("orderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("project_sem4_api.Entities.Restaurant_Table", b =>
                {
                    b.HasOne("project_sem4_api.Entities.StatusTable", "StatusTable")
                        .WithMany()
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusTable");
                });
#pragma warning restore 612, 618
        }
    }
}
