using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pri.CleanArchitecture.Music.Infrastructure.Migrations
{
    public partial class AddIdentitySeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRecord_AspNetUsers_UsersId",
                table: "ApplicationUserRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserRecord",
                table: "ApplicationUserRecord");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserRecord_UsersId",
                table: "ApplicationUserRecord");

            migrationBuilder.DeleteData(
                table: "PropertyRecord",
                keyColumns: new[] { "PropertiesId", "RecordsId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ApplicationUserRecord",
                newName: "ApplicationUsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserRecord",
                table: "ApplicationUserRecord",
                columns: new[] { "ApplicationUsersId", "RecordsId" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "0816d758-d569-4b0b-acac-b7b84ac28bba", new DateTime(2024, 3, 27, 15, 5, 45, 371, DateTimeKind.Local).AddTicks(6952), "admin@music.com", true, "Bart", "Soete", false, null, "ADMIN@MUSIC.COM", "ADMIN@MUSIC.COM", "AQAAAAEAACcQAAAAEG5j1ZoavTZPumZPZBdySEedWc/RsvoFRFjuhdQG1e7VIafrD1Eewp02DkLsmctY2g==", null, false, "8c8a217c-ddfc-46cc-8080-3967628ac7c4", false, "admin@music.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2", 0, "1daf9db7-03f1-4506-bbc5-659c8c161532", new DateTime(2024, 3, 27, 15, 5, 45, 371, DateTimeKind.Local).AddTicks(7014), "user@music.com", true, "Mileto", "Di Marco", false, null, "USER@MUSIC.COM", "USER@MUSIC.COM", "AQAAAAEAACcQAAAAEOn0RJcl5yKtAC0nXg6RvM1tvqvB0sf646/lkqyklTqtQ5QzaAkRN6q/S4pQYoRQQQ==", null, false, "46e5e895-e332-4fda-85bf-9f0b5f9cda5e", false, "user@music.com" });

            migrationBuilder.InsertData(
                table: "ApplicationUserRecord",
                columns: new[] { "ApplicationUsersId", "RecordsId" },
                values: new object[,]
                {
                    { "1", 1 },
                    { "1", 2 },
                    { "1", 3 },
                    { "2", 1 },
                    { "2", 2 },
                    { "2", 3 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin", "1" },
                    { 2, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "User", "2" },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "27/03/2024 15:05:45", "1" },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "27/03/2024 15:05:45", "2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRecord_RecordsId",
                table: "ApplicationUserRecord",
                column: "RecordsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRecord_AspNetUsers_ApplicationUsersId",
                table: "ApplicationUserRecord",
                column: "ApplicationUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRecord_AspNetUsers_ApplicationUsersId",
                table: "ApplicationUserRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserRecord",
                table: "ApplicationUserRecord");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserRecord_RecordsId",
                table: "ApplicationUserRecord");

            migrationBuilder.DeleteData(
                table: "ApplicationUserRecord",
                keyColumns: new[] { "ApplicationUsersId", "RecordsId" },
                keyValues: new object[] { "1", 1 });

            migrationBuilder.DeleteData(
                table: "ApplicationUserRecord",
                keyColumns: new[] { "ApplicationUsersId", "RecordsId" },
                keyValues: new object[] { "1", 2 });

            migrationBuilder.DeleteData(
                table: "ApplicationUserRecord",
                keyColumns: new[] { "ApplicationUsersId", "RecordsId" },
                keyValues: new object[] { "1", 3 });

            migrationBuilder.DeleteData(
                table: "ApplicationUserRecord",
                keyColumns: new[] { "ApplicationUsersId", "RecordsId" },
                keyValues: new object[] { "2", 1 });

            migrationBuilder.DeleteData(
                table: "ApplicationUserRecord",
                keyColumns: new[] { "ApplicationUsersId", "RecordsId" },
                keyValues: new object[] { "2", 2 });

            migrationBuilder.DeleteData(
                table: "ApplicationUserRecord",
                keyColumns: new[] { "ApplicationUsersId", "RecordsId" },
                keyValues: new object[] { "2", 3 });

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.RenameColumn(
                name: "ApplicationUsersId",
                table: "ApplicationUserRecord",
                newName: "UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserRecord",
                table: "ApplicationUserRecord",
                columns: new[] { "RecordsId", "UsersId" });

            migrationBuilder.InsertData(
                table: "PropertyRecord",
                columns: new[] { "PropertiesId", "RecordsId" },
                values: new object[] { 3, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRecord_UsersId",
                table: "ApplicationUserRecord",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRecord_AspNetUsers_UsersId",
                table: "ApplicationUserRecord",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
