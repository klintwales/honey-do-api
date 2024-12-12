using HoneyDoApi.interfaces;
using HoneyDoApi.models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HoneyDoApi.repos;
public class ReminderRepo(IMongoDatabase database) : IReminderRepo
{
    private readonly IMongoCollection<Reminder> _reminders = database.GetCollection<Reminder>("reminders");

    public async Task<Reminder> GetAllReminders()
    {
        return await _reminders.Find(r => r.GetType() == typeof(Reminder)).FirstOrDefaultAsync();
    }

    public async Task<Reminder> GetReminderById(ObjectId id)
    {
        var reminder = await _reminders.Find(r => r.Id == id).FirstOrDefaultAsync();
        if (reminder == null)
        {
            throw new Exception("No document found with this id.");
        }
        return reminder;
    }

    public async Task CreateReminder(Reminder reminder)
    {
        await _reminders.InsertOneAsync(reminder);
    }
}