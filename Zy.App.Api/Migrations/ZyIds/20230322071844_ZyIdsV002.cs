using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zy.App.Api.Migrations.ZyIds
{
    public partial class ZyIdsV002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ZyIds.PersistedGrant",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectId",
                table: "ZyIds.PersistedGrant",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ZyIds.PersistedGrant",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expiration",
                table: "ZyIds.PersistedGrant",
                type: "datetime(6)",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "ZyIds.PersistedGrant",
                type: "nvarchar(4000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "ZyIds.PersistedGrant",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "ZyIds.PersistedGrant",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ZyIds.ClientSecret",
                type: "nvarchar(4000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ZyIds.ClientSecret",
                type: "nvarchar(500)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "RawValue",
                table: "ZyIds.ClientSecret",
                type: "nvarchar(4000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expiration",
                table: "ZyIds.ClientSecret",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ZyIds.ClientSecret",
                type: "nvarchar(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "ZyIds.ClientSecret",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "ZyIds.ClientSecret",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Scope",
                table: "ZyIds.ClientScope",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "ZyIds.ClientScope",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "RedirectUri",
                table: "ZyIds.ClientRedirectUri",
                type: "nvarchar(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "ZyIds.ClientRedirectUri",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "GrantType",
                table: "ZyIds.ClientGrantType",
                type: "nvarchar(500)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "ZyIds.ClientGrantType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<bool>(
                name: "UpdateAccessTokenClaimsOnRefresh",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<int>(
                name: "SlidingRefreshTokenLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "RequireConsent",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "RequireClientSecret",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<int>(
                name: "RefreshTokenUsage",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RefreshTokenExpiration",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IncludeJwtId",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<int>(
                name: "IdentityTokenLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "EnableLocalLogin",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<int>(
                name: "ConsentLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ClientUri",
                table: "ZyIds.Client",
                type: "nvarchar(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "ZyIds.Client",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "ZyIds.Client",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientClaimsPrefix",
                table: "ZyIds.Client",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorizationCodeLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "AllowOfflineAccess",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "AllowAccessTokensViaBrowser",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<int>(
                name: "AccessTokenType",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AccessTokenLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AbsoluteRefreshTokenLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ZyIds.Client",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ZyIds.Client");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ZyIds.PersistedGrant",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectId",
                table: "ZyIds.PersistedGrant",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ZyIds.PersistedGrant",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expiration",
                table: "ZyIds.PersistedGrant",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "ZyIds.PersistedGrant",
                type: "nvarchar(4000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "ZyIds.PersistedGrant",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "ZyIds.PersistedGrant",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ZyIds.ClientSecret",
                type: "nvarchar(4000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ZyIds.ClientSecret",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RawValue",
                table: "ZyIds.ClientSecret",
                type: "nvarchar(4000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expiration",
                table: "ZyIds.ClientSecret",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ZyIds.ClientSecret",
                type: "nvarchar(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "ZyIds.ClientSecret",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "ZyIds.ClientSecret",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Scope",
                table: "ZyIds.ClientScope",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "ZyIds.ClientScope",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "RedirectUri",
                table: "ZyIds.ClientRedirectUri",
                type: "nvarchar(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "ZyIds.ClientRedirectUri",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "GrantType",
                table: "ZyIds.ClientGrantType",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "ZyIds.ClientGrantType",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);

            migrationBuilder.AlterColumn<bool>(
                name: "UpdateAccessTokenClaimsOnRefresh",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "SlidingRefreshTokenLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "RequireConsent",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "RequireClientSecret",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "RefreshTokenUsage",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RefreshTokenExpiration",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IncludeJwtId",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "IdentityTokenLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "EnableLocalLogin",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ConsentLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ClientUri",
                table: "ZyIds.Client",
                type: "nvarchar(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "ZyIds.Client",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "ZyIds.Client",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ClientClaimsPrefix",
                table: "ZyIds.Client",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorizationCodeLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "AllowOfflineAccess",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AllowAccessTokensViaBrowser",
                table: "ZyIds.Client",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "AccessTokenType",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AccessTokenLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AbsoluteRefreshTokenLifetime",
                table: "ZyIds.Client",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
