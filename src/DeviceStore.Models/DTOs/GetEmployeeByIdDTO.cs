using DeviceStore.Repository;

namespace DeviceStore.Models.DTOs;

public class GetEmployeeByIdDTO
{
    public Person PersonData { get; set; }
    public decimal Salary { get; set; }
    public EmployeePositionDTO Position { get; set; }
    public DateTime HireDate { get; set; }
}