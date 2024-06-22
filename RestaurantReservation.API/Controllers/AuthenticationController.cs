using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Authentication;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/authenticate")]
public class AuthenticationController : ControllerBase
{
    private readonly ICustomersRepository _customersRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationController(ICustomersRepository customersRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _customersRepository = customersRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    
    /// <summary>
    ///     
    /// </summary>
    /// <param name="userCredentials">The email of the customer</param>
    /// <returns>Authentication token</returns>
    [HttpPost]
    public async Task<IActionResult> AuthenticateUser(UserCredentials userCredentials)
    {
        var customer =
            await _customersRepository.GetCustomerByEmailAsync(userCredentials.Email);

        if (customer == null)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        var token = _jwtTokenGenerator.GenerateToken(customer);

        return Ok(new { Token = token });
    }
}