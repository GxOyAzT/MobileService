using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileService.DataAccess.Migrations
{
    public partial class CreateStatisticsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatsUserModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlashcardsTurnOver = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsUserModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatsUserModels");
        }
    }
}
