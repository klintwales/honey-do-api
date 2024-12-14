using HoneyDoApi.models;
using HoneyDoApi.repos;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HoneyDoApi.interfaces;

public interface IReminderRepo
{
    Task<Reminder> GetAllReminders();
    Task<Reminder> GetReminderById(string id);
    Task CreateReminder(Reminder reminder);

    Task<Reminder> UpdateReminder(Reminder reminder);
}