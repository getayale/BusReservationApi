using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusReservationApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPassengerLastUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Last Updated",
                table: "passengers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Last Updated",
                table: "passengers");
        }
    }
}
