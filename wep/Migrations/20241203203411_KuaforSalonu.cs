using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wep.Migrations
{
    public partial class KuaforSalonu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EmployeeSurname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EmployeeExperience = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    workingHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "servis",
                columns: table => new
                {
                    ServisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServisName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servis", x => x.ServisID);
                    table.ForeignKey(
                        name: "FK_servis_employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_servis_EmployeeID",
                table: "servis",
                column: "EmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "servis");

            migrationBuilder.DropTable(
                name: "employee");
        }
    }
}
