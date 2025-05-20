using DeviceStore.Application;
using DeviceStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        var connectionString = builder.Configuration.GetConnectionString("local");
        if (connectionString != null)
        {
            builder.Services.AddDbContext<DevicesContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("local")));
            builder.Services.AddTransient<IDeviceService, DeviceService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
        }
        else
        {
            throw new Exception("No connection string found");
        }
        var app = builder.Build();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}