using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusReservationApi.Migrations
{
    /// <inheritdoc />
    public partial class FixShadowPropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Last Updated",
                table: "passengers",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "Last Updated",
                table: "bookings",
                newName: "LastUpdated");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "passengers",
                newName: "Last Updated");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "bookings",
                newName: "Last Updated");
        }
    }
}
