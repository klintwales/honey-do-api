using HoneyDoApi.models;
using HoneyDoApi.repos;
using MongoDB.Driver;

namespace HoneyDoApi.interfaces;

public interface IReminderRepo
{
    Task<Reminder> GetAllReminders();
    Task<Reminder> GetReminderById(int id);
    void CreateReminder(Reminder reminder);
}