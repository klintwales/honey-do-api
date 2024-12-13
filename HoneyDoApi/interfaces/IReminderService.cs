using HoneyDoApi.models;
using MongoDB.Driver;

namespace HoneyDoApi.interfaces;

public interface IReminderService
{
    public Task<Reminder> CreateReminder(Reminder newReminder);
    
    public Task<UpdateResult> UpdateReminder(Reminder updatedReminder);
    
}