using EmployeeManagement.APIs.AutoMapper;
using EmployeeManagementSystem.BL.Implementations;
using EmployeeManagementSystem.BL.Interfaces;
using EmployeeManagementSystemDBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(s =>
//{
//    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//     {
//         Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
//                      Enter 'Bearer' [space] and then your token in the text input below.
//                      \r\n\r\nExample: 'Bearer 12345abcdef'",
//         Name = "Authorization",
//         In = ParameterLocation.Header,
//         Type = SecuritySchemeType.ApiKey,
//         Scheme = "Bearer"
//     });
//});
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});

// Configure database and db context
builder.Services.AddDbContext<TeleDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TeleDB"), builder =>
    {
        var assembly = Assembly.GetAssembly(typeof(TeleDBContext));
        builder.MigrationsAssembly(assemblyName: assembly?.FullName);
    });
});

// Configure JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTConfiguration:SecretKey").Value))
    };
});

// Configure DI
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IAuthorizationBL, AuthorizationBL>();
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);


app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
//app.MapControllers();

app.Run();
