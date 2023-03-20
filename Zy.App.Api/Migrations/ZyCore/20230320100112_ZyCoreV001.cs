using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zy.App.Api.Migrations.ZyCore
{
    public partial class ZyCoreV001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenuEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", nullable: false, defaultValue: ""),
                    Path = table.Column<string>(type: "nvarchar(2000)", nullable: false, defaultValue: ""),
                    Sort = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ParentId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    FullPath = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IconName = table.Column<string>(type: "nvarchar(500)", nullable: false, defaultValue: ""),
                    CreateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastUpdateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    LastUpdateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    RowVersion = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuEntity", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleMenuEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    MenuId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastUpdateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    LastUpdateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    RowVersion = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenuEntity", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuEntity");

            migrationBuilder.DropTable(
                name: "RoleMenuEntity");
        }
    }
}
