using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileService.DataAccess.Migrations
{
    public partial class RenameColumnFlashcardsTurnOverCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FlashcardsTurnOver",
                table: "StatsUserModels",
                newName: "FlashcardsTurnOverCount");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StatsUserModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FlashcardsTurnOverCount",
                table: "StatsUserModels",
                newName: "FlashcardsTurnOver");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "StatsUserModels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
