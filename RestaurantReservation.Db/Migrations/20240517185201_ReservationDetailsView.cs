using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class ReservationDetailsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE VIEW ReservationDetails
                  AS
                  SELECT r.ReservationID, 
                         c.FirstName + ' ' + c.LastName AS CustomerName,
                         rs.Name AS RestaurantName
                  FROM Reservations r
                  INNER JOIN 
                      Customers c ON r.CustomerID = c.CustomerID
                  INNER JOIN 
                      Restaurants rs ON rs.RestaurantID = r.RestaurantID;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DROP VIEW ReservationDetails");
        }
    }
}
