using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garage3._0.Migrations
{
    /// <inheritdoc />
    public partial class SeedParkingSpot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpot_ParkedVehicle_RegNumber",
                table: "ParkingSpot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingSpot",
                table: "ParkingSpot");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpot_RegNumber",
                table: "ParkingSpot");

            migrationBuilder.RenameTable(
                name: "ParkingSpot",
                newName: "ParkingSpots");

            migrationBuilder.RenameColumn(
                name: "Spot",
                table: "ParkingSpots",
                newName: "SpotId");

            migrationBuilder.AlterColumn<string>(
                name: "RegNumber",
                table: "ParkingSpots",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "ParkingSpots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingSpots",
                table: "ParkingSpots",
                column: "SpotId");

            migrationBuilder.InsertData(
                table: "ParkingSpots",
                columns: new[] { "SpotId", "IsAvailable", "RegNumber" },
                values: new object[,]
                {
                    { 1, true, null },
                    { 2, true, null },
                    { 3, true, null },
                    { 4, true, null },
                    { 5, true, null },
                    { 6, true, null },
                    { 7, true, null },
                    { 8, true, null },
                    { 9, true, null },
                    { 10, true, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_RegNumber",
                table: "ParkingSpots",
                column: "RegNumber",
                unique: true,
                filter: "[RegNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_ParkedVehicle_RegNumber",
                table: "ParkingSpots",
                column: "RegNumber",
                principalTable: "ParkedVehicle",
                principalColumn: "RegistrationNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_ParkedVehicle_RegNumber",
                table: "ParkingSpots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingSpots",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpots_RegNumber",
                table: "ParkingSpots");

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "SpotId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "SpotId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "SpotId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "SpotId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "SpotId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "SpotId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "SpotId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "SpotId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "SpotId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "SpotId",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "ParkingSpots");

            migrationBuilder.RenameTable(
                name: "ParkingSpots",
                newName: "ParkingSpot");

            migrationBuilder.RenameColumn(
                name: "SpotId",
                table: "ParkingSpot",
                newName: "Spot");

            migrationBuilder.AlterColumn<string>(
                name: "RegNumber",
                table: "ParkingSpot",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingSpot",
                table: "ParkingSpot",
                column: "Spot");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpot_RegNumber",
                table: "ParkingSpot",
                column: "RegNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpot_ParkedVehicle_RegNumber",
                table: "ParkingSpot",
                column: "RegNumber",
                principalTable: "ParkedVehicle",
                principalColumn: "RegistrationNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
