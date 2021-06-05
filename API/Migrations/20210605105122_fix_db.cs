using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fix_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Libraries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "bc8dc993-40a2-43d5-b175-3562e7586b18", new DateTime(2021, 6, 5, 13, 51, 21, 186, DateTimeKind.Local).AddTicks(2835), "AQAAAAEAACcQAAAAEOsBw6pA0YL6Jahkex48gtDreOXOZRRTtrkqKG6nmiiMH+Wh8LhLeDpf8NPpJLLAXQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "a0a96a6a-8f7a-4fb4-8e95-729df3a44e59", new DateTime(2021, 6, 5, 13, 51, 21, 193, DateTimeKind.Local).AddTicks(8820), "AQAAAAEAACcQAAAAEDejZ36Br2VIYP95DfSxsRn81CYx7QTKKQQVkiQiimiph43SnSeL8ouDpHnWdMIK8Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Components");

            migrationBuilder.AddColumn<Guid>(
                name: "BlockId",
                table: "Libraries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlockId",
                table: "Components",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlockId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "f96f10f1-89dd-4071-8830-8c306c5d0e54", new DateTime(2021, 5, 30, 20, 43, 47, 998, DateTimeKind.Local).AddTicks(4858), "AQAAAAEAACcQAAAAENbYTGMslp2KVFnl681wVhX39HmCkPHbyfDFA2Zl9yd8zxGcsw7GSEgSMM5Tf/ZkVw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "ffc4a280-edd3-4d06-88fa-82d6f2c3e8eb", new DateTime(2021, 5, 30, 20, 43, 48, 1, DateTimeKind.Local).AddTicks(6822), "AQAAAAEAACcQAAAAEFQdUqKsw1ihMSIVIPQHKZr3YnII4edZMHS93yMVT9mp3u4zaRMGiF14bJwOZjPopA==" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
