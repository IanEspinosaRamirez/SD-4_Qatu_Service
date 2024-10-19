using Application;
using Infrastructure;
using Web.Api;
using Web.Api.Extensions;
using Web.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<HandleErrors>();

app.MapControllers();

app.Run();
