using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Db.DTOs;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeesRepository employeesRepository,
        IMapper mapper)
    {
        _employeesRepository = employeesRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _employeesRepository.GetEmployeesAsync();

        return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await _employeesRepository.GetEmployeeAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<EmployeeDto>(employee));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var employee = _mapper.Map<Employee>(employeeDto);

        var createdEmployee = await _employeesRepository.AddEmployeeAsync(employee);

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeID },
            _mapper.Map<EmployeeDto>(createdEmployee));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _employeesRepository.GetEmployeeAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        await _employeesRepository.DeleteEmployeeAsync(employee);

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateEmployee(int id,
        JsonPatchDocument<EmployeeDto> employeePatchDocument)
    {
        var employee = await _employeesRepository.GetEmployeeAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        employeePatchDocument.ApplyTo(employeeDto, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!TryValidateModel(employeeDto))
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(employeeDto, employee);

        var updatedEmployee = await _employeesRepository.UpdateEmployeeAsync(id, employee);

        await _employeesRepository.SaveChangesAsync();

        return Ok(updatedEmployee);
    }

    [HttpGet("managers")]
    public async Task<IActionResult> GetManagers()
    {
        var managers = await _employeesRepository.GetManagersAsync();

        return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(managers));
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="employeeId">The employee id</param>
    /// <returns>Returns average order amount for a specific employee</returns>
    [HttpGet("{employeeId:int}/average-order-amount")]
    public async Task<IActionResult> GetAverageOrderAmount(int employeeId)
    {
        var employee = await _employeesRepository.GetEmployeeAsync(employeeId);

        if (employee == null)
        {
            return NotFound();
        }

        var averageOrderAmount = await _employeesRepository.CalculateAverageOrderAmountAsync(employeeId);

        return Ok(new { AverageOrderAmount = averageOrderAmount });
    }
}