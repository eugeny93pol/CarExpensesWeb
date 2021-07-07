using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.Repository.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("18066d70-ba42-4ff9-867e-276e1733caeb"));

            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("27ee86c1-be47-42e8-a4bf-3eb7f7b23706"));

            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("525f0f19-6bc8-4ec7-ba74-5d02064e391f"));

            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("6315e3b4-1523-4067-af44-be660ec22e8b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("52cd1068-30b5-42fc-b4e9-15ae79748935"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("81d58e71-ad12-48af-87fc-7130801ff1ff"));

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Actions");

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckPoint",
                table: "Actions",
                type: "bit",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("1da55e32-e39e-4f9d-ae1c-8632bed2ac83"));

            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("3f210540-457a-4b84-8e0f-ea741dbba8f6"));

            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("4d8fc6e5-25d6-45ff-b150-eb12ecbf1b2b"));

            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("6b762860-55a7-4ac2-8c9b-b0bf87dc49a6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2dc15676-469f-4ed0-aa35-b6f8251225f5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7a402ca5-4a05-45ec-8085-283eb675607a"));

            migrationBuilder.DropColumn(
                name: "IsCheckPoint",
                table: "Actions");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Actions",
                type: "money",
                nullable: true);

            migrationBuilder.InsertData(
                table: "CarActionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("18066d70-ba42-4ff9-867e-276e1733caeb"), "mileage" },
                    { new Guid("6315e3b4-1523-4067-af44-be660ec22e8b"), "purchases" },
                    { new Guid("27ee86c1-be47-42e8-a4bf-3eb7f7b23706"), "refill" },
                    { new Guid("525f0f19-6bc8-4ec7-ba74-5d02064e391f"), "repair" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("52cd1068-30b5-42fc-b4e9-15ae79748935"), "admin" },
                    { new Guid("81d58e71-ad12-48af-87fc-7130801ff1ff"), "user" }
                });
        }
    }
}
