using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class improvedTableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_BibleBooks_BobleBookId",
                table: "Chapters");

            migrationBuilder.RenameColumn(
                name: "BobleBookId",
                table: "Chapters",
                newName: "BibleBookId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_BobleBookId",
                table: "Chapters",
                newName: "IX_Chapters_BibleBookId");

            migrationBuilder.AddColumn<int>(
                name: "Book",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e756c817-bcb7-47b2-8e7b-52a6b3065cf4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "613c11c6-e741-4a41-87e0-1e4738d472f0", "AQAAAAIAAYagAAAAEJPDoh2vtkCb4DeY5jDAxP4nBM1m4f7BnE2WvobU9J1lS/x5nn8Q4kfiwQU6aImvXg==", "3592d21a-d041-44bd-a200-b92cd0e74350" });

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_BibleBooks_BibleBookId",
                table: "Chapters",
                column: "BibleBookId",
                principalTable: "BibleBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_BibleBooks_BibleBookId",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "Book",
                table: "Chapters");

            migrationBuilder.RenameColumn(
                name: "BibleBookId",
                table: "Chapters",
                newName: "BobleBookId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_BibleBookId",
                table: "Chapters",
                newName: "IX_Chapters_BobleBookId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e756c817-bcb7-47b2-8e7b-52a6b3065cf4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cec6f062-e44c-42a8-b925-000522f32771", "AQAAAAIAAYagAAAAEC1+xn2sjWz5c0d8X57K1TRqxXd86xugtR7cL9KYkpZPhPIoVsVjZ+hvWh1LIbucxQ==", "9163aff7-1cdc-41e8-8af4-5943bbc5c3d7" });

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_BibleBooks_BobleBookId",
                table: "Chapters",
                column: "BobleBookId",
                principalTable: "BibleBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
