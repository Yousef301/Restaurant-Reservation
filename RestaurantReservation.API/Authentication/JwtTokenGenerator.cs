using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Customer customer)
    {
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["SecretKey"]));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenClaims = new List<Claim>();
        tokenClaims.Add(new Claim("id", customer.CustomerID.ToString()));
        tokenClaims.Add(new Claim("email", customer.Email));

        var jwtToken = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: tokenClaims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return token;
    }
}