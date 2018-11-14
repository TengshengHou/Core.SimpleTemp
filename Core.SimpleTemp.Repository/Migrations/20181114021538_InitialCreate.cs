using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.SimpleTemp.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysDepartment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Manager = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysDepartment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoginName = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    SysDepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysUser_SysDepartment_SysDepartmentId",
                        column: x => x.SysDepartmentId,
                        principalTable: "SysDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreateUserId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SysUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysRole_SysUser_SysUserId",
                        column: x => x.SysUserId,
                        principalTable: "SysUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SysRoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysMenu_SysRole_SysRoleId",
                        column: x => x.SysRoleId,
                        principalTable: "SysRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysUserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_SysUserRole_SysRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SysRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysUserRole_SysUser_UserId",
                        column: x => x.UserId,
                        principalTable: "SysUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysRoleMenu",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    RoleId1 = table.Column<int>(nullable: true),
                    RoleMenuId = table.Column<int>(nullable: true),
                    MenuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoleMenu", x => new { x.RoleId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_SysRoleMenu_SysMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "SysMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysRoleMenu_SysRoleMenu_RoleId1_RoleMenuId",
                        columns: x => new { x.RoleId1, x.RoleMenuId },
                        principalTable: "SysRoleMenu",
                        principalColumns: new[] { "RoleId", "MenuId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysMenu_SysRoleId",
                table: "SysMenu",
                column: "SysRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRole_SysUserId",
                table: "SysRole",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleMenu_MenuId",
                table: "SysRoleMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleMenu_RoleId1_RoleMenuId",
                table: "SysRoleMenu",
                columns: new[] { "RoleId1", "RoleMenuId" });

            migrationBuilder.CreateIndex(
                name: "IX_SysUser_SysDepartmentId",
                table: "SysUser",
                column: "SysDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserRole_RoleId",
                table: "SysUserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysRoleMenu");

            migrationBuilder.DropTable(
                name: "SysUserRole");

            migrationBuilder.DropTable(
                name: "SysMenu");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropTable(
                name: "SysUser");

            migrationBuilder.DropTable(
                name: "SysDepartment");
        }
    }
}
