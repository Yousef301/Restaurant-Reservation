using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Db.DTOs;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/reservations/{reservationId}/menu-items")]
public class MenuItemsController : ControllerBase
{
    private readonly IMenuItemsRepository _menuItemsRepository;
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IMapper _mapper;

    public MenuItemsController(IMenuItemsRepository menuItemsRepository,
        IReservationsRepository reservationsRepository,
        IMapper mapper)
    {
        _menuItemsRepository = menuItemsRepository;
        _reservationsRepository = reservationsRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenuItems(int reservationId)
    {
        var reservation = await _reservationsRepository.GetReservationAsync(reservationId);

        if (reservation == null)
        {
            return NotFound();
        }

        var menuItems = await _menuItemsRepository.ListOrderedMenuItemsAsync(reservationId);

        return Ok(_mapper.Map<IEnumerable<MenuItemDto>>(menuItems));
    }
}