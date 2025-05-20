using DeviceStore.Models.DTOs;

namespace DeviceStore.Application;

public interface IEmployeeService
{
    Task<IEnumerable<GetEmployeesDTO>> GetEmployeesAsync(CancellationToken ct);
    Task<GetEmployeeByIdDTO> GetEmployeeByIdAsync(int id,CancellationToken ct);
    
}