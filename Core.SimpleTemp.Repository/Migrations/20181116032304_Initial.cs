using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.SimpleTemp.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "SysRoleId",
                table: "SysUserRole",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "SysUserId",
                table: "SysUserRole",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "SysDepartmentId",
                table: "SysUser",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "SysUser",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "LoginName",
                table: "SysUser",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "SysUser",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "SysMenuId",
                table: "SysRoleMenu",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "SysRoleId",
                table: "SysRoleMenu",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "SysRole",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "SysMenu",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "SysMenu",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "SysDepartment",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "SysDepartment",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SysRoleId",
                table: "SysUserRole",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "SysUserId",
                table: "SysUserRole",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "SysDepartmentId",
                table: "SysUser",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "SysUser",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginName",
                table: "SysUser",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SysUser",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "SysMenuId",
                table: "SysRoleMenu",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "SysRoleId",
                table: "SysRoleMenu",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SysRole",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "SysMenu",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SysMenu",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "SysDepartment",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SysDepartment",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
