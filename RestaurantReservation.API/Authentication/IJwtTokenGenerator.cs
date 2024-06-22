using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Authentication;

public interface IJwtTokenGenerator
{
    public string GenerateToken(Customer customer);
}