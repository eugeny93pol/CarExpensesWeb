using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.Repository.Migrations
{
    public partial class AverageFuelConsumption1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("30420df2-8e2f-40dc-a6db-79ab77e13efd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dea3dc94-58ca-4009-a79b-b833404e1ce7"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("901e4af6-0b52-46d8-8a45-423e4bb57c34"), "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("d044de7a-5b08-4638-a09a-04ed5b1f2079"), "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("901e4af6-0b52-46d8-8a45-423e4bb57c34"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d044de7a-5b08-4638-a09a-04ed5b1f2079"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("30420df2-8e2f-40dc-a6db-79ab77e13efd"), "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("dea3dc94-58ca-4009-a79b-b833404e1ce7"), "user" });
        }
    }
}
