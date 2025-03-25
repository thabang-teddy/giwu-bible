using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profile",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppData",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AppData", "BookMarkId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirtName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d504b801-3159-4524-8147-2e060b25e026", 0, null, null, "54f537d7-f0e8-4731-a3f9-bb52f6754e3a", "admin@example.com", true, "Admin", "Giwu", false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEHdGk5D04t5doLoVAmIasHq4dBMFu83EjnVVqkcWqSeTBvLHfFoovyRyvDKd49TbmQ==", null, false, "e9bccd76-1f31-4635-96ee-430f4d072f4d", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d504b801-3159-4524-8147-2e060b25e026");

            migrationBuilder.DropColumn(
                name: "AppData",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Profile",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
