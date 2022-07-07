using ShowTrack.Server.Models;
using ShowTrack.Server.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);
        ConfigureApp(builder);
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.Configure<ShowsDatabaseSettings>(
            builder.Configuration.GetSection("ShowsDatabaseSettings"));
        
        builder.Services.AddSingleton<ShowsService>();
        
        builder.Services.AddControllersWithViews()
        .AddJsonOptions(
            options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

        builder.Services.AddRazorPages();

        builder.Services.AddSwaggerGen();
    }

    private static void ConfigureApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShowTrack API V1");
            });
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}