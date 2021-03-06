﻿// <auto-generated />
using HordeFlow.HR.Infrastructure;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace HordeFlow.HR.Migrations
{
    [DbContext(typeof(HrContext))]
    [Migration("20171119123439_InitialMigrate")]
    partial class InitialMigrate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(300);

                    b.Property<string>("AddressType")
                        .HasMaxLength(15);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<bool>("IsPrimary");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(20);

                    b.Property<int?>("StateId")
                        .IsRequired();

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("CompanyAddressId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("ParentCompanyId");

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("ParentCompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.CompanyAddress", b =>
                {
                    b.Property<int>("CompanyId");

                    b.Property<int>("AddressId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("CompanyId", "AddressId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("CompanyAddresses");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int?>("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId", "Name")
                        .IsUnique()
                        .HasFilter("[CompanyId] IS NOT NULL");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Designation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int?>("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId", "Name")
                        .IsUnique()
                        .HasFilter("[CompanyId] IS NOT NULL");

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Avatar")
                        .HasMaxLength(300);

                    b.Property<DateTime?>("Birthdate");

                    b.Property<string>("Citizenship")
                        .HasMaxLength(100);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("CompanyId")
                        .IsRequired();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int?>("DepartmentId");

                    b.Property<int?>("DesignationId");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("GSIS")
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<bool?>("IsDeleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("MaritalStatus");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("PHIC")
                        .HasMaxLength(50);

                    b.Property<string>("Religion")
                        .HasMaxLength(100);

                    b.Property<string>("SSS")
                        .HasMaxLength(50);

                    b.Property<string>("TIN")
                        .HasMaxLength(50);

                    b.Property<int?>("TeamId");

                    b.Property<string>("Title")
                        .HasMaxLength(20);

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DesignationId");

                    b.HasIndex("TeamId");

                    b.HasIndex("CompanyId", "Code");

                    b.HasIndex("CompanyId", "Id");

                    b.HasIndex("FirstName", "LastName")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.EmployeeAddress", b =>
                {
                    b.Property<int>("EmployeeId");

                    b.Property<int>("AddressId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("EmployeeId", "AddressId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("EmployeeAddresses");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.GroupRole", b =>
                {
                    b.Property<int>("GroupId");

                    b.Property<int>("RoleId");

                    b.HasKey("GroupId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("GroupRoles");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int?>("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<bool?>("IsDeleted");

                    b.Property<bool?>("IsSystemAdministrator")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("CountryId");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("State");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int?>("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId", "Name")
                        .IsUnique()
                        .HasFilter("[CompanyId] IS NOT NULL");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int?>("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("ConfirmPassword")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool?>("IsConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool?>("IsDeleted");

                    b.Property<bool?>("IsSystemAdministrator")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MobileNo")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(300);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50);

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("RecoveryEmail")
                        .HasMaxLength(50);

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(300);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<int?>("UserCreatedId");

                    b.Property<int?>("UserModifiedId");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.UserGroup", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("GroupId");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRoleClaim<int>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserClaim<int>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserLogin<int>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<int>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.RoleClaim", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>");


                    b.ToTable("RoleClaims");

                    b.HasDiscriminator().HasValue("RoleClaim");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.UserClaim", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>");


                    b.ToTable("UserClaims");

                    b.HasDiscriminator().HasValue("UserClaim");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.UserLogin", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>");


                    b.ToTable("UserLogins");

                    b.HasDiscriminator().HasValue("UserLogin");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.UserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<int>");


                    b.ToTable("UserRoles");

                    b.HasDiscriminator().HasValue("UserRole");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Address", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.State", "State")
                        .WithMany("Addresses")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Company", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Company", "ParentCompany")
                        .WithMany("Companies")
                        .HasForeignKey("ParentCompanyId");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.CompanyAddress", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Address", "Address")
                        .WithMany("CompanyAddresses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Company", "Company")
                        .WithMany("CompanyAddresses")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Department", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Company", "Company")
                        .WithMany("Departments")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Designation", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Company", "Company")
                        .WithMany("Designations")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Employee", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Designation", "Designation")
                        .WithMany("Employees")
                        .HasForeignKey("DesignationId");

                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Team", "Team")
                        .WithMany("Employees")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.EmployeeAddress", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Address", "Address")
                        .WithMany("EmployeeAddresses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Employee", "Employee")
                        .WithMany("EmployeeAddresses")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Group", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.GroupRole", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Group", "Group")
                        .WithMany("Roles")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Role", "Role")
                        .WithMany("Groups")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Role", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.State", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.Team", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Company", "Company")
                        .WithMany("Teams")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.User", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("HordeFlow.HR.Infrastructure.Models.UserGroup", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HordeFlow.HR.Infrastructure.Models.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HordeFlow.HR.Infrastructure.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("HordeFlow.HR.Infrastructure.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
