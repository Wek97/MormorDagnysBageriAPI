using Microsoft.EntityFrameworkCore;
using MormorDagnysBageri.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MormorDagnysContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("sqlitedev"));
});

builder.Services.AddControllers();

var app = builder.Build();

var seedDatabase = new SeedDatabase();
await seedDatabase.InitDb(app);

app.MapControllers();

app.Run();