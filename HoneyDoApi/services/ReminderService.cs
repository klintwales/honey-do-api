using HoneyDoApi.interfaces;
using HoneyDoApi.models;
using MongoDB.Driver;

namespace HoneyDoApi.services;

public class ReminderService(IReminderRepo reminderRepo) : IReminderService
{
    public async Task<Reminder> CreateReminder(Reminder newReminder)
    {
        await reminderRepo.CreateReminder(newReminder);
        return await reminderRepo.GetReminderById(newReminder.Id);
    }

    public async Task<UpdateResult> UpdateReminder(Reminder updatedReminder)
    {
        return await reminderRepo.UpdateReminder(updatedReminder);
    }
}