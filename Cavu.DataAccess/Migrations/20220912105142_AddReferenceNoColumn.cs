using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cavu.DataAccess.Migrations
{
    public partial class AddReferenceNoColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceNo",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceNo",
                table: "Reservations");
        }
    }
}
