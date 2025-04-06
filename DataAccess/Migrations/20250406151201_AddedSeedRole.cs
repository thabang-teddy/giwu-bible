using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d04dce2-969a-435d-bba4-df3f325983dc", null, "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e756c817-bcb7-47b2-8e7b-52a6b3065cf4",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf6648ae-324f-48df-84cc-6d49a508f1b7", "admin@giwu.com", "ADMIN@GIWU.COM", "AQAAAAIAAYagAAAAEE/aCQWgaYB7LOswIiL5PPtPOt4x6bkJZsxlADoMdZrpHlkZQA/awo/0j6VlHZlnYQ==", "2a11994b-bf6c-4bcf-8a77-b0b0d5f72fe9" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8d04dce2-969a-435d-bba4-df3f325983dc", "e756c817-bcb7-47b2-8e7b-52a6b3065cf4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8d04dce2-969a-435d-bba4-df3f325983dc", "e756c817-bcb7-47b2-8e7b-52a6b3065cf4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e756c817-bcb7-47b2-8e7b-52a6b3065cf4",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7222a742-5c73-4b66-af1c-396b83465273", "admin@example.com", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEM59KzHDL3+Te8oCE9WUDaXeOQ9aqu8Dh0ga7YjAhL5wTPkKvw+VGtRnzov+yqcvaQ==", "93e17676-8459-45f2-b155-e9e88d4f9043" });
        }
    }
}
