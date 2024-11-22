using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3._0.Migrations
{
    /// <inheritdoc />
    public partial class UpdateParkingSpot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_ParkedVehicle_RegNumber",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpots_RegNumber",
                table: "ParkingSpots");

            migrationBuilder.RenameColumn(
                name: "RegNumber",
                table: "ParkingSpots",
                newName: "ParkedVehicleRegistrationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_ParkedVehicleRegistrationNumber",
                table: "ParkingSpots",
                column: "ParkedVehicleRegistrationNumber",
                unique: true,
                filter: "[ParkedVehicleRegistrationNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_ParkedVehicle_ParkedVehicleRegistrationNumber",
                table: "ParkingSpots",
                column: "ParkedVehicleRegistrationNumber",
                principalTable: "ParkedVehicle",
                principalColumn: "RegistrationNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_ParkedVehicle_ParkedVehicleRegistrationNumber",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpots_ParkedVehicleRegistrationNumber",
                table: "ParkingSpots");

            migrationBuilder.RenameColumn(
                name: "ParkedVehicleRegistrationNumber",
                table: "ParkingSpots",
                newName: "RegNumber");

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
    }
}
