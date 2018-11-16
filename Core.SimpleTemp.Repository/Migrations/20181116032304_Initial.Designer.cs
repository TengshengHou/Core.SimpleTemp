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
    [Migration("20181116032304_Initial")]
    partial class Initial
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
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<Guid>("CreateUserId");

                    b.Property<int>("IsDeleted");

                    b.Property<string>("Manager");

                    b.Property<string>("Name");

                    b.Property<Guid>("ParentId");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.ToTable("SysDepartment");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysMenu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Icon");

                    b.Property<string>("Name");

                    b.Property<Guid>("ParentId");

                    b.Property<string>("Remarks");

                    b.Property<int>("SerialNumber");

                    b.Property<int>("Type");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("SysMenu");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int>("CreateUserId");

                    b.Property<string>("Name");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.ToTable("SysRole");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysRoleMenu", b =>
                {
                    b.Property<Guid>("SysRoleId");

                    b.Property<Guid>("SysMenuId");

                    b.HasKey("SysRoleId", "SysMenuId");

                    b.HasIndex("SysMenuId");

                    b.ToTable("SysRoleMenu");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("LoginName")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasMaxLength(30);

                    b.Property<Guid?>("SysDepartmentId");

                    b.HasKey("Id");

                    b.HasIndex("SysDepartmentId");

                    b.ToTable("SysUser");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysUserRole", b =>
                {
                    b.Property<Guid>("SysUserId");

                    b.Property<Guid>("SysRoleId");

                    b.HasKey("SysUserId", "SysRoleId");

                    b.HasIndex("SysRoleId");

                    b.ToTable("SysUserRole");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysRoleMenu", b =>
                {
                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysMenu", "SysMenu")
                        .WithMany()
                        .HasForeignKey("SysMenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysRole", "SysRole")
                        .WithMany()
                        .HasForeignKey("SysRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysUser", b =>
                {
                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysDepartment")
                        .WithMany("Users")
                        .HasForeignKey("SysDepartmentId");
                });

            modelBuilder.Entity("Core.SimpleTemp.Domain.Entities.SysUserRole", b =>
                {
                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysRole", "SysRole")
                        .WithMany()
                        .HasForeignKey("SysRoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.SimpleTemp.Domain.Entities.SysUser", "SysUser")
                        .WithMany()
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
