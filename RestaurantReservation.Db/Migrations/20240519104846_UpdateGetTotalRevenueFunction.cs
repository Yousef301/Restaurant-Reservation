using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGetTotalRevenueFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE FUNCTION GetTotalRevenueByRestaurantID (@RestaurantID INT)
                  RETURNS DECIMAL(18, 2)
                  AS
                  BEGIN
                      DECLARE @TotalRevenue DECIMAL(18, 2);
                      SELECT 
                          @TotalRevenue = ISNULL(SUM(o.TotalAmount), 0)
                      FROM 
                          Reservations r
                      INNER JOIN 
                          Restaurants rs ON r.RestaurantID = rs.RestaurantID
                      INNER JOIN 
                          Orders o ON o.ReservationID = r.ReservationID
                      WHERE 
                          rs.RestaurantID = @RestaurantID;
                      RETURN @TotalRevenue;
                  END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DROP FUNCTION GetTotalRevenueByRestaurantID");
        }
    }
}
