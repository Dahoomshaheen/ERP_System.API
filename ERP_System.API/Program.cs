using ERP_System.Infrastructure ;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
// Defining interfaces along with their corresponding classes
using ERP_System.Application.Interfaces;
using ERP_System.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Add services to the container.


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Defaultconnection")));

// General Warehouse Registration
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Registering the item warehouse and the invoice warehouse
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISalesRepository, SalesRepository>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP System API", Version = "v1" });

    // Adding a lock button  in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Enter the token directly here"
});
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Enable Authorization
builder.Services.AddAuthorization();
//----19/11/2026
// Allow Frontend apps to communicate with this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()   // يسمح لأي دومين أو بورت يكلم السيرفر
              .AllowAnyMethod()   // يسمح بكل العمليات (GET, POST, PUT, DELETE)
              .AllowAnyHeader();  // يسمح بإرسال الهيدرز زي الـ Authorization Token
    });
});
//----19/11/2026

var app = builder.Build();


//----19/11/2026
app.UseHttpsRedirection();

// IMPORTANT: Must be placed BEFORE UseAuthentication and UseAuthorization
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();
//----19/11/2026

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
