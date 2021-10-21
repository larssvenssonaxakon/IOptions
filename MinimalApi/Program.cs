using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MinimalApi.Handlers;
using MinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITestHandler, TestHandler>();

// Configure JSON options
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.IncludeFields = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TestNet6", Version = "v1" });
});

await using var app = builder.Build();
#if DEBUG
app.Urls.Add("https://*:*");
#endif

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestNet6 v1");
        c.RoutePrefix = string.Empty;
        });
}

app.MapGet("/hello/{name}", (string name, string thisIsFromQuery) =>
{
    if (thisIsFromQuery == null)
    {
        return Results.BadRequest();
    }

    var result = $"Hello from route {name}! Hello from query {thisIsFromQuery}";
    return Results.Ok(result);
}).WithName("get-hello") //Swagger operation-id
.WithTags("HelloGroup") //Grouping in swagger, doesn't seem to work
.Produces(400).Produces<string>(200); //Describe result along with http status code

app.MapPost("/hello/{name}", (string name, string thisIsFromQuery, TestRequest testRequest) => $"Hello from route {name}! Hello from query {thisIsFromQuery}! Hello from body {testRequest.Body}");

app.MapGet("/handler/{name}", (string name, [FromServices] ITestHandler handler) => handler.Handle(name));

app.MapGet("/noshow", () => { })
    .ExcludeFromDescription(); //Hide from swagger.

await app.RunAsync();