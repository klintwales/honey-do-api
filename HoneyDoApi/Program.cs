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
builder.Services.AddSingleton<IMongoClient>(_ =>
{
    var connectionString = builder.Configuration.GetValue<string>(("MongoDBSettings:ConnectionString"));
    return new MongoClient(connectionString);
});
builder.Services.AddScoped<IMongoDatabase>(sp => 
    sp.GetRequiredService<IMongoClient>()
        .GetDatabase(builder.Configuration.GetValue<string>("MongoDBSettings:DatabaseName")));
builder.Services.AddScoped<IReminderRepo, ReminderRepo>();
builder.Services.AddScoped<IReminderService, ReminderService>();
var app = builder.Build();

var reminders = new List<Reminder> { new Reminder() };

var todosApi = app.MapGroup("/reminders");
todosApi.MapGet("/get-all", () => reminders);
todosApi.MapPost("/create-reminder", async (HttpContext context, IReminderService reminderService) =>
{
    var reminder = new Reminder();
    reminder.Title = "Reminder";
    reminder.Description = "Description";
    reminder.Complete = false;
    reminder.CreatedDate = DateTime.UtcNow;
    reminder.ModifiedDate = DateTime.UtcNow;
    await reminderService.CreateReminder(reminder);
    return Results.Ok("Reminder created");

});
app.Run();
[JsonSerializable(typeof(List<Reminder>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}