using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileService.DataAccess.Migrations
{
    public partial class ChangeTablesNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashcardModels_Collections_CollectionModelId",
                table: "FlashcardModels");

            migrationBuilder.DropForeignKey(
                name: "FK_FlashcardProgressModels_FlashcardModels_FlashcardModelId",
                table: "FlashcardProgressModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlashcardProgressModels",
                table: "FlashcardProgressModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlashcardModels",
                table: "FlashcardModels");

            migrationBuilder.RenameTable(
                name: "FlashcardProgressModels",
                newName: "FlashcardProgresses");

            migrationBuilder.RenameTable(
                name: "FlashcardModels",
                newName: "Flashcards");

            migrationBuilder.RenameIndex(
                name: "IX_FlashcardProgressModels_FlashcardModelId",
                table: "FlashcardProgresses",
                newName: "IX_FlashcardProgresses_FlashcardModelId");

            migrationBuilder.RenameIndex(
                name: "IX_FlashcardModels_CollectionModelId",
                table: "Flashcards",
                newName: "IX_Flashcards_CollectionModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlashcardProgresses",
                table: "FlashcardProgresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flashcards",
                table: "Flashcards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashcardProgresses_Flashcards_FlashcardModelId",
                table: "FlashcardProgresses",
                column: "FlashcardModelId",
                principalTable: "Flashcards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_Collections_CollectionModelId",
                table: "Flashcards",
                column: "CollectionModelId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashcardProgresses_Flashcards_FlashcardModelId",
                table: "FlashcardProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_Collections_CollectionModelId",
                table: "Flashcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flashcards",
                table: "Flashcards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlashcardProgresses",
                table: "FlashcardProgresses");

            migrationBuilder.RenameTable(
                name: "Flashcards",
                newName: "FlashcardModels");

            migrationBuilder.RenameTable(
                name: "FlashcardProgresses",
                newName: "FlashcardProgressModels");

            migrationBuilder.RenameIndex(
                name: "IX_Flashcards_CollectionModelId",
                table: "FlashcardModels",
                newName: "IX_FlashcardModels_CollectionModelId");

            migrationBuilder.RenameIndex(
                name: "IX_FlashcardProgresses_FlashcardModelId",
                table: "FlashcardProgressModels",
                newName: "IX_FlashcardProgressModels_FlashcardModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlashcardModels",
                table: "FlashcardModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlashcardProgressModels",
                table: "FlashcardProgressModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashcardModels_Collections_CollectionModelId",
                table: "FlashcardModels",
                column: "CollectionModelId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlashcardProgressModels_FlashcardModels_FlashcardModelId",
                table: "FlashcardProgressModels",
                column: "FlashcardModelId",
                principalTable: "FlashcardModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
