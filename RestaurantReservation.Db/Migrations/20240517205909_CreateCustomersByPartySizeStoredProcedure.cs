using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class CreateCustomersByPartySizeStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE GetCustomersByPartySize
                       @PartySize INT
                  AS
                  BEGIN
                      SELECT c.*
                      FROM Customers c
                      INNER JOIN Reservations r
                          ON c.CustomerID = r.CustomerID
                      WHERE r.PartySize > @PartySize;
                  END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DROP PROCEDURE GetCustomersByPartySize");
        }
    }
}
