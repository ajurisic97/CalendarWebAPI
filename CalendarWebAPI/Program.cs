using CalendarWebAPI.DbModels;
using CalendarWebAPI.Repositories;
using CalendarWebAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//REPOSITORIES
builder.Services.AddScoped<CalendarItemsRepository>();
builder.Services.AddScoped<SchedulerRepository>();
//SERVICES
builder.Services.AddScoped<CalendarItemsService>();
builder.Services.AddScoped<SchedulerService>();


builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<CalendarContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


