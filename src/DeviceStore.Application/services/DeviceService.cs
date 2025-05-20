using DeviceStore.Models.DTOs;
using DeviceStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace DeviceStore.Application;

public class DeviceService : IDeviceService
{
    private readonly DevicesContext _context;
    public DeviceService(DevicesContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetDevicesDTO>> GetAllAsync()
    {
        var result = await _context.Devices.Select(d => new GetDevicesDTO
        {
            Id = d.Id,
            Name = d.Name,
        }).ToListAsync();
        return result;
    }
}