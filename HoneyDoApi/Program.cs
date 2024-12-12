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
var app = builder.Build();

var reminders = new List<Reminder> { new Reminder() };

var todosApi = app.MapGroup("/reminders");
todosApi.MapGet("/get-all", () => reminders);
todosApi.MapPost("/create-reminder", async (HttpContext context, IReminderService reminderService, Reminder reminder) =>
{
    try
    {
        await reminderService.CreateReminder(reminder);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.StatusCode(500);
    }
    return Results.Ok("Reminder created");

});
app.Run();
[JsonSerializable(typeof(List<Reminder>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}