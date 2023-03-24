using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zy.App.Api.Migrations.ZyUser
{
    public partial class ZyUserV003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "User.UserRole",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "User.UserRole");
        }
    }
}
