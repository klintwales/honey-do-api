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

    public async Task<Reminder> GetReminderById(string id)
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

    public async Task<UpdateResult> UpdateReminder(Reminder reminder)
    {
        var filter = Builders<Reminder>.Filter.Eq("_id", new ObjectId(reminder.Id));
        var update = Builders<Reminder>.Update
            .Set("Title", reminder.Title)
            .Set("Description", reminder.Description)
            .Set("ModifiedDate", DateTime.UtcNow)
            .Set("Complete", reminder.Complete);
        var options = new FindOneAndUpdateOptions<Reminder>{
            ReturnDocument = ReturnDocument.After
        };
        var returnedReminder = await _reminders.UpdateOneAsync(filter, update);
        return returnedReminder;
    }
}