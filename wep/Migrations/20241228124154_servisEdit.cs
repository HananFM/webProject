using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wep.Migrations
{
    public partial class servisEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22c01966-c7d7-48a1-8583-c697b9c5aef2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8584f54-e6e5-44b1-8360-53e131a3b673");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da9461f0-69f7-463e-83d7-7fc383b79acd");

            migrationBuilder.AddColumn<string>(
                name: "ServisDuration",
                table: "servis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36f6efce-f842-4e0c-a01c-790058990d40", "a56195b7-7af6-4dbb-974b-56bb52392806", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a0df84d5-da9a-4a67-92d7-c07c70303b40", "2a58bf49-b9fa-4006-8322-d6779f8d7453", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b0e28afe-c686-43cb-a169-7f5a80934efb", "ddf306df-b9f8-4d10-bcf4-ee3611035599", "employee", "employee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36f6efce-f842-4e0c-a01c-790058990d40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0df84d5-da9a-4a67-92d7-c07c70303b40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0e28afe-c686-43cb-a169-7f5a80934efb");

            migrationBuilder.DropColumn(
                name: "ServisDuration",
                table: "servis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "22c01966-c7d7-48a1-8583-c697b9c5aef2", "be13fd4e-5c38-47ba-82e0-32998f735dde", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b8584f54-e6e5-44b1-8360-53e131a3b673", "4bf2c936-2585-4917-a44c-6771fffb9d32", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "da9461f0-69f7-463e-83d7-7fc383b79acd", "40b25c0f-f28d-469c-9213-3b571450799d", "employee", "employee" });
        }
    }
}
