using DeviceStore.Models.DTOs;
using DeviceStore.Repository;

namespace DeviceStore.Application;

public interface IDeviceService
{
    Task<IEnumerable<GetDevicesDTO>> GetAllAsync(CancellationToken ct);
    Task<GetDeviceByIdDTO> GetByIdAsync(int id,CancellationToken ct);
    
    Task PostDeviceAsync(PostDeviceDTO device, CancellationToken ct);
    Task PatchDeviceAsync(int id, PostDeviceDTO device, CancellationToken ct);
    Task DeleteDeviceAsync(int id, CancellationToken ct);
}