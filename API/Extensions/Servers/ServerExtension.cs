using Microsoft.EntityFrameworkCore;

public class ServerExtension
{
    
    public static void ConfigureSQLServices(WebApplicationBuilder builder, string connectionString)
    {
        
        builder.Services.AddDbContext<AppDbContext>(
            options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(90),
                        errorNumbersToAdd: null
                    );
                });
            }, ServiceLifetime.Transient);
    }
}