using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3._0.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserIDId",
                table: "ParkedVehicle",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialSecurityNr",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_ApplicationUserIDId",
                table: "ParkedVehicle",
                column: "ApplicationUserIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_AspNetUsers_ApplicationUserIDId",
                table: "ParkedVehicle",
                column: "ApplicationUserIDId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_AspNetUsers_ApplicationUserIDId",
                table: "ParkedVehicle");

            migrationBuilder.DropIndex(
                name: "IX_ParkedVehicle_ApplicationUserIDId",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "ApplicationUserIDId",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SocialSecurityNr",
                table: "AspNetUsers");
        }
    }
}
