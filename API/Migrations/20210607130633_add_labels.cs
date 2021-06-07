using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_labels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentLabels",
                columns: table => new
                {
                    ComponentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabelsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentLabels", x => new { x.ComponentsId, x.LabelsId });
                    table.ForeignKey(
                        name: "FK_ComponentLabels_Components_ComponentsId",
                        column: x => x.ComponentsId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentLabels_Label_LabelsId",
                        column: x => x.LabelsId,
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibraryLabels",
                columns: table => new
                {
                    LabelsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LibrariesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryLabels", x => new { x.LabelsId, x.LibrariesId });
                    table.ForeignKey(
                        name: "FK_LibraryLabels_Label_LabelsId",
                        column: x => x.LabelsId,
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryLabels_Libraries_LibrariesId",
                        column: x => x.LibrariesId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "abd61154-589a-40a9-a40b-b6d2f8d3dc1c", new DateTime(2021, 6, 7, 16, 6, 32, 553, DateTimeKind.Local).AddTicks(7253), "AQAAAAEAACcQAAAAELckpn+m+CwdSBjHYqNCrnR6kQT9QbNW3JyU76hIZysbjE9NccPulUkQ6O7LFRX5Jw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "556105a2-5ecd-468a-8d70-5dfd48ad010d", new DateTime(2021, 6, 7, 16, 6, 32, 557, DateTimeKind.Local).AddTicks(6027), "AQAAAAEAACcQAAAAEMZi5L9EjZzB+LKMDVqlkaMHTCduzdUd6p/tSVpVPymi4ktC4b/4O9krWalIjVVVug==" });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentLabels_LabelsId",
                table: "ComponentLabels",
                column: "LabelsId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryLabels_LibrariesId",
                table: "LibraryLabels",
                column: "LibrariesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentLabels");

            migrationBuilder.DropTable(
                name: "LibraryLabels");

            migrationBuilder.DropTable(
                name: "Label");

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
    }
}
