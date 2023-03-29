﻿// <auto-generated />
using System;
using FitnessClub.DAL.FitnessClubDataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FitnessClub.DAL.FitnessClubDataBase.Migrations
{
    [DbContext(typeof(FitnessClubContext))]
    partial class FitnessClubContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExerciseWorkout", b =>
                {
                    b.Property<string>("ExercisesCode")
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("WorkoutsGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExercisesCode", "WorkoutsGuid");

                    b.HasIndex("WorkoutsGuid");

                    b.ToTable("ExerciseWorkout");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers.User", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("UserRoleCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Guid");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("UserRoleCode");

                    b.ToTable("Users", "consumers");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.IndividualPlan", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cost")
                        .HasColumnType("money");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime>("EndedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(0)");

                    b.Property<Guid>("RequestGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Guid");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("RequestGuid");

                    b.ToTable("IndividualPlans");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.Request", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Porpose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestStatusCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserClientGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserManagerGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("RequestStatusCode");

                    b.HasIndex("UserClientGuid");

                    b.HasIndex("UserManagerGuid");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.Workout", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AssignedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(0)");

                    b.Property<Guid>("IndividualPlanGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(0)");

                    b.Property<byte>("NumberOfRepetitions")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("Pulse")
                        .HasColumnType("tinyint");

                    b.Property<byte>("QuantityPerWeek")
                        .HasColumnType("tinyint");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Guid");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("IndividualPlanGuid");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries.Exercise", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Code");

                    b.ToTable("Exercises", "dictionaries");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries.RequestStatus", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Code");

                    b.ToTable("RequestStatuses", "dictionaries");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries.UserRole", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Code");

                    b.ToTable("UserRoles", "dictionaries");
                });

            modelBuilder.Entity("ExerciseWorkout", b =>
                {
                    b.HasOne("FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries.Exercise", null)
                        .WithMany()
                        .HasForeignKey("ExercisesCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.Workout", null)
                        .WithMany()
                        .HasForeignKey("WorkoutsGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers.User", b =>
                {
                    b.HasOne("FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.IndividualPlan", b =>
                {
                    b.HasOne("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.Request", "Request")
                        .WithMany("IndividualPlans")
                        .HasForeignKey("RequestGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.Request", b =>
                {
                    b.HasOne("FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries.RequestStatus", "RequestStatus")
                        .WithMany("Requests")
                        .HasForeignKey("RequestStatusCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers.User", "UserClient")
                        .WithMany("ClientRequests")
                        .HasForeignKey("UserClientGuid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers.User", "UserManager")
                        .WithMany("ManagerRequests")
                        .HasForeignKey("UserManagerGuid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RequestStatus");

                    b.Navigation("UserClient");

                    b.Navigation("UserManager");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.Workout", b =>
                {
                    b.HasOne("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.IndividualPlan", "IndividualPlan")
                        .WithMany("Workouts")
                        .HasForeignKey("IndividualPlanGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IndividualPlan");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers.User", b =>
                {
                    b.Navigation("ClientRequests");

                    b.Navigation("ManagerRequests");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.IndividualPlan", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo.Request", b =>
                {
                    b.Navigation("IndividualPlans");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries.RequestStatus", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
