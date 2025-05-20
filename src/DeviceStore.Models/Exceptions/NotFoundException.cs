namespace DeviceStore.Models.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(int id) : base($"Device with ID {id} not found.") { }
}