using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.Repository.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_CarActionTypes_Type",
                table: "Actions");

            migrationBuilder.DropTable(
                name: "CarActionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Actions_Type",
                table: "Actions");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2dc15676-469f-4ed0-aa35-b6f8251225f5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7a402ca5-4a05-45ec-8085-283eb675607a"));

            migrationBuilder.AddColumn<long>(
                name: "Mileage",
                table: "Cars",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<long>(
                name: "Mileage",
                table: "Actions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("92e25f10-d42f-4119-8941-4ea6a8f6f559"), "admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("25d03aa4-7d30-45e5-aa5a-39b923130fc4"), "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("25d03aa4-7d30-45e5-aa5a-39b923130fc4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("92e25f10-d42f-4119-8941-4ea6a8f6f559"));

            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Actions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Mileage",
                table: "Actions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "CarActionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarActionTypes", x => x.Id);
                    table.UniqueConstraint("AK_CarActionTypes_Name", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "CarActionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6b762860-55a7-4ac2-8c9b-b0bf87dc49a6"), "Mileage" },
                    { new Guid("4d8fc6e5-25d6-45ff-b150-eb12ecbf1b2b"), "purchases" },
                    { new Guid("3f210540-457a-4b84-8e0f-ea741dbba8f6"), "Refill" },
                    { new Guid("1da55e32-e39e-4f9d-ae1c-8632bed2ac83"), "Repair" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2dc15676-469f-4ed0-aa35-b6f8251225f5"), "admin" },
                    { new Guid("7a402ca5-4a05-45ec-8085-283eb675607a"), "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_Type",
                table: "Actions",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_CarActionTypes_Type",
                table: "Actions",
                column: "Type",
                principalTable: "CarActionTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
