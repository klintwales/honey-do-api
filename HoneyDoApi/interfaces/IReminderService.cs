using HoneyDoApi.models;
using MongoDB.Driver;

namespace HoneyDoApi.interfaces;

public interface IReminderService
{
    public Task<Reminder> CreateReminderAsync(Reminder newReminder);

    public Task<Reminder> GetReminderByIdAsync(string reminderId);
    
    public Task<Reminder> UpdateReminderAsync(Reminder updatedReminder);
    
}