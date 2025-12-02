using MyBackend.Data;
using MyBackend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureDatabase(builder.Configuration);

// Add MVC with views and Razor pages support
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureServices();
builder.Services.ConfigureSwagger();

var app = builder.Build();
app.UseCors("FrontendPolicy");

// Enable static files (CSS, JS, images)
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANT: authentication before authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers for API endpoints
app.MapControllers();

// Map default route for MVC views
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed database with test data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.SeedData(context);
}

app.Run();