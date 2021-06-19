using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fix_db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "AspNetUserClaims",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "AspNetRoleClaims",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "a619c99a-5248-468f-98ed-e97219fc1794", new DateTime(2021, 6, 9, 23, 32, 49, 116, DateTimeKind.Local).AddTicks(6345), "AQAAAAEAACcQAAAAEMgvePh1vlUB+TEmQcBzkv5xZfqYIc80/qx4in5afnZdYFLjDbHwyYSzSIx/ENqpfw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "976e4e4c-fe98-4a9f-a740-adfafa66c618", new DateTime(2021, 6, 9, 23, 32, 49, 119, DateTimeKind.Local).AddTicks(9506), "AQAAAAEAACcQAAAAEOkZzKh0E/jJqmZGqd4MT/84YgQOhDh3FkrnUqPu7l1BOY4xnkhtT9iGki07cPu87w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "AspNetUserClaims",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "AspNetRoleClaims",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "77604de6-d05a-41fb-98dc-7801c58b5b8d", new DateTime(2021, 6, 7, 23, 25, 41, 815, DateTimeKind.Local).AddTicks(678), "AQAAAAEAACcQAAAAECly6Pob0jk+j66fTQf9zzfO7d2Klpf1qfeAVw9kLzWxWm9AiVQink1C63bJlqXOgw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "c5dfae14-9b5a-4b99-bcb7-e1082c1cc049", new DateTime(2021, 6, 7, 23, 25, 41, 818, DateTimeKind.Local).AddTicks(931), "AQAAAAEAACcQAAAAEJFFEo/x1q0PriZqq1dP4pHWrO88EQJDhHuZ0/XZ37V4mL5uhs1LBkvBQJb8pNmF2Q==" });
        }
    }
}
