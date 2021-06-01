using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_top_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopComponents_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopLibraries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LibraryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopLibraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopLibraries_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopUsers_AspNetUsers_UserId",
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
                values: new object[] { "f96f10f1-89dd-4071-8830-8c306c5d0e54", new DateTime(2021, 5, 30, 20, 43, 47, 998, DateTimeKind.Local).AddTicks(4858), "AQAAAAEAACcQAAAAENbYTGMslp2KVFnl681wVhX39HmCkPHbyfDFA2Zl9yd8zxGcsw7GSEgSMM5Tf/ZkVw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "ffc4a280-edd3-4d06-88fa-82d6f2c3e8eb", new DateTime(2021, 5, 30, 20, 43, 48, 1, DateTimeKind.Local).AddTicks(6822), "AQAAAAEAACcQAAAAEFQdUqKsw1ihMSIVIPQHKZr3YnII4edZMHS93yMVT9mp3u4zaRMGiF14bJwOZjPopA==" });

            migrationBuilder.CreateIndex(
                name: "IX_TopComponents_ComponentId",
                table: "TopComponents",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_TopLibraries_LibraryId",
                table: "TopLibraries",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_TopUsers_UserId",
                table: "TopUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopComponents");

            migrationBuilder.DropTable(
                name: "TopLibraries");

            migrationBuilder.DropTable(
                name: "TopUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "0167c39e-0c68-4bb0-89d9-427648e8d56a", new DateTime(2021, 5, 15, 16, 49, 48, 347, DateTimeKind.Local).AddTicks(3673), "AQAAAAEAACcQAAAAEOPU/SbalkbedgVDmBV/zzcxf6+b1NbigQF8KQz1JLoFmkoHTJCrKH3LaHAKLpxrrw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "d96ac49b-7f7c-4397-afbc-b688a60d616c", new DateTime(2021, 5, 15, 16, 49, 48, 352, DateTimeKind.Local).AddTicks(1811), "AQAAAAEAACcQAAAAEKw8ns1aKSvMFs+PZo7Uin2+ISdNHRP2YX9eoIZzqGOycSwy/F1MEt7URPWvcxIDQQ==" });
        }
    }
}
