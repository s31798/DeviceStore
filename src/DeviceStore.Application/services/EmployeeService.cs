using DeviceStore.Models.DTOs;
using DeviceStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace DeviceStore.Application;

public class EmployeeService : IEmployeeService
{
    private readonly DevicesContext _context;
    public EmployeeService(DevicesContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetEmployeesDTO>> GetEmployeesAsync(CancellationToken ct)
    {
        var result = await _context.Employees.Include(e => e.Person).Select(e => new GetEmployeesDTO
        {
            Id = e.Id,
            FullName = e.Person.FirstName + " " + e.Person.LastName,

        }).ToListAsync(ct);
        return result;
    }

    public async Task<GetEmployeeByIdDTO> GetEmployeeByIdAsync(int id, CancellationToken ct)
    {
        var employee = await _context.Employees
            .Where(e => e.Id == id)
            .Select(e => new GetEmployeeByIdDTO
            {
                PersonData = e.Person,
                Salary = e.Salary,
                HireDate = e.HireDate,
                Position = new EmployeePositionDTO
                {
                    PositionId = e.Position.Id,
                    PositionName = e.Position.Name
                }
            })
            .FirstOrDefaultAsync(ct);
        if(employee == null)
            throw new KeyNotFoundException("Employee not found");
        return employee;
    }
}