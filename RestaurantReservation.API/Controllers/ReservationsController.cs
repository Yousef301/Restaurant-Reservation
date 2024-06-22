using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Db.DTOs;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IMapper _mapper;

    public ReservationsController(IReservationsRepository reservationsRepository,
        IMapper mapper)
    {
        _reservationsRepository = reservationsRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = await _reservationsRepository.GetReservationsAsync();

        return Ok(_mapper.Map<IEnumerable<ReservationDto>>(reservations));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetReservation(int id)
    {
        var reservation = await _reservationsRepository.GetReservationAsync(id);

        if (reservation == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ReservationDto>(reservation));
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var reservation = _mapper.Map<Reservation>(reservationDto);

        var createdReservation = await _reservationsRepository.AddReservationAsync(reservation);

        return CreatedAtAction(nameof(GetReservation), new { id = reservation.ReservationID },
            _mapper.Map<ReservationDto>(createdReservation));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var reservation = await _reservationsRepository.GetReservationAsync(id);

        if (reservation == null)
        {
            return NotFound();
        }

        await _reservationsRepository.DeleteReservationAsync(reservation);

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateReservation(int id,
        JsonPatchDocument<ReservationDto> reservationPatchDocument)
    {
        var reservation = await _reservationsRepository.GetReservationAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }

        var reservationDto = _mapper.Map<ReservationDto>(reservation);

        reservationPatchDocument.ApplyTo(reservationDto, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!TryValidateModel(reservationDto))
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(reservationDto, reservation);

        var updatedReservation = await _reservationsRepository.UpdateReservationAsync(id, reservation);

        await _reservationsRepository.SaveChangesAsync();

        return Ok(updatedReservation);
    }
}