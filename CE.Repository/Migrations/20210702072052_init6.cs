using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.Repository.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SparePart_Actions_CarActionRepairId",
                table: "SparePart");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("25d03aa4-7d30-45e5-aa5a-39b923130fc4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("92e25f10-d42f-4119-8941-4ea6a8f6f559"));

            migrationBuilder.DropColumn(
                name: "CarActionId",
                table: "SparePart");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarActionRepairId",
                table: "SparePart",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SparePart_Actions_CarActionRepairId",
                table: "SparePart");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("15921c3b-1321-4836-9728-9c9a73e2224f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ce540ef4-34b9-44b0-a76f-dec77c806784"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CarActionRepairId",
                table: "SparePart",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CarActionId",
                table: "SparePart",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("92e25f10-d42f-4119-8941-4ea6a8f6f559"), "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("25d03aa4-7d30-45e5-aa5a-39b923130fc4"), "user" });

            migrationBuilder.AddForeignKey(
                name: "FK_SparePart_Actions_CarActionRepairId",
                table: "SparePart",
                column: "CarActionRepairId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
