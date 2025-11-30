using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixedfieldSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirtName",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e756c817-bcb7-47b2-8e7b-52a6b3065cf4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8de78bcd-409a-4927-99e3-5f24188737cc", "AQAAAAIAAYagAAAAELJq7Cyrbb7kDVkhJknllzX05o8mOyBC65d6BO0FC/rszmTS2/tKAXXlmmMcMwCjSg==", "04f61ca5-572d-4652-8a75-32b6336074c9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "FirtName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e756c817-bcb7-47b2-8e7b-52a6b3065cf4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed075907-cb41-4ea4-84a5-99c007d836be", "AQAAAAIAAYagAAAAENiByNb+895OfBgI5OW5IQrIp29tE7TcwonlhPJLFaiR0av3S12hpG0m3VTjupIUew==", "7fc9d2fb-f823-4e33-9b14-db9da753a705" });
        }
    }
}
