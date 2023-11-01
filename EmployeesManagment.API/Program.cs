using EmployeesManagment.API.Data;
using EmployeesManagment.API.Mappings;
using EmployeesManagment.API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//Adding authorization in swagger ui testing
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Managment API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=  ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },

                    Scheme = "Oauth2",
                    Name=JwtBearerDefaults.AuthenticationScheme,
                    In=ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//Injecting a Dependency Injection manually. In this case, is a DI using DbContextClass and connection string :)
builder.Services.AddDbContext<EmployeesManagmentDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeManagmentConnectionString")));


builder.Services.AddDbContext<EmployeesManagmentAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeManagmentAuthConnectionString")));

//using AddScoped from .net to access our interfaces and classes to our program
builder.Services.AddScoped<IEmployeeRepository,SQLEmployeeRepository>();
builder.Services.AddScoped<IJobOfferRepository,SQLJobOfferRepository>();
builder.Services.AddScoped<ITokenRepository,TokenRepository>();

//Activated Automapper created by us
builder.Services.AddAutoMapper(typeof(AutomapperProfiles));


builder.Services
    .AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("EmployeesManagment")
    .AddEntityFrameworkStores<EmployeesManagmentAuthDbContext>()
    .AddDefaultTokenProviders();


//validation for setting password at Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

});

//Adding Jwt Bearer Authentication. Adding parameters for token validation, including issuer, audience, lifetime and issuer signing key
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Activated Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
