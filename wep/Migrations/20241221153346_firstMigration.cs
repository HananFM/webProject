using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wep.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "584e5ca0-3315-4561-bee3-b5fcffa4775a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b369aef6-1489-45ce-a75d-794e2a5acbd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7aa4d4d-5ca5-429c-868c-5fb7457c0e93");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "584e5ca0-3315-4561-bee3-b5fcffa4775a", "3edfb1a5-89cd-4940-99f3-82cae5eb1da9", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b369aef6-1489-45ce-a75d-794e2a5acbd8", "43cfb66d-6b00-4f92-b5aa-e6aa39cdbee1", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7aa4d4d-5ca5-429c-868c-5fb7457c0e93", "89b83c64-2d74-47cc-b14a-38b53289e80d", "employee", "employee" });
        }
    }
}
