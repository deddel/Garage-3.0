using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garage3._0.Migrations
{
    /// <inheritdoc />
    public partial class ParkingSpotParkedVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "ParkedVehicle",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ParkedVehicle_RegistrationNumber",
                table: "ParkedVehicle",
                column: "RegistrationNumber");

            migrationBuilder.CreateTable(
                name: "ParkingSpots",
                columns: table => new
                {
                    SpotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkedVehicleRegistrationNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpots", x => x.SpotId);
                    table.ForeignKey(
                        name: "FK_ParkingSpots_ParkedVehicle_ParkedVehicleRegistrationNumber",
                        column: x => x.ParkedVehicleRegistrationNumber,
                        principalTable: "ParkedVehicle",
                        principalColumn: "RegistrationNumber");
                });

            migrationBuilder.InsertData(
                table: "ParkingSpots",
                columns: new[] { "SpotId", "IsAvailable", "ParkedVehicleRegistrationNumber" },
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
                name: "IX_ParkedVehicle_RegistrationNumber",
                table: "ParkedVehicle",
                column: "RegistrationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_ParkedVehicleRegistrationNumber",
                table: "ParkingSpots",
                column: "ParkedVehicleRegistrationNumber",
                unique: true,
                filter: "[ParkedVehicleRegistrationNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpots");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ParkedVehicle_RegistrationNumber",
                table: "ParkedVehicle");

            migrationBuilder.DropIndex(
                name: "IX_ParkedVehicle_RegistrationNumber",
                table: "ParkedVehicle");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "ParkedVehicle",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
