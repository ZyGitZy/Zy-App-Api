using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zy.App.Api.Migrations.ZyIds
{
    public partial class ZyIdsV001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ZyIds.Client",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ConsentLifetime = table.Column<int>(type: "int", nullable: false),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    RefreshTokenUsage = table.Column<int>(type: "int", nullable: false),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RefreshTokenExpiration = table.Column<int>(type: "int", nullable: false),
                    AccessTokenType = table.Column<int>(type: "int", nullable: false),
                    EnableLocalLogin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IncludeJwtId = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(type: "int", nullable: false),
                    IdentityTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    AccessTokenLifetime = table.Column<int>(type: "int", nullable: false),
                    ClientUri = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    RequireClientSecret = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowOfflineAccess = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RequireConsent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ClientClaimsPrefix = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    CreateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastUpdateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    LastUpdateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    RowVersion = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZyIds.Client", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ZyIds.ClientGrantType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    GrantType = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    CreateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastUpdateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    LastUpdateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    RowVersion = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZyIds.ClientGrantType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ZyIds.ClientRedirectUri",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RedirectUri = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    CreateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastUpdateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    LastUpdateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    RowVersion = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZyIds.ClientRedirectUri", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ZyIds.ClientScope",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    CreateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastUpdateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    LastUpdateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    RowVersion = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZyIds.ClientScope", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ZyIds.ClientSecret",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    RawValue = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    CreateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastUpdateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    LastUpdateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    RowVersion = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZyIds.ClientSecret", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ZyIds.PersistedGrant",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    CreateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastUpdateByUserId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    LastUpdateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    RowVersion = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZyIds.PersistedGrant", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZyIds.Client");

            migrationBuilder.DropTable(
                name: "ZyIds.ClientGrantType");

            migrationBuilder.DropTable(
                name: "ZyIds.ClientRedirectUri");

            migrationBuilder.DropTable(
                name: "ZyIds.ClientScope");

            migrationBuilder.DropTable(
                name: "ZyIds.ClientSecret");

            migrationBuilder.DropTable(
                name: "ZyIds.PersistedGrant");
        }
    }
}
