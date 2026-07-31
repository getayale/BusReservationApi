using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusReservationApi.Migrations
{
    /// <inheritdoc />
    public partial class MoveToFluentConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_busRoutes_BusRouteId",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_passengers_PassengerId",
                table: "bookings");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "busRoutes",
                newName: "MaxCapacity");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "passengers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PassengerCode",
                table: "passengers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "passengers",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "passengers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "RouteCode",
                table: "busRoutes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "busRoutes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Departure",
                table: "busRoutes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SeatNumber",
                table: "bookings",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_passengers_PassengerCode",
                table: "passengers",
                column: "PassengerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_busRoutes_RouteCode",
                table: "busRoutes",
                column: "RouteCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_busRoutes_BusRouteId",
                table: "bookings",
                column: "BusRouteId",
                principalTable: "busRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_passengers_PassengerId",
                table: "bookings",
                column: "PassengerId",
                principalTable: "passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_busRoutes_BusRouteId",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_passengers_PassengerId",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_passengers_PassengerCode",
                table: "passengers");

            migrationBuilder.DropIndex(
                name: "IX_busRoutes_RouteCode",
                table: "busRoutes");

            migrationBuilder.RenameColumn(
                name: "MaxCapacity",
                table: "busRoutes",
                newName: "Capacity");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "passengers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PassengerCode",
                table: "passengers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "passengers",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "passengers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RouteCode",
                table: "busRoutes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "busRoutes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Departure",
                table: "busRoutes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "SeatNumber",
                table: "bookings",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_busRoutes_BusRouteId",
                table: "bookings",
                column: "BusRouteId",
                principalTable: "busRoutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_passengers_PassengerId",
                table: "bookings",
                column: "PassengerId",
                principalTable: "passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
