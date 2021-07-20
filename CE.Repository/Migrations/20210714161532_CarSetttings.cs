using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.Repository.Migrations
{
    public partial class CarSetttings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SparePart_Actions_CarActionRepairId",
                table: "SparePart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SparePart",
                table: "SparePart");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("15921c3b-1321-4836-9728-9c9a73e2224f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ce540ef4-34b9-44b0-a76f-dec77c806784"));

            migrationBuilder.DropColumn(
                name: "MeasurementSystem",
                table: "CarsSettings");

            migrationBuilder.RenameTable(
                name: "SparePart",
                newName: "SpareParts");

            migrationBuilder.RenameIndex(
                name: "IX_SparePart_CarActionRepairId",
                table: "SpareParts",
                newName: "IX_SpareParts_CarActionRepairId");

            migrationBuilder.AddColumn<int>(
                name: "DistanceUnit",
                table: "CarsSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuelConsumptionType",
                table: "CarsSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuelVolumeUnit",
                table: "CarsSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpareParts",
                table: "SpareParts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("97c010de-5e25-482c-8689-170cedc4b048"), "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a5426fbb-c218-4c86-b522-e91fecdb2508"), "user" });

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_Actions_CarActionRepairId",
                table: "SpareParts",
                column: "CarActionRepairId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_Actions_CarActionRepairId",
                table: "SpareParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpareParts",
                table: "SpareParts");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("97c010de-5e25-482c-8689-170cedc4b048"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a5426fbb-c218-4c86-b522-e91fecdb2508"));

            migrationBuilder.DropColumn(
                name: "DistanceUnit",
                table: "CarsSettings");

            migrationBuilder.DropColumn(
                name: "FuelConsumptionType",
                table: "CarsSettings");

            migrationBuilder.DropColumn(
                name: "FuelVolumeUnit",
                table: "CarsSettings");

            migrationBuilder.RenameTable(
                name: "SpareParts",
                newName: "SparePart");

            migrationBuilder.RenameIndex(
                name: "IX_SpareParts_CarActionRepairId",
                table: "SparePart",
                newName: "IX_SparePart_CarActionRepairId");

            migrationBuilder.AddColumn<string>(
                name: "MeasurementSystem",
                table: "CarsSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SparePart",
                table: "SparePart",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("15921c3b-1321-4836-9728-9c9a73e2224f"), "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("ce540ef4-34b9-44b0-a76f-dec77c806784"), "user" });

            migrationBuilder.AddForeignKey(
                name: "FK_SparePart_Actions_CarActionRepairId",
                table: "SparePart",
                column: "CarActionRepairId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
