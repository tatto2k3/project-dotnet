using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueStarMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddSeatTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "SEAT",
    columns: table => new
    {
        SeatId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
        SeatType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
        FlightId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
        IsBooked = table.Column<bool>(nullable: true),
    },
    constraints: table =>
    {
        table.PrimaryKey("PK__SEAT__79B89923D3A743A3", x => x.SeatId);
    });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
