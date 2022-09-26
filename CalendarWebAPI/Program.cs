using CalendarWebAPI.DbModels;
using CalendarWebAPI.Repositories;
using CalendarWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using CalendarWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:8158").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//REPOSITORIES
builder.Services.AddScoped<CalendarItemsRepository>();
builder.Services.AddScoped<SchedulerRepository>();
builder.Services.AddScoped<EventRepository>();
builder.Services.AddScoped<PersonRepository>();
//SERVICES
builder.Services.AddScoped<CalendarItemsService>();
builder.Services.AddScoped<SchedulerService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<PersonService>();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.Converters.Add(new StringEnumConverter());
});
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
app.UseCors("CORSPolicy");
app.UseAuthorization();
AppDbInitializer.Seed(app);
app.MapControllers();

app.Run();

/*Just in case
 * 
Scaffold-DbContext "Data Source=.;Initial Catalog=Calendar;User ID=sa;Password=Andelo1234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DbModels -UseDatabaseNames -Force -Context "CalendarContext" -Schema "Catalog","Person" * */
