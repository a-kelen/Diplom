using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
