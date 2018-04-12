using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OlympicApp.Data.Migrations
{
    public partial class Medal_Referee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Referee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referee_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_MedalId",
                table: "Country",
                column: "MedalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Referee_CountryId",
                table: "Referee",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_Medal_MedalId",
                table: "Country",
                column: "MedalId",
                principalTable: "Medal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Country_Medal_MedalId",
                table: "Country");

            migrationBuilder.DropTable(
                name: "Medal");

            migrationBuilder.DropTable(
                name: "Referee");

            migrationBuilder.DropIndex(
                name: "IX_Country_MedalId",
                table: "Country");
        }
    }
}
