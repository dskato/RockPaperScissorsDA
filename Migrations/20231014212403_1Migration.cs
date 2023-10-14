using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockPaperScissorsDA.Migrations
{
    public partial class _1Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "anon_user",
                columns: table => new
                {
                    AnonUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalGamesPlayed = table.Column<int>(type: "int", nullable: true),
                    TotalWins = table.Column<int>(type: "int", nullable: true),
                    TotalLosses = table.Column<int>(type: "int", nullable: true),
                    TotalDraws = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anon_user", x => x.AnonUserId);
                });

            migrationBuilder.CreateTable(
                name: "game",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserChoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComputerChoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnonUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_game_anon_user_AnonUserId",
                        column: x => x.AnonUserId,
                        principalTable: "anon_user",
                        principalColumn: "AnonUserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_AnonUserId",
                table: "game",
                column: "AnonUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game");

            migrationBuilder.DropTable(
                name: "anon_user");
        }
    }
}
