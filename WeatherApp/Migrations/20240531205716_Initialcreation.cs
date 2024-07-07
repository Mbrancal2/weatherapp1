using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Migrations
{
    /// <inheritdoc />
    public partial class Initialcreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Lon = table.Column<double>(type: "REAL", nullable: false),
                    Lat = table.Column<double>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => new { x.Lon, x.Lat });
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Dt = table.Column<int>(type: "INTEGER", nullable: false),
                    Lon = table.Column<double>(type: "REAL", nullable: false),
                    Lat = table.Column<double>(type: "REAL", nullable: false),
                    Condition = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Temp = table.Column<double>(type: "REAL", nullable: false),
                    Temp_min = table.Column<double>(type: "REAL", nullable: false),
                    Temp_max = table.Column<double>(type: "REAL", nullable: false),
                    Timezone = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => new { x.Lon, x.Lat, x.Dt });
                    table.ForeignKey(
                        name: "FK_Weather_City_Lon_Lat",
                        columns: x => new { x.Lon, x.Lat },
                        principalTable: "City",
                        principalColumns: new[] { "Lon", "Lat" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weather_Dt_Lon_Lat",
                table: "Weather",
                columns: new[] { "Dt", "Lon", "Lat" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
