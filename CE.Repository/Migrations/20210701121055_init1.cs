using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.Repository.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("1d277e04-415b-4f34-83b8-0de33fd8a836"));

            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("3157e000-3673-4da5-97c4-a2f1b838991e"));

            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("4795cc2c-eaea-4c14-a8ac-c420f5c52059"));

            migrationBuilder.DeleteData(
                table: "CarActionTypes",
                keyColumn: "Id",
                keyValue: new Guid("9065f0b4-4324-456e-9e80-321e1b069940"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("836188c0-4a07-42d8-b724-96924477680a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a2eb62cd-259f-46e0-94a9-8e419c550f55"));

            migrationBuilder.AddColumn<decimal>(
                name: "CarActionRepair_Total",
                table: "Actions",
                type: "money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostOfWork",
                table: "Actions",
                type: "money",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FuelType",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Actions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Actions",
                type: "money",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SparePart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: true),
                    Quantity = table.Column<byte>(type: "tinyint", nullable: false),
                    LimitByMileage = table.Column<long>(type: "bigint", nullable: true),
                    LimitByTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarActionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarActionRepairId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SparePart_Actions_CarActionRepairId",
                        column: x => x.CarActionRepairId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_SparePart_CarActionRepairId",
                table: "SparePart",
                column: "CarActionRepairId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SparePart");

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
                name: "CarActionRepair_Total",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "CostOfWork",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Actions");

            migrationBuilder.InsertData(
                table: "CarActionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4795cc2c-eaea-4c14-a8ac-c420f5c52059"), "mileage" },
                    { new Guid("9065f0b4-4324-456e-9e80-321e1b069940"), "purchases" },
                    { new Guid("1d277e04-415b-4f34-83b8-0de33fd8a836"), "refill" },
                    { new Guid("3157e000-3673-4da5-97c4-a2f1b838991e"), "repair" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a2eb62cd-259f-46e0-94a9-8e419c550f55"), "admin" },
                    { new Guid("836188c0-4a07-42d8-b724-96924477680a"), "user" }
                });
        }
    }
}
