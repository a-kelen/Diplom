using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_blocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LibraryReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ComponentReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateTable(
                name: "ComponentBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentBlocks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentBlocks_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibraryBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LibraryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryBlocks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryBlocks_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBlocks_AspNetUsers_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBlocks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "90c25fcc-ba9d-4074-a5c0-90adc22b1b90", new DateTime(2021, 5, 4, 12, 55, 9, 756, DateTimeKind.Local).AddTicks(6139), "AQAAAAEAACcQAAAAEJ1dmXCRq5MTmRwu7OTSI1kQSd/ddAHaGBFiOeTNtcZC/02hT4hsnYI5TbGqvXqm9A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "7804a8f2-f1f3-4078-940c-274609dacce4", new DateTime(2021, 5, 4, 12, 55, 9, 761, DateTimeKind.Local).AddTicks(3022), "AQAAAAEAACcQAAAAEKujN3A371GwrP2wSggpMvhq+d8gpVZ0gBA9sSaULvqxuI2dICk/cjZQTeezU4SGHg==" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentBlocks_ComponentId",
                table: "ComponentBlocks",
                column: "ComponentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentBlocks_UserId",
                table: "ComponentBlocks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryBlocks_LibraryId",
                table: "LibraryBlocks",
                column: "LibraryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryBlocks_UserId",
                table: "LibraryBlocks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlocks_PersonId",
                table: "UserBlocks",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBlocks_UserId",
                table: "UserBlocks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ComponentBlocks");

            migrationBuilder.DropTable(
                name: "LibraryBlocks");

            migrationBuilder.DropTable(
                name: "UserBlocks");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserReports");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LibraryReports");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ComponentReports");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "7ebf3e86-58e9-4d26-a4ad-cc666a27dfd2", new DateTime(2021, 4, 29, 23, 7, 6, 68, DateTimeKind.Local).AddTicks(4365), "AQAAAAEAACcQAAAAEDCo9EHEna8xI8n59cDVeXqXsr+aJQ8dI09VfriDyTNXtI+eeLTMX0nnYIb8C9z8dA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "84ba957a-4992-4413-b8ae-ff70fea29be7", new DateTime(2021, 4, 29, 23, 7, 6, 73, DateTimeKind.Local).AddTicks(2826), "AQAAAAEAACcQAAAAEItvdgiuyvfhWNUcfL6hL2HlABV9/vqVFYrvSGwGhs29fMs5zlvKxM+q4UQyAes6vQ==" });
        }
    }
}
