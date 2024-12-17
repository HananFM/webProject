using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wep.Migrations
{
    public partial class KuafrSalonu4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rendezvou_servis_ServisID",
                table: "rendezvou");

            migrationBuilder.DropForeignKey(
                name: "FK_rendezvou_User_UserId",
                table: "rendezvou");

            migrationBuilder.DropForeignKey(
                name: "FK_servis_rendezvou_RendezvouID",
                table: "servis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_servis_RendezvouID",
                table: "servis");

            migrationBuilder.DropIndex(
                name: "IX_rendezvou_ServisID",
                table: "rendezvou");

            migrationBuilder.DropIndex(
                name: "IX_rendezvou_UserId",
                table: "rendezvou");

            migrationBuilder.DropColumn(
                name: "RendezvouID",
                table: "servis");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "user");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "rendezvou",
                newName: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "rendezvou",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "RendezvouID",
                table: "servis",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

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
                name: "FK_rendezvou_servis_ServisID",
                table: "rendezvou",
                column: "ServisID",
                principalTable: "servis",
                principalColumn: "ServisID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rendezvou_User_UserId",
                table: "rendezvou",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_servis_rendezvou_RendezvouID",
                table: "servis",
                column: "RendezvouID",
                principalTable: "rendezvou",
                principalColumn: "RendezvouID");
        }
    }
}
