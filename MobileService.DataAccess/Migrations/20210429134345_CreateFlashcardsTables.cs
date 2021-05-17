using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileService.DataAccess.Migrations
{
    public partial class CreateFlashcardsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlashcardModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Native = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Foreign = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CollectionModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlashcardModels_Collections_CollectionModelId",
                        column: x => x.CollectionModelId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlashcardProgressModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PracticeDirection = table.Column<int>(type: "int", nullable: false),
                    PracticeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CorrectInRow = table.Column<int>(type: "int", nullable: false),
                    FlashcardModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardProgressModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlashcardProgressModels_FlashcardModels_FlashcardModelId",
                        column: x => x.FlashcardModelId,
                        principalTable: "FlashcardModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardModels_CollectionModelId",
                table: "FlashcardModels",
                column: "CollectionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardProgressModels_FlashcardModelId",
                table: "FlashcardProgressModels",
                column: "FlashcardModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashcardProgressModels");

            migrationBuilder.DropTable(
                name: "FlashcardModels");
        }
    }
}
