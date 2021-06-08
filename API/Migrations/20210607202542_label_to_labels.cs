using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class label_to_labels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentLabels_Label_LabelsId",
                table: "ComponentLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryLabels_Label_LabelsId",
                table: "LibraryLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Label",
                table: "Label");

            migrationBuilder.RenameTable(
                name: "Label",
                newName: "Labels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labels",
                table: "Labels",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentLabels_Labels_LabelsId",
                table: "ComponentLabels",
                column: "LabelsId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryLabels_Labels_LabelsId",
                table: "LibraryLabels",
                column: "LabelsId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentLabels_Labels_LabelsId",
                table: "ComponentLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryLabels_Labels_LabelsId",
                table: "LibraryLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labels",
                table: "Labels");

            migrationBuilder.RenameTable(
                name: "Labels",
                newName: "Label");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Label",
                table: "Label",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentLabels_Label_LabelsId",
                table: "ComponentLabels",
                column: "LabelsId",
                principalTable: "Label",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryLabels_Label_LabelsId",
                table: "LibraryLabels",
                column: "LabelsId",
                principalTable: "Label",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
