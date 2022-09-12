using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cavu.DataAccess.Migrations
{
    public partial class AddColumnReservationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationStatus",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationStatus",
                table: "Reservations");
        }
    }
}
