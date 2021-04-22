using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Add_Component_Dependencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dependencies",
                table: "Components",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dependencies",
                table: "Components");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "5fed7852-84c7-4410-8e69-9c01a0732003", new DateTime(2021, 4, 9, 16, 40, 50, 112, DateTimeKind.Local).AddTicks(4838), "AQAAAAEAACcQAAAAEJ6FeyADmdRf7lECA+dgZuW1ylpYBJ0+rLTfnBkwSAtmMKRWZuBZ9UO+972hO6MZEQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "3057c60b-6249-4f98-9a86-1ac1e5d70786", new DateTime(2021, 4, 9, 16, 40, 50, 120, DateTimeKind.Local).AddTicks(5433), "AQAAAAEAACcQAAAAEMDMWyu4E8CqpetVhahb48LJq+YYSGRHciwHXSRcubK8jDAOVhsz8Aycyxtu0W2aWw==" });
        }
    }
}
