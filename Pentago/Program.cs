using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pentago.Services.Authentication;
using Pentago.Services.Authentication.Models;
using Pentago.Services.Engine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var authenticationOptions = new AuthenticationOptions();
        builder.Configuration.GetSection("AuthenticationOptions").Bind(authenticationOptions);

        options.RequireHttpsMetadata = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authenticationOptions.Issuer,
            ValidAudience = authenticationOptions.Audience,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = authenticationOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>(_ =>
    new AuthenticationService(builder.Configuration.GetConnectionString("App")));
builder.Services.AddScoped<IEngine, Engine>(_ => new Engine(builder.Configuration.GetConnectionString("Engine")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();