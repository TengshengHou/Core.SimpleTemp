using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.SimpleTemp.Repository.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysMenu_SysRole_SysRoleId",
                table: "SysMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_SysRole_SysUser_SysUserId",
                table: "SysRole");

            migrationBuilder.DropIndex(
                name: "IX_SysRole_SysUserId",
                table: "SysRole");

            migrationBuilder.DropIndex(
                name: "IX_SysMenu_SysRoleId",
                table: "SysMenu");

            migrationBuilder.DropColumn(
                name: "SysUserId",
                table: "SysRole");

            migrationBuilder.DropColumn(
                name: "SysRoleId",
                table: "SysMenu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SysUserId",
                table: "SysRole",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SysRoleId",
                table: "SysMenu",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysRole_SysUserId",
                table: "SysRole",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysMenu_SysRoleId",
                table: "SysMenu",
                column: "SysRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SysMenu_SysRole_SysRoleId",
                table: "SysMenu",
                column: "SysRoleId",
                principalTable: "SysRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysRole_SysUser_SysUserId",
                table: "SysRole",
                column: "SysUserId",
                principalTable: "SysUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
