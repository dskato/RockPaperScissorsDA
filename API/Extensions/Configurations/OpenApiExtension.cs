using Microsoft.OpenApi.Models;

public class OpenApiExtension
{

    public static void ConfigureOpenApi(WebApplicationBuilder builder)
    {

        var contact = new OpenApiContact()
        {
            Name = "David Araujo",
            Email = "dskato0603@gmail.com"
        };

        var info = new OpenApiInfo()
        {
            Version = "v1",
            Title = "Rock Paper Scissors API",
            Description = "RPS microservice.",
            Contact = contact
        };

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", info);
            
        });

    }
}