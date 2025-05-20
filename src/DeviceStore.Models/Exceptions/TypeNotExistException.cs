namespace DeviceStore.Models.Exceptions;

public class TypeNotExistException : Exception
{
    public TypeNotExistException(string message) : base(message) { }
}