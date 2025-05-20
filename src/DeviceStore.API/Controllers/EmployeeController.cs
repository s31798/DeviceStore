using DeviceStore.Application;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IResult> GetEmployeesAsync(CancellationToken ct)
    {
        try
        {
            var results = await _employeeService.GetEmployeesAsync(ct);
            return Results.Ok(results);
        }
        catch (Exception e)
        {
            return Results.StatusCode(500);
        }
    }
    [HttpGet("{id}")]
    public async Task<IResult> GetEmployeeByIdAsync(int id,CancellationToken ct)
    {
        try
        {
            var results = await _employeeService.GetEmployeeByIdAsync(id,ct);
            return Results.Ok(results);
        }
        catch (KeyNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Results.StatusCode(500);
        }
        
    }
    
}