using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meteo.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "CreatorClientName", "CreatorFullName", "CreatorIp", "CreatorUserId", "CreatorUsername", "DateOfBirth", "DeleteTime", "EmployeeCode", "FirstName", "Gender", "H256Password", "IsActive", "IsDeleted", "LastName", "ModifierClientName", "ModifierFullName", "ModifierIp", "ModifierUserId", "ModifierUsername", "ModifyTime", "NationalCode", "Username" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "modelBuilder", "فاقد نام", "127.0.0.1", 0, "modelBuilder", null, null, null, null, null, "$2a$11$C6UzMDM.H6dfI/f/IKcEeOaTn5MZcZr1r4y0fUlOCV9f5cVHgZ7di", true, false, null, null, null, null, null, null, null, null, "Administrator" });
        }
    }
}
