using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OlympicApp.Data.Migrations
{
    public partial class Withdraw_Medal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Country_Medal_MedalId",
                table: "Country");

            migrationBuilder.DropTable(
                name: "Medal");

            migrationBuilder.DropIndex(
                name: "IX_Country_MedalId",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "MedalId",
                table: "Country");

            migrationBuilder.AddColumn<int>(
                name: "Bronze",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gold",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Silver",
                table: "Country",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bronze",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Gold",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Silver",
                table: "Country");

            migrationBuilder.AddColumn<int>(
                name: "MedalId",
                table: "Country",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Medal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bronze = table.Column<int>(nullable: true),
                    Gold = table.Column<int>(nullable: true),
                    Silver = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medal", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_MedalId",
                table: "Country",
                column: "MedalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Country_Medal_MedalId",
                table: "Country",
                column: "MedalId",
                principalTable: "Medal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
