using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.Repository.Migrations
{
    public partial class AverageFuelConsumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("97c010de-5e25-482c-8689-170cedc4b048"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a5426fbb-c218-4c86-b522-e91fecdb2508"));

            migrationBuilder.AddColumn<decimal>(
                name: "AverageFuelConsumption",
                table: "Actions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("30420df2-8e2f-40dc-a6db-79ab77e13efd"), "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("dea3dc94-58ca-4009-a79b-b833404e1ce7"), "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("30420df2-8e2f-40dc-a6db-79ab77e13efd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dea3dc94-58ca-4009-a79b-b833404e1ce7"));

            migrationBuilder.DropColumn(
                name: "AverageFuelConsumption",
                table: "Actions");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("97c010de-5e25-482c-8689-170cedc4b048"), "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a5426fbb-c218-4c86-b522-e91fecdb2508"), "user" });
        }
    }
}
