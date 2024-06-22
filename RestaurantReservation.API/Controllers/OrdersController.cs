using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Db.DTOs;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/reservations/{reservationId}/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IReservationsRepository _reservationRepository;
    private readonly IMapper _mapper;

    public OrdersController(IOrdersRepository ordersRepository,
        IReservationsRepository reservationRepository,
        IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(int reservationId)
    {
        var reservation = await _reservationRepository.GetReservationAsync(reservationId);

        if (reservation == null)
        {
            return NotFound();
        }

        var orders = await _ordersRepository.GetOrdersAsync(reservationId);

        return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
    }
}