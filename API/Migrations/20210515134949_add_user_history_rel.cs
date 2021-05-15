using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_user_history_rel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "HistoryItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_HistoryItems_UserId",
                table: "HistoryItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryItems_AspNetUsers_UserId",
                table: "HistoryItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryItems_AspNetUsers_UserId",
                table: "HistoryItems");

            migrationBuilder.DropIndex(
                name: "IX_HistoryItems_UserId",
                table: "HistoryItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HistoryItems");

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
    }
}
