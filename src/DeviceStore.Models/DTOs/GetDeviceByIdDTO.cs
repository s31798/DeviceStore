namespace DeviceStore.Models.DTOs;

public class GetDeviceByIdDTO
{
    public string Name { get; set; }
    public string DeviceType { get; set; }
    public bool isEnabled { get; set; }
    public Dictionary<string, object>? AdditionalProperties { get; set; }
    public EmployeDeviceDTO CurrentEmpoyee{ get; set; }
}