﻿// <auto-generated />
using System;
using Core.SimpleTemp.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.SimpleTemp.Repository.Migrations
{
    [DbContext(typeof(CoreDBContext))]
    [Migration("20181114021538_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<Guid>("CreateUserId");

                    b.Property<int>("IsDeleted");

                    b.Property<string>("Manager");

                    b.Property<string>("Name");

                    b.Property<int>("ParentId");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.ToTable("SysDepartment");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Icon");

                    b.Property<string>("Name");

                    b.Property<int>("ParentId");

                    b.Property<string>("Remarks");

                    b.Property<int>("SerialNumber");

                    b.Property<int?>("SysRoleId");

                    b.Property<int>("Type");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("SysRoleId");

                    b.ToTable("SysMenu");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int>("CreateUserId");

                    b.Property<string>("Name");

                    b.Property<string>("Remarks");

                    b.Property<int?>("SysUserId");

                    b.HasKey("Id");

                    b.HasIndex("SysUserId");

                    b.ToTable("SysRole");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysRoleMenu", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("MenuId");

                    b.Property<int?>("RoleId1");

                    b.Property<int?>("RoleMenuId");

                    b.HasKey("RoleId", "MenuId");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId1", "RoleMenuId");

                    b.ToTable("SysRoleMenu");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int?>("SysDepartmentId");

                    b.HasKey("Id");

                    b.HasIndex("SysDepartmentId");

                    b.ToTable("SysUser");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysUserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("SysUserRole");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysMenu", b =>
                {
                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysRole")
                        .WithMany("RoleMenus")
                        .HasForeignKey("SysRoleId");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysRole", b =>
                {
                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysUser")
                        .WithMany("SysRole")
                        .HasForeignKey("SysUserId");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysRoleMenu", b =>
                {
                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysMenu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysRoleMenu", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId1", "RoleMenuId");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysUser", b =>
                {
                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysDepartment")
                        .WithMany("Users")
                        .HasForeignKey("SysDepartmentId");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysUserRole", b =>
                {
                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}