using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.Repository.Migrations
{
    public partial class ActionTypesDefaultCarId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VIN",
                table: "Cars",
                newName: "Vin");

            migrationBuilder.AddColumn<long>(
                name: "DefaultCarId",
                table: "UsersSettings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Actions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ActionTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTypes", x => x.Id);
                    table.UniqueConstraint("AK_ActionTypes_Name", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "ActionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "mileage" },
                    { 2L, "purchases" },
                    { 3L, "refill" },
                    { 4L, "repair" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_Type",
                table: "Actions",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_ActionTypes_Type",
                table: "Actions",
                column: "Type",
                principalTable: "ActionTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_ActionTypes_Type",
                table: "Actions");

            migrationBuilder.DropTable(
                name: "ActionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Actions_Type",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "DefaultCarId",
                table: "UsersSettings");

            migrationBuilder.RenameColumn(
                name: "Vin",
                table: "Cars",
                newName: "VIN");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
