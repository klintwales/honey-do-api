using HoneyDoApi.models;
using HoneyDoApi.repos;
using MongoDB.Driver;

namespace HoneyDoApi.interfaces;

public interface IReminderRepo
{
    Task<Reminder> GetAllReminders(string id);
    // Add other methods as needed
}