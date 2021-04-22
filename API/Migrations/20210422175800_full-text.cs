using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fulltext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "38bb0560-38dd-4593-adbd-867f7599e02f", new DateTime(2021, 4, 22, 20, 57, 59, 211, DateTimeKind.Local).AddTicks(3569), "AQAAAAEAACcQAAAAEAOJE62DAwTj+6CmP6mfcEe8ySkkgx1NiEmR3UF18ykDIRs47wUTUoHpHhhavblpOg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "ec66a13c-d78e-4ee3-837c-214065828b94", new DateTime(2021, 4, 22, 20, 57, 59, 220, DateTimeKind.Local).AddTicks(429), "AQAAAAEAACcQAAAAEHXoK6fKi1yco5HfW/S+O6pD6shy9C0eGBUYd0yEzpL8NIPy1bmdXLY0hNJ+I5jEvQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "82440f1a-3644-410d-8e8c-ff907472cd89", new DateTime(2021, 4, 10, 9, 15, 33, 282, DateTimeKind.Local).AddTicks(9955), "AQAAAAEAACcQAAAAEMHcotrq8ia+TzQa5WRAg4C+OtPSkyC+hTc2HnTSCteGk3K2U5N0+q24BwA6uGMXrg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "f4c8ded8-4072-4410-8e95-3fbf625c92a6", new DateTime(2021, 4, 10, 9, 15, 33, 287, DateTimeKind.Local).AddTicks(7058), "AQAAAAEAACcQAAAAECShwV9XuC52gAJiRZ8u5/pbtpddxZARJc3+49VuQ/+U/qNw5lXZJPBMXpZrywkGjw==" });
        }
    }
}
