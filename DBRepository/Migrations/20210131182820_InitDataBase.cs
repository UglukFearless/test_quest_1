/****************************************************************************
*  Файл: 20210131182820_InitDataBase.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение класса InitDataBase
****************************************************************************/

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DBRepository.Migrations
{
    /// <summary>
    /// Инициализация базы данных
    /// </summary>
    public partial class InitDataBase : Migration
    {
        // применение миграции
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Таблица Ролей пользователей в системе безопасности
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            // Таблица Пользователей в контексте системы безопасности
            migrationBuilder.CreateTable(
                name: "SecurityUsers",
                columns: table => new
                {
                    SecurityUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityUsers", x => x.SecurityUserId);
                });

            // Таблица Разрешений системы безопасности
            migrationBuilder.CreateTable(
                name: "ClaimsDefenitions",
                columns: table => new
                {
                    ClaimsDefenitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimsDefenitions", x => x.ClaimsDefenitionId);
                    table.ForeignKey(
                        name: "FK_ClaimsDefenitions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            // Таблица Токенов обновления
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeOfDeath = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Session = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_SecurityUsers_SecurityUserId",
                        column: x => x.SecurityUserId,
                        principalTable: "SecurityUsers",
                        principalColumn: "SecurityUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            // Таблица связей Пользователей и Ролей
            migrationBuilder.CreateTable(
                name: "RoleSecurityUser",
                columns: table => new
                {
                    RolesRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecurityUsersSecurityUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSecurityUser", x => new { x.RolesRoleId, x.SecurityUsersSecurityUserId });
                    table.ForeignKey(
                        name: "FK_RoleSecurityUser_Roles_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleSecurityUser_SecurityUsers_SecurityUsersSecurityUserId",
                        column: x => x.SecurityUsersSecurityUserId,
                        principalTable: "SecurityUsers",
                        principalColumn: "SecurityUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            // Создание ключей уникальности
            migrationBuilder.CreateIndex(
                name: "IX_ClaimsDefenitions_RoleId_Value",
                table: "ClaimsDefenitions",
                columns: new[] { "RoleId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_SecurityUserId",
                table: "RefreshTokens",
                column: "SecurityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Session",
                table: "RefreshTokens",
                column: "Session",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Code",
                table: "Roles",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleSecurityUser_SecurityUsersSecurityUserId",
                table: "RoleSecurityUser",
                column: "SecurityUsersSecurityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityUsers_Login",
                table: "SecurityUsers",
                column: "Login",
                unique: true);
        }

        // откат миграции
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimsDefenitions");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "RoleSecurityUser");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SecurityUsers");
        }
    }
}
