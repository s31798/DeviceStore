using System.Text.Json;
using DeviceStore.Models.DTOs;
using DeviceStore.Models.Exceptions;
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

    public async Task<IEnumerable<GetDevicesDTO>> GetAllAsync(CancellationToken ct)
    {
        var result = await _context.Devices.Select(d => new GetDevicesDTO
        {
            Id = d.Id,
            Name = d.Name,
        }).ToListAsync(ct);
        return result;
    }

    public async Task<GetDeviceByIdDTO> GetByIdAsync(int id, CancellationToken ct)
    {
        
        try
        {
            var device = await _context.Devices
                .Where(d => d.Id == id)
                .Select(d => new 
                {
                    d.Name,
                    d.DeviceTypeId,
                    d.IsEnabled,
                    d.AdditionalProperties
                })
                .FirstOrDefaultAsync(ct);
            var deviceType = await _context.DeviceTypes.Where(t => t.Id == device.DeviceTypeId).Select(t => t.Name).FirstOrDefaultAsync(ct);
            var additionalProperties = string.IsNullOrEmpty(device.AdditionalProperties) 
                ? new Dictionary<string, object>()
                : JsonSerializer.Deserialize<Dictionary<string, object>>(device.AdditionalProperties);
            
            var employeeId = await _context.DeviceEmployees.Where(de => de.DeviceId == id).Select(de => de.EmployeeId).FirstOrDefaultAsync(ct);
            var employeeName = await _context.Employees.Include(e => e.Person).Where(e => e.Id == employeeId)
                .Select(e => e.Person.FirstName).FirstOrDefaultAsync(ct);
             return new GetDeviceByIdDTO 
             {
                 Name = device.Name,
                 DeviceType = deviceType,
                 isEnabled = device.IsEnabled,
                 AdditionalProperties = additionalProperties,
                 CurrentEmpoyee = new EmployeDeviceDTO
                 {
                     Id = employeeId,
                     Name = employeeName,
                 }
                        
             };

        }
        catch (Exception e)
        {
            throw e;
        }
        
    }

    public async Task PostDeviceAsync(PostDeviceDTO device, CancellationToken ct)
    {
        var deviceTypeId = await _context.DeviceTypes.Where(t => t.Name == device.DeviceTypeName).Select(t => t.Id).FirstOrDefaultAsync(ct);

        var insDevice = new Device
        {
            Name = device.Name,
            IsEnabled = device.IsEnabled,
            AdditionalProperties = JsonSerializer.Serialize(device.AdditionalProperties),
            DeviceTypeId = deviceTypeId,
        };
        await _context.Devices.AddAsync(insDevice, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task PatchDeviceAsync(int id, PostDeviceDTO device, CancellationToken ct)
    {
        var existingDevice = await _context.Devices
            .FirstOrDefaultAsync(d => d.Id == id, ct);

        if (existingDevice == null)
            throw new NotFoundException(id);

        var deviceTypeId = await _context.DeviceTypes
            .Where(t => t.Name == device.DeviceTypeName)
            .Select(t => t.Id)
            .FirstOrDefaultAsync(ct);

        if (deviceTypeId == 0)
            throw new Exception($"Device type '{device.DeviceTypeName}' not found.");

        existingDevice.Name = device.Name;
        existingDevice.IsEnabled = device.IsEnabled;
        existingDevice.DeviceTypeId = deviceTypeId;
        existingDevice.AdditionalProperties = JsonSerializer.Serialize(device.AdditionalProperties);

        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteDeviceAsync(int id, CancellationToken ct)
    {
        var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id,ct);
        if (device == null)
        {
            throw new NotFoundException(id);
        }
        _context.Devices.Remove(device);
        await _context.SaveChangesAsync(ct);
    }
}