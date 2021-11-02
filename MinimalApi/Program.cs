using Microsoft.AspNetCore.Mvc;
using MinimalApi.Handlers;
using MinimalApi.Models;
using MinimalApi.PetersStaticSingelton;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IHandler<string, string>, TestHandler>();
builder.Services.AddTransient<IHandler<int, string>, AnotherHandler>();

builder.Services.AddTransient<IHandler<CommandToHandle, ResponseToCommand>, AHanlderWithModels>();
builder.Services.AddSingleton<PetersSingelton>();

// Configure JSON options
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.IncludeFields = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TestNet6", Version = "v1" });
    c.SwaggerDoc("v2", new() { Title = "TestNet6", Version = "v2" });
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        c.RoutePrefix = string.Empty;
        });
}

app.MapGet("v1/hello/{name}", (string name, string thisIsFromQuery) =>
{
    if (thisIsFromQuery == null)
    {
        return Results.BadRequest();
    }

    var result = $"Hello from route {name}! Hello from query {thisIsFromQuery}";
    return Results.Ok(result);
}).WithName("get-hello") //Swagger operation-id
.WithTags("HelloGroup") //Grouping in swagger, doesn't seem to work
.WithGroupName("v1")
.Produces(400).Produces<string>(200); //Describe result along with http status code

app.MapPost("v2/hello/{name}", (string name, string thisIsFromQuery, TestRequest thisIsFromBody) => $"Hello from route {name}! Hello from query {thisIsFromQuery}! Hello from body {thisIsFromBody.Body}")
    .WithGroupName("v2");

app.MapGet("v1/handler/{name}", (string name, [FromServices] IHandler<string, string> handler) => handler.Handle(name))
    .WithGroupName("v1");

app.MapGet("v1/usingPetersSingelton/{name}", (string name, [FromServices] PetersSingelton peterSingelton) => peterSingelton.PetersNotStaticStaticMethod(name))
    .WithGroupName("v1");

app.MapGet("v1/anotherhalder/{number}", (int number, [FromServices] IHandler<int, string> anotherHandler) => anotherHandler.Handle(number))
    .WithGroupName("v1");

app.MapPost("v1/handler-with-models", (CommandToHandle thisIsFromBody, [FromServices] IHandler<CommandToHandle, ResponseToCommand> handlerWithModels) => handlerWithModels.Handle(thisIsFromBody))
    .WithGroupName("v1");

app.MapGet("/noshow", () => { })
    .ExcludeFromDescription(); //Hide from swagger.

await app.RunAsync();