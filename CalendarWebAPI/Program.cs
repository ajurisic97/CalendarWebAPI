using CalendarWebAPI.DbModels;
using CalendarWebAPI.Repositories;
using CalendarWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using CalendarWebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using CalendarWebAPI.Helper;

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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
//REPOSITORIES
builder.Services.AddScoped<CalendarItemsRepository>();
builder.Services.AddScoped<SchedulerRepository>();
builder.Services.AddScoped<EventRepository>();
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<WorkingDayRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoleRepository>();
//SERVICES
builder.Services.AddScoped<CalendarItemsService>();
builder.Services.AddScoped<SchedulerService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<WorkingDayService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();

builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.Converters.Add(new StringEnumConverter());
});
builder.Services.AddDbContext<CalendarContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("CORSPolicy");
app.UseAuthentication();
app.UseAuthorization();
AppDbInitializer.Seed(app);
app.UseMiddleware<AuthorizationMiddleware>();
app.MapControllers();

app.Run();

/*Just in case
 * 
Scaffold-DbContext "Data Source=.;Initial Catalog=Calendar;User ID=sa;Password=Andelo1234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DbModels -UseDatabaseNames -Force -Context "CalendarContext" -Schema "Catalog","Person" * */
