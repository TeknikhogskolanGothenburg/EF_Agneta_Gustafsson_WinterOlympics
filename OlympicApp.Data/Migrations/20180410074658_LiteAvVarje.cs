using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OlympicApp.Data.Migrations
{
    public partial class LiteAvVarje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestContestant_Referee_RefereeId",
                table: "ContestContestant");

            migrationBuilder.AlterColumn<int>(
                name: "RefereeId",
                table: "ContestContestant",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestContestant_Referee_RefereeId",
                table: "ContestContestant",
                column: "RefereeId",
                principalTable: "Referee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestContestant_Referee_RefereeId",
                table: "ContestContestant");

            migrationBuilder.AlterColumn<int>(
                name: "RefereeId",
                table: "ContestContestant",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ContestContestant_Referee_RefereeId",
                table: "ContestContestant",
                column: "RefereeId",
                principalTable: "Referee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
