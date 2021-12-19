using Microsoft.OpenApi.Models;
using TestNet6;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.AddAppSettingsSection(new AppSettings());
var subSettings = builder.AddAppSettingsSection(new SubSettings());

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TestNet6", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestNet6 v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
