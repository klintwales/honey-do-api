using HoneyDoApi.interfaces;
using HoneyDoApi.models;
using MongoDB.Driver;

namespace HoneyDoApi.repos;
public class ReminderRepo(IMongoDatabase database) : IReminderRepo
{
    private readonly IMongoCollection<Reminder> _reminders = database.GetCollection<Reminder>("reminders");

    public async Task<Reminder> GetAllReminders(string id)
    {
        return await _reminders.Find(r => r.GetType() == typeof(Reminder)).FirstOrDefaultAsync();
    }
}