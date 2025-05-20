namespace DeviceStore.Models.DTOs;

public class PostDeviceDTO
{
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public string DeviceTypeName { get; set; }
    public Dictionary<string, object> AdditionalProperties { get; set; }
}