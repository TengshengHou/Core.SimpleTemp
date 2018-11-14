using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.SimpleTemp.Repository.Migrations
{
    public partial class SysUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysRoleMenu_SysMenu_MenuId",
                table: "SysRoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_SysRoleMenu_SysRoleMenu_RoleId1_RoleMenuId",
                table: "SysRoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUserRole_SysRole_RoleId",
                table: "SysUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUserRole_SysUser_UserId",
                table: "SysUserRole");

            migrationBuilder.DropIndex(
                name: "IX_SysRoleMenu_RoleId1_RoleMenuId",
                table: "SysRoleMenu");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "SysRoleMenu");

            migrationBuilder.DropColumn(
                name: "RoleMenuId",
                table: "SysRoleMenu");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "SysUserRole",
                newName: "SysRoleId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SysUserRole",
                newName: "SysUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SysUserRole_RoleId",
                table: "SysUserRole",
                newName: "IX_SysUserRole_SysRoleId");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "SysRoleMenu",
                newName: "SysMenuId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "SysRoleMenu",
                newName: "SysRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_SysRoleMenu_MenuId",
                table: "SysRoleMenu",
                newName: "IX_SysRoleMenu_SysMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_SysRoleMenu_SysMenu_SysMenuId",
                table: "SysRoleMenu",
                column: "SysMenuId",
                principalTable: "SysMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysRoleMenu_SysRole_SysRoleId",
                table: "SysRoleMenu",
                column: "SysRoleId",
                principalTable: "SysRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUserRole_SysRole_SysRoleId",
                table: "SysUserRole",
                column: "SysRoleId",
                principalTable: "SysRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUserRole_SysUser_SysUserId",
                table: "SysUserRole",
                column: "SysUserId",
                principalTable: "SysUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysRoleMenu_SysMenu_SysMenuId",
                table: "SysRoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_SysRoleMenu_SysRole_SysRoleId",
                table: "SysRoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUserRole_SysRole_SysRoleId",
                table: "SysUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUserRole_SysUser_SysUserId",
                table: "SysUserRole");

            migrationBuilder.RenameColumn(
                name: "SysRoleId",
                table: "SysUserRole",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "SysUserId",
                table: "SysUserRole",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SysUserRole_SysRoleId",
                table: "SysUserRole",
                newName: "IX_SysUserRole_RoleId");

            migrationBuilder.RenameColumn(
                name: "SysMenuId",
                table: "SysRoleMenu",
                newName: "MenuId");

            migrationBuilder.RenameColumn(
                name: "SysRoleId",
                table: "SysRoleMenu",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_SysRoleMenu_SysMenuId",
                table: "SysRoleMenu",
                newName: "IX_SysRoleMenu_MenuId");

            migrationBuilder.AddColumn<int>(
                name: "RoleId1",
                table: "SysRoleMenu",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleMenuId",
                table: "SysRoleMenu",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleMenu_RoleId1_RoleMenuId",
                table: "SysRoleMenu",
                columns: new[] { "RoleId1", "RoleMenuId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SysRoleMenu_SysMenu_MenuId",
                table: "SysRoleMenu",
                column: "MenuId",
                principalTable: "SysMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysRoleMenu_SysRoleMenu_RoleId1_RoleMenuId",
                table: "SysRoleMenu",
                columns: new[] { "RoleId1", "RoleMenuId" },
                principalTable: "SysRoleMenu",
                principalColumns: new[] { "RoleId", "MenuId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUserRole_SysRole_RoleId",
                table: "SysUserRole",
                column: "RoleId",
                principalTable: "SysRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUserRole_SysUser_UserId",
                table: "SysUserRole",
                column: "UserId",
                principalTable: "SysUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
