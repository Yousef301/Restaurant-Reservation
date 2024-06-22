using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Db.DTOs;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/customers")]
[Authorize]
public class CustomerController : ControllerBase
{
    private readonly ICustomersRepository _customersRepository;
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IMapper _mapper;

    public CustomerController(ICustomersRepository customersRepository,
        IReservationsRepository reservationsRepository,
        IMapper mapper)
    {
        _customersRepository = customersRepository;
        _reservationsRepository = reservationsRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await _customersRepository.GetCustomersAsync();

        return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        var customer = await _customersRepository.GetCustomerAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customer = _mapper.Map<Customer>(customerDto);

        var createdCustomer = await _customersRepository.AddCustomerAsync(customer);

        return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, createdCustomer);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _customersRepository.GetCustomerAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        await _customersRepository.DeleteCustomerAsync(customer);

        return NoContent();
    }


    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateCustomer(int id,
        JsonPatchDocument<CustomerDto> customerPatchDocument)
    {
        var customer = await _customersRepository.GetCustomerAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        var customerDto = _mapper.Map<CustomerDto>(customer);

        customerPatchDocument.ApplyTo(customerDto, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!TryValidateModel(customerDto))
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(customerDto, customer);

        var updatedCustomer = await _customersRepository.UpdateCustomerAsync(id, customer);

        await _customersRepository.SaveChangesAsync();

        return Ok(updatedCustomer);
    }

    [HttpGet("{id:int}/reservations")]
    public async Task<IActionResult> GetCustomerReservations(int id)
    {
        var reservations = await _reservationsRepository.GetCustomerReservationsAsync(id);

        return Ok(_mapper.Map<IEnumerable<ReservationDto>>(reservations));
    }
}