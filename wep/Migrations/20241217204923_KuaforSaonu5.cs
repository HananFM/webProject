using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wep.Migrations
{
    public partial class KuaforSaonu5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_rendezvou_ServisID",
                table: "rendezvou",
                column: "ServisID");

            migrationBuilder.CreateIndex(
                name: "IX_rendezvou_UserID",
                table: "rendezvou",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_rendezvou_servis_ServisID",
                table: "rendezvou",
                column: "ServisID",
                principalTable: "servis",
                principalColumn: "ServisID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rendezvou_user_UserID",
                table: "rendezvou",
                column: "UserID",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rendezvou_servis_ServisID",
                table: "rendezvou");

            migrationBuilder.DropForeignKey(
                name: "FK_rendezvou_user_UserID",
                table: "rendezvou");

            migrationBuilder.DropIndex(
                name: "IX_rendezvou_ServisID",
                table: "rendezvou");

            migrationBuilder.DropIndex(
                name: "IX_rendezvou_UserID",
                table: "rendezvou");
        }
    }
}
