using Microsoft.EntityFrameworkCore.Migrations;

namespace MPED.Infrastructure.Migrations
{
    public partial class BookingRoomEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "BookingRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "BookingRooms");
        }
    }
}
