using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatedBibleBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BibleBooks_Bibles_BibleId",
                table: "BibleBooks");

            migrationBuilder.DropIndex(
                name: "IX_BibleBooks_BibleId",
                table: "BibleBooks");

            migrationBuilder.DropColumn(
                name: "ChapterCount",
                table: "BibleBooks");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BibleBooks",
                newName: "BookList");

            migrationBuilder.AddColumn<Guid>(
                name: "BobleBookId",
                table: "Bibles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BibleId",
                table: "BibleBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e756c817-bcb7-47b2-8e7b-52a6b3065cf4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cec6f062-e44c-42a8-b925-000522f32771", "AQAAAAIAAYagAAAAEC1+xn2sjWz5c0d8X57K1TRqxXd86xugtR7cL9KYkpZPhPIoVsVjZ+hvWh1LIbucxQ==", "9163aff7-1cdc-41e8-8af4-5943bbc5c3d7" });

            migrationBuilder.CreateIndex(
                name: "IX_BibleBooks_BibleId",
                table: "BibleBooks",
                column: "BibleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BibleBooks_Bibles_BibleId",
                table: "BibleBooks",
                column: "BibleId",
                principalTable: "Bibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BibleBooks_Bibles_BibleId",
                table: "BibleBooks");

            migrationBuilder.DropIndex(
                name: "IX_BibleBooks_BibleId",
                table: "BibleBooks");

            migrationBuilder.DropColumn(
                name: "BobleBookId",
                table: "Bibles");

            migrationBuilder.RenameColumn(
                name: "BookList",
                table: "BibleBooks",
                newName: "Name");

            migrationBuilder.AlterColumn<Guid>(
                name: "BibleId",
                table: "BibleBooks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "ChapterCount",
                table: "BibleBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e756c817-bcb7-47b2-8e7b-52a6b3065cf4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf6648ae-324f-48df-84cc-6d49a508f1b7", "AQAAAAIAAYagAAAAEE/aCQWgaYB7LOswIiL5PPtPOt4x6bkJZsxlADoMdZrpHlkZQA/awo/0j6VlHZlnYQ==", "2a11994b-bf6c-4bcf-8a77-b0b0d5f72fe9" });

            migrationBuilder.CreateIndex(
                name: "IX_BibleBooks_BibleId",
                table: "BibleBooks",
                column: "BibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BibleBooks_Bibles_BibleId",
                table: "BibleBooks",
                column: "BibleId",
                principalTable: "Bibles",
                principalColumn: "Id");
        }
    }
}
