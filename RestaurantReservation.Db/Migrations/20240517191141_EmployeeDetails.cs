using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE VIEW EmployeeDetails AS
                  SELECT 
                      e.FirstName + ' ' + e.LastName AS EmployeeName,
                      e.Position,
                      r.Name AS RestaurantName,
                      r.Address AS RestaurantAddress
                  FROM Employees e
                  INNER JOIN 
                      Restaurants r ON e.RestaurantID = r.RestaurantID;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DROP VIEW EmployeeDetails");
        }
    }
}
