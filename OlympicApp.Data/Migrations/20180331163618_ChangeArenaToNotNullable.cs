using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OlympicApp.Data.Migrations
{
    public partial class ChangeArenaToNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Arena",
                table: "ContestContestant",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Arena",
                table: "ContestContestant",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
