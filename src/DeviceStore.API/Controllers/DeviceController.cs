using DeviceStore.Application;
using DeviceStore.Models.DTOs;
using DeviceStore.Models.Exceptions;
using DeviceStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/devices")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;
    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }
    
    [HttpGet]
    public async Task<IResult> GetDevicesAsync(CancellationToken ct)
    {
        try
        {
            var devices = await _deviceService.GetAllAsync(ct); 
            return Results.Ok(devices);
        }
        catch (Exception e)
        {
            return Results.StatusCode(500);
        }
    }
    [HttpGet("{id}")]
    public async Task<IResult> GetDeviceByIdAsync(int id,CancellationToken ct)
    {
        try
        {
            var device = await _deviceService.GetByIdAsync(id,ct); 
            return Results.Ok(device);
        }
        catch (Exception e)
        {
            return Results.StatusCode(500);
        }
      
    }

    [HttpPost]
    public async Task<IResult> AddDeviceAsync([FromBody] PostDeviceDTO device, CancellationToken ct)
    {
        try
        {
            await _deviceService.PostDeviceAsync(device, ct);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.StatusCode(500);
        }
        
    }
    [HttpPatch("{id}")]
    public async Task<IResult> UpdateDeviceAsync(int id, [FromBody] PostDeviceDTO device, CancellationToken ct)
    {
        try
        {
            await _deviceService.PatchDeviceAsync(id, device, ct);
            return Results.Ok();
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (TypeNotExistException e)
        {
            return Results.BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Results.StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteDeviceAsync(int id, CancellationToken ct)
    {
        try
        {
            await _deviceService.DeleteDeviceAsync(id, ct);
            return Results.Ok();
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Results.StatusCode(500);
        }
    }
}