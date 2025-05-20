using DeviceStore.Models.DTOs;
using DeviceStore.Repository;

namespace DeviceStore.Application;

public interface IDeviceService
{
    Task<IEnumerable<GetDevicesDTO>> GetAllAsync();
}