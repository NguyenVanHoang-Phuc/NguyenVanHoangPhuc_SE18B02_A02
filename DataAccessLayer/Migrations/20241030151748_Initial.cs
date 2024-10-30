using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerFullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerBirthday = table.Column<DateOnly>(type: "date", nullable: true),
                    CustomerStatus = table.Column<byte>(type: "tinyint", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TypeNote = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomType", x => x.RoomTypeID);
                });

            migrationBuilder.CreateTable(
                name: "BookingReservation",
                columns: table => new
                {
                    BookingReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingReservation", x => x.BookingReservationID);
                    table.ForeignKey(
                        name: "FK_BookingReservation_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomInformation",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoomDetailDescription = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: true),
                    RoomMaxCapacity = table.Column<int>(type: "int", nullable: true),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false),
                    RoomStatus = table.Column<byte>(type: "tinyint", nullable: true),
                    RoomPricePerDay = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomInformation", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_RoomInformation_RoomType_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalTable: "RoomType",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetail",
                columns: table => new
                {
                    BookingReservationID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ActualPrice = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetail", x => new { x.BookingReservationID, x.RoomID });
                    table.ForeignKey(
                        name: "FK_BookingDetail_BookingReservation_BookingReservationID",
                        column: x => x.BookingReservationID,
                        principalTable: "BookingReservation",
                        principalColumn: "BookingReservationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetail_RoomInformation_RoomID",
                        column: x => x.RoomID,
                        principalTable: "RoomInformation",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerID", "CustomerBirthday", "CustomerFullName", "CustomerStatus", "EmailAddress", "Password", "Telephone" },
                values: new object[,]
                {
                    { 3, new DateOnly(1990, 2, 2), "William Shakespeare", (byte)1, "WilliamShakespeare@FUMiniHotel.org", "123@", "0903939393" },
                    { 5, new DateOnly(1991, 3, 3), "Elizabeth Taylor", (byte)1, "ElizabethTaylor@FUMiniHotel.org", "144@", "0903939377" },
                    { 8, new DateOnly(1992, 11, 10), "James Cameron", (byte)1, "JamesCameron@FUMiniHotel.org", "443@", "0903946582" },
                    { 9, new DateOnly(1991, 12, 5), "Charles Dickens", (byte)1, "CharlesDickens@FUMiniHotel.org", "563@", "0903955633" },
                    { 10, new DateOnly(1993, 12, 24), "George Orwell", (byte)1, "GeorgeOrwell@FUMiniHotel.org", "177@", "0913933493" }
                });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "RoomTypeID", "RoomTypeName", "TypeDescription", "TypeNote" },
                values: new object[,]
                {
                    { 1, "Standard room", "This is typically the most affordable option and provides basic amenities such as a bed, dresser, and TV.", "N/A" },
                    { 2, "Suite", "Suites usually offer more space and amenities than standard rooms, such as a separate living area, kitchenette, and multiple bathrooms.", "N/A" },
                    { 3, "Deluxe room", "Deluxe rooms offer additional features such as a balcony or sea view, upgraded bedding, and improved décor.", "N/A" },
                    { 4, "Executive room", "Executive rooms are designed for business travelers and offer perks such as free breakfast, evening drink, and high-speed internet.", "N/A" },
                    { 5, "Family Room", "A room specifically designed to accommodate families, often with multiple beds and additional space for children.", "N/A" },
                    { 6, "Connecting Room", "Two or more rooms with a connecting door, providing flexibility for larger groups or families traveling together.", "N/A" },
                    { 7, "Penthouse Suite", "An extravagant, top-floor suite with exceptional views and exclusive amenities, typically chosen for special occasions or VIP guests.", "N/A" },
                    { 8, "Bungalow", "A standalone cottage-style accommodation, providing privacy and a sense of seclusion often within a resort setting", "N/A" }
                });

            migrationBuilder.InsertData(
                table: "BookingReservation",
                columns: new[] { "BookingReservationID", "BookingDate", "BookingStatus", "CustomerID", "TotalPrice" },
                values: new object[,]
                {
                    { 1, new DateOnly(2023, 12, 20), (byte)1, 3, 378.00m },
                    { 2, new DateOnly(2023, 12, 21), (byte)1, 3, 1493.00m }
                });

            migrationBuilder.InsertData(
                table: "RoomInformation",
                columns: new[] { "RoomID", "RoomDetailDescription", "RoomMaxCapacity", "RoomNumber", "RoomPricePerDay", "RoomStatus", "RoomTypeID" },
                values: new object[,]
                {
                    { 1, "A basic room with essential amenities, suitable for individual travelers or couples.", 3, "2364", 149.00m, (byte)1, 1 },
                    { 2, "Deluxe rooms offer additional features such as a balcony or sea view, upgraded bedding, and improved décor.", 5, "3345", 299.00m, (byte)1, 3 },
                    { 3, "A luxurious and spacious room with separate living and sleeping areas, ideal for guests seeking extra comfort and space.", 4, "4432", 199.00m, (byte)1, 2 },
                    { 5, "Floor 3, Window in the North West", 5, "3342", 219.00m, (byte)1, 5 },
                    { 7, "Floor 4, main window in the South", 4, "4434", 179.00m, (byte)1, 1 }
                });

            migrationBuilder.InsertData(
                table: "BookingDetail",
                columns: new[] { "BookingReservationID", "RoomID", "ActualPrice", "EndDate", "StartDate" },
                values: new object[,]
                {
                    { 1, 3, 199.00m, new DateOnly(2024, 1, 2), new DateOnly(2024, 1, 1) },
                    { 1, 7, 179.00m, new DateOnly(2024, 1, 2), new DateOnly(2024, 1, 1) },
                    { 2, 3, 199.00m, new DateOnly(2024, 1, 6), new DateOnly(2024, 1, 5) },
                    { 2, 5, 219.00m, new DateOnly(2024, 1, 9), new DateOnly(2024, 1, 5) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_RoomID",
                table: "BookingDetail",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingReservation_CustomerID",
                table: "BookingReservation",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomInformation_RoomTypeID",
                table: "RoomInformation",
                column: "RoomTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetail");

            migrationBuilder.DropTable(
                name: "BookingReservation");

            migrationBuilder.DropTable(
                name: "RoomInformation");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "RoomType");
        }
    }
}
