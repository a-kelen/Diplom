using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_history_item : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryItems", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "da031094-d9b8-4bec-93a6-f0dc741086d7", new DateTime(2021, 5, 15, 12, 0, 14, 534, DateTimeKind.Local).AddTicks(7079), "AQAAAAEAACcQAAAAELSSjrnIYw71iwYGi9zNENepckwregqcM1K6H9qjbpPow+0Vmocg29FvD7Yh7zUUYQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "460fde52-9628-4ffe-9b8e-d39937bd7b9b", new DateTime(2021, 5, 15, 12, 0, 14, 539, DateTimeKind.Local).AddTicks(2323), "AQAAAAEAACcQAAAAEBHVazvAcEwXRvwzApPks35arg0A1doocLfgrtpskUOmSltrVKFEUK6MU8YQRjEiHw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "02d60581-a60d-4f06-9b68-fdb39e6a5f92", new DateTime(2021, 5, 9, 17, 27, 34, 783, DateTimeKind.Local).AddTicks(2650), "AQAAAAEAACcQAAAAEJgnqMMdfPQaLlqN4CyQTG8yNkncoN+f2HptxiZeUtEOctgfSDb25W8buLH1DzV48Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "da97ee0b-bf3a-48b9-8f4a-ca22ae229a2b", new DateTime(2021, 5, 9, 17, 27, 34, 786, DateTimeKind.Local).AddTicks(7002), "AQAAAAEAACcQAAAAEJsDOFrbgnO4CP6SQiNIHZykRP+HSC7u+UflC3leDmR5TrYIAnQh+LhT1hhdbSdA8A==" });
        }
    }
}
