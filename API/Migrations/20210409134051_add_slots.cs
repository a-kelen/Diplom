using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_slots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slots_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ComponentId",
                table: "Slots",
                column: "ComponentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slots");

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
    }
}
