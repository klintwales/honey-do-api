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

builder.Services.AddScoped<IMongoDatabase>(sp => 
    sp.GetRequiredService<IMongoClient>().GetDatabase(builder.Configuration.GetValue<string>("MongoDBSettings:DatabaseName")));

// Add repository to the container
builder.Services.AddScoped<IReminderRepo, ReminderRepo>();
builder.Services.AddScoped<IReminderService, ReminderService>();
var app = builder.Build();

var reminders = new List<Reminder> { new Reminder() };

var todosApi = app.MapGroup("/reminders");
todosApi.MapGet("/get-all", () => reminders);
todosApi.MapPost("/create-reminder", (context) => null);
app.Run();

[JsonSerializable(typeof(List<Reminder>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}