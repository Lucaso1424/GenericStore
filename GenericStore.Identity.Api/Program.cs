using Core.Application.Services;
using GenericStore.Application.Mappings;
using GenericStore.Identity.Application.Interfaces;
using GenericStore.Identity.Application.Services;
using GenericStore.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

// AddDbContext() for SQL Server

//var connectionString = builder.Configuration.GetConnectionString("GenericStoreContext");

//builder.Services.AddDbContext<GenericStoreContext>(options =>
//    options.UseSqlServer(connectionString));

// AddDbContext() for PostgreSQL

var connectionString = builder.Configuration.GetConnectionString("GenericStoreContext");

builder.Services.AddDbContext<GenericStoreContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSingleton<UtilsService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Api.Write", policy =>
    {
        policy.RequireClaim("scope", "api.write");
        policy.RequireAuthenticatedUser();
    });

// Configuración de autenticación de Azure AD (para Microsoft Entra ID)
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

//builder.Services.AddAuthentication()
//    .AddOpenIdConnect("aad", options =>
//    {
//        options.Authority = $"https://login.microsoftonline.com/{tenantId}/v2.0";
//        options.ClientId = clientId;
//        options.ClientSecret = clientSecret;
//        options.ResponseType = "code";
//    });

var app = builder.Build();

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

// Configuración Azure 