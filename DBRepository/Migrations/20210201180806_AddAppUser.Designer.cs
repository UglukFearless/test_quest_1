﻿// <auto-generated />
using System;
using DBRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DBRepository.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20210201180806_AddAppUser")]
    partial class AddAppUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Models.Common.AppUser", b =>
                {
                    b.Property<Guid>("AppUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("Registrated")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("SecurityUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AppUserId");

                    b.HasIndex("Nickname")
                        .IsUnique();

                    b.HasIndex("SecurityUserId");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Models.Security.ClaimsDefenition", b =>
                {
                    b.Property<Guid>("ClaimsDefenitionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClaimsDefenitionId");

                    b.HasIndex("RoleId", "Value")
                        .IsUnique();

                    b.ToTable("ClaimsDefenitions");
                });

            modelBuilder.Entity("Models.Security.RefreshToken", b =>
                {
                    b.Property<Guid>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SecurityUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Session")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeOfDeath")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("SecurityUserId");

                    b.HasIndex("Session")
                        .IsUnique();

                    b.HasIndex("Token")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Models.Security.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Models.Security.SecurityUser", b =>
                {
                    b.Property<Guid>("SecurityUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SecurityUserId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("SecurityUsers");
                });

            modelBuilder.Entity("RoleSecurityUser", b =>
                {
                    b.Property<Guid>("RolesRoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SecurityUsersSecurityUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RolesRoleId", "SecurityUsersSecurityUserId");

                    b.HasIndex("SecurityUsersSecurityUserId");

                    b.ToTable("RoleSecurityUser");
                });

            modelBuilder.Entity("Models.Common.AppUser", b =>
                {
                    b.HasOne("Models.Security.SecurityUser", "SecurityUser")
                        .WithMany()
                        .HasForeignKey("SecurityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SecurityUser");
                });

            modelBuilder.Entity("Models.Security.ClaimsDefenition", b =>
                {
                    b.HasOne("Models.Security.Role", "Role")
                        .WithMany("ClaimsDefenitions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Models.Security.RefreshToken", b =>
                {
                    b.HasOne("Models.Security.SecurityUser", "SecurityUser")
                        .WithMany()
                        .HasForeignKey("SecurityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SecurityUser");
                });

            modelBuilder.Entity("RoleSecurityUser", b =>
                {
                    b.HasOne("Models.Security.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Security.SecurityUser", null)
                        .WithMany()
                        .HasForeignKey("SecurityUsersSecurityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Security.Role", b =>
                {
                    b.Navigation("ClaimsDefenitions");
                });
#pragma warning restore 612, 618
        }
    }
}
