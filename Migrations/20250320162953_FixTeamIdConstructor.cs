using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FightSquad___CLI.Migrations
{
    /// <inheritdoc />
    public partial class FixTeamIdConstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Fighters",
                newName: "FighterId");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Fighters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fighters_TeamId",
                table: "Fighters",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fighters_Teams_TeamId",
                table: "Fighters",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fighters_Teams_TeamId",
                table: "Fighters");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Fighters_TeamId",
                table: "Fighters");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Fighters");

            migrationBuilder.RenameColumn(
                name: "FighterId",
                table: "Fighters",
                newName: "Id");
        }
    }
}
