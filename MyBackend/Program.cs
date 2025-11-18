using MyBackend.Data;
using MyBackend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddControllers();
builder.Services.ConfigureServices();
builder.Services.ConfigureSwagger();

var app = builder.Build();
app.UseCors("FrontendPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANT: authentication before authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed database with test data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.SeedData(context);
}

app.Run();