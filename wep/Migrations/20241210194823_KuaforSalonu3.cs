using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wep.Migrations
{
    public partial class KuaforSalonu3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ServisFee",
                table: "servis",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "RendezvouID",
                table: "servis",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "rendezvou",
                columns: table => new
                {
                    RendezvouID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandezvouTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServisID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rendezvou", x => x.RendezvouID);
                    table.ForeignKey(
                        name: "FK_rendezvou_servis_ServisID",
                        column: x => x.ServisID,
                        principalTable: "servis",
                        principalColumn: "ServisID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rendezvou_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_servis_RendezvouID",
                table: "servis",
                column: "RendezvouID");

            migrationBuilder.CreateIndex(
                name: "IX_rendezvou_ServisID",
                table: "rendezvou",
                column: "ServisID");

            migrationBuilder.CreateIndex(
                name: "IX_rendezvou_UserId",
                table: "rendezvou",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_servis_rendezvou_RendezvouID",
                table: "servis",
                column: "RendezvouID",
                principalTable: "rendezvou",
                principalColumn: "RendezvouID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_servis_rendezvou_RendezvouID",
                table: "servis");

            migrationBuilder.DropTable(
                name: "rendezvou");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_servis_RendezvouID",
                table: "servis");

            migrationBuilder.DropColumn(
                name: "RendezvouID",
                table: "servis");

            migrationBuilder.AlterColumn<string>(
                name: "ServisFee",
                table: "servis",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
