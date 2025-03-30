using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewFieldsToBible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d504b801-3159-4524-8147-2e060b25e026");

            migrationBuilder.AddColumn<string>(
                name: "LagacyId",
                table: "Bibles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RootTable",
                table: "Bibles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RootUrl",
                table: "Bibles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AppData", "BookMarkId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirtName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c95d534a-2773-44c1-a143-1b9f3f443784", 0, null, null, "af7235d9-cace-46be-9fa6-2e877d5c8a54", "admin@example.com", true, "Admin", "Giwu", false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEK3GcuhQulFnHSxSlIS1ArR93tUxDiNViZ/vqnTMg0ZpMkwXuLE2IInvXl8coJH8Ug==", null, false, "39e5c933-a1bb-4b87-8623-436d08843421", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c95d534a-2773-44c1-a143-1b9f3f443784");

            migrationBuilder.DropColumn(
                name: "LagacyId",
                table: "Bibles");

            migrationBuilder.DropColumn(
                name: "RootTable",
                table: "Bibles");

            migrationBuilder.DropColumn(
                name: "RootUrl",
                table: "Bibles");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AppData", "BookMarkId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirtName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d504b801-3159-4524-8147-2e060b25e026", 0, null, null, "54f537d7-f0e8-4731-a3f9-bb52f6754e3a", "admin@example.com", true, "Admin", "Giwu", false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEHdGk5D04t5doLoVAmIasHq4dBMFu83EjnVVqkcWqSeTBvLHfFoovyRyvDKd49TbmQ==", null, false, "e9bccd76-1f31-4635-96ee-430f4d072f4d", false, "admin" });
        }
    }
}
