using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_element_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Libraries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Components");

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
        }
    }
}
