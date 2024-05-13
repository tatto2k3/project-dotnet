using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueStarMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SEAT",
                columns: table => new
                {
                    SEAT_ID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SEAT_TYPE = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FLIGHT_ID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ISBOOKED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SEAT__79B89923D3A743A3", x => x.SEAT_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SEAT");
        }
    }
}
