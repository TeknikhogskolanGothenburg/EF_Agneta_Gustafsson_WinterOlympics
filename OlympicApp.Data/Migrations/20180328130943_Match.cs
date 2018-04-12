using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OlympicApp.Data.Migrations
{
    public partial class Match : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContestContestant",
                columns: table => new
                {
                    ContestId = table.Column<int>(nullable: false),
                    ContestantId = table.Column<int>(nullable: false),
                    Arena = table.Column<string>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    RefereeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestContestant", x => new { x.ContestId, x.ContestantId });
                    table.ForeignKey(
                        name: "FK_ContestContestant_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContestContestant_Contestant_ContestantId",
                        column: x => x.ContestantId,
                        principalTable: "Contestant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContestContestant_Referee_RefereeId",
                        column: x => x.RefereeId,
                        principalTable: "Referee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContestContestant_ContestantId",
                table: "ContestContestant",
                column: "ContestantId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestContestant_RefereeId",
                table: "ContestContestant",
                column: "RefereeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContestContestant");
        }
    }
}
