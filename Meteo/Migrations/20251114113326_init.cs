using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meteo.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreatorFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierUserId = table.Column<int>(type: "int", nullable: true),
                    ModifierFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreatorFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierUserId = table.Column<int>(type: "int", nullable: true),
                    ModifierFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    H256Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreatorFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierUserId = table.Column<int>(type: "int", nullable: true),
                    ModifierFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    HasAccess = table.Column<bool>(type: "bit", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreatorFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierUserId = table.Column<int>(type: "int", nullable: true),
                    ModifierFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreatorFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifierUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierUserId = table.Column<int>(type: "int", nullable: true),
                    ModifierFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "CreatorClientName", "CreatorFullName", "CreatorIp", "CreatorUserId", "CreatorUsername", "DateOfBirth", "DeleteTime", "EmployeeCode", "FirstName", "Gender", "H256Password", "IsActive", "IsDeleted", "LastName", "ModifierClientName", "ModifierFullName", "ModifierIp", "ModifierUserId", "ModifierUsername", "ModifyTime", "NationalCode", "Username" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "modelBuilder", "فاقد نام", "127.0.0.1", 0, "modelBuilder", null, null, null, null, null, "$2a$11$C6UzMDM.H6dfI/f/IKcEeOaTn5MZcZr1r4y0fUlOCV9f5cVHgZ7di", true, false, null, null, null, null, null, null, null, null, "Administrator" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
