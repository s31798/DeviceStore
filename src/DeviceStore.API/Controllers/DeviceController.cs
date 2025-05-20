using DeviceStore.Application;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;
    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }
    
    [HttpGet]
    public async Task<IResult> Get()
    {
        var products = await _deviceService.GetAllAsync();
        return Results.Ok(products);
    }
}