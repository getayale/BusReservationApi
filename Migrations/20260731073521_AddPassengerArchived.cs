using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusReservationApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPassengerArchived : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "passengers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "passengers");
        }
    }
}
