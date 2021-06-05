using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class remove_files_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "8931e99e-1f7e-45e8-81ec-cfa640a65e99", new DateTime(2021, 6, 5, 13, 58, 21, 580, DateTimeKind.Local).AddTicks(5368), "AQAAAAEAACcQAAAAEIVX7fbSrZN6u7lwE7WBUyffGnEQwrHSvG3Um0azI9+i+qMi2dRPRn0FRoCSwUMXXg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "69cec7a1-4316-4a3a-9639-58bdd7e80528", new DateTime(2021, 6, 5, 13, 58, 21, 592, DateTimeKind.Local).AddTicks(6097), "AQAAAAEAACcQAAAAEJpWgPn68zAk0HoaBopuFQT1GNVZ7m1EoNvvFnryliZUFxKdHwo7jYVplI5Nux/ftA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Files_ComponentId",
                table: "Files",
                column: "ComponentId");
        }
    }
}
