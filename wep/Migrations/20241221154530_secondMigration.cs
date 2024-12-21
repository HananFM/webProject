using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wep.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50a3e0e2-2d88-487e-8ae0-7868dd0727ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f94682b-69a1-49bf-ad27-3f1f72aa78ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb9c9f97-e5c7-45db-98c9-f37ba035509b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aace3aec-62a7-4140-b05b-f7f0ecf5bd59", "b6b8e004-bba7-4078-b352-040145415e03", "employee", "employee" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e36f47ca-41c8-4a06-91b8-234f06500207", "d3365133-020d-4b0c-b0db-e042dbb3d9bc", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f9193730-0938-41a8-abf2-36999130b55b", "750413d4-f8ba-42e1-92b7-3a4ae4ea25d9", "client", "client" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aace3aec-62a7-4140-b05b-f7f0ecf5bd59");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e36f47ca-41c8-4a06-91b8-234f06500207");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9193730-0938-41a8-abf2-36999130b55b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50a3e0e2-2d88-487e-8ae0-7868dd0727ce", "65d1ce4e-60ef-473e-81ad-3cbd2b8d1e61", "client", "client" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f94682b-69a1-49bf-ad27-3f1f72aa78ec", "beba192f-de1f-4c7c-bed4-7048785a145f", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb9c9f97-e5c7-45db-98c9-f37ba035509b", "659b8ad6-636e-432b-85f9-11c768241748", "employee", "employee" });
        }
    }
}
