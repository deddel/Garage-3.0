using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garage3._0.Migrations
{
    /// <inheritdoc />
    public partial class SeededParkingSpotNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
