using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Report_Created_label : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "UserReports",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "LibraryReports",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "ComponentReports",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "3525d797-dcb0-4f1f-9c7b-1f4f795145da", new DateTime(2021, 4, 8, 14, 12, 20, 415, DateTimeKind.Local).AddTicks(166), "AQAAAAEAACcQAAAAEEC9MDpDa0gAD6JB81OOmWex6wLWXMTCkvvFuGH+dMpDAnXQ16xMF+js8uM04lRPUw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "08026c82-3546-40e6-a9fd-ef5829f5b436", new DateTime(2021, 4, 8, 14, 12, 20, 417, DateTimeKind.Local).AddTicks(9919), "AQAAAAEAACcQAAAAEIEe0gD5zyCiGOZA0ehaF5ypCik5hfodVkiji15xUWHXdQh0rZDymsI+aL127ha5pQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "UserReports",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "LibraryReports",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "ComponentReports",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2429acd-e887-47f8-8ad2-6502e05c9068"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "c8be0df9-85a9-40f7-a74f-cd9215051b71", new DateTime(2021, 4, 3, 12, 50, 25, 994, DateTimeKind.Local).AddTicks(6883), "AQAAAAEAACcQAAAAEAhn2e/oR7W7p80Ad3Xjr5LnEEH0yBc9w0AtYmejpMujrCi3xnyk2aTt9JFbwU1WyA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d350afff-86b3-449b-be6c-e87394d5a629"),
                columns: new[] { "ConcurrencyStamp", "Created", "PasswordHash" },
                values: new object[] { "6daea8c6-c575-4eba-9598-ce0c444eca7a", new DateTime(2021, 4, 3, 12, 50, 25, 998, DateTimeKind.Local).AddTicks(3318), "AQAAAAEAACcQAAAAEDpj/7sc2ISCJzlZSl/bxXYj+aJ/9YlpVWlEUk4lyeZsfgDtfACioz1ix6IoGFFXUg==" });
        }
    }
}
