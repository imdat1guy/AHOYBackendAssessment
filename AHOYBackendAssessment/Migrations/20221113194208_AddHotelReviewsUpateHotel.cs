using Microsoft.EntityFrameworkCore.Migrations;

namespace AHOYBackendAssessment.Migrations
{
    public partial class AddHotelReviewsUpateHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Hotels",
                newName: "HotelID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Bookings",
                newName: "BookingID");

            migrationBuilder.CreateTable(
                name: "HotelReviews",
                columns: table => new
                {
                    HotelReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelReviews", x => x.HotelReviewID);
                    table.ForeignKey(
                        name: "FK_HotelReviews_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelReviews_HotelID",
                table: "HotelReviews",
                column: "HotelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelReviews");

            migrationBuilder.RenameColumn(
                name: "HotelID",
                table: "Hotels",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "Bookings",
                newName: "ID");
        }
    }
}
