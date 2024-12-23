using System.Text.Json.Serialization;
using HoneyDoApi.interfaces;
using HoneyDoApi.models;
using HoneyDoApi.repos;
using HoneyDoApi.services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var connectionString = builder.Configuration.GetValue<string>(("MongoDBSettings:ConnectionString"));
var settings = MongoClientSettings.FromConnectionString(connectionString);
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
var client = new MongoClient(settings);
var database = client.GetDatabase(builder.Configuration.GetValue<string>("MongoDBSettings:DatabaseName"));

builder.Services.AddSingleton(database);
builder.Services.AddSingleton<IReminderRepo, ReminderRepo>();
builder.Services.AddSingleton<IReminderService, ReminderService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalhostOnly", policy =>
    {
        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors("LocalhostOnly");
var reminders = new List<Reminder> { new Reminder() };

var todosApi = app.MapGroup("/reminders");
todosApi.MapGet("/get-reminder-by-id/",
    async (HttpContext context, IReminderService reminderService, string reminderId) =>
    {
        try
        {
            return Results.Ok(await reminderService.GetReminderByIdAsync(reminderId));
        }    
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Results.StatusCode(500);
        }
        return Results.StatusCode(500);
        
    });
todosApi.MapPost("/create-reminder", async (HttpContext context, IReminderService reminderService, Reminder reminder) =>
{
    try
    {
        return Results.Ok(await reminderService.CreateReminderAsync(reminder));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.StatusCode(500);
    }

});
todosApi.MapPost("/update-reminder", async (HttpContext context, IReminderService reminderService, Reminder reminder) =>
{
    try
    {
        return Results.Ok(await reminderService.UpdateReminderAsync(reminder));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.StatusCode(500);
    }

});
app.Run();
[JsonSerializable(typeof(List<Reminder>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}