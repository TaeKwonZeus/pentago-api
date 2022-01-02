using Pentago.Services.Authentication;
using Pentago.Services.Engine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>(_ => new AuthenticationService(builder.Configuration.GetConnectionString("App")));
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
