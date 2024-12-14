using HoneyDoApi.interfaces;
using HoneyDoApi.models;
using MongoDB.Driver;

namespace HoneyDoApi.services;

public class ReminderService(IReminderRepo reminderRepo) : IReminderService
{
    public async Task<Reminder> CreateReminderAsync(Reminder newReminder)
    {
        await reminderRepo.CreateReminder(newReminder);
        return await reminderRepo.GetReminderById(newReminder.Id);
    }

    public async Task<Reminder> GetReminderByIdAsync(string reminderId)
    {
       return await reminderRepo.GetReminderById(reminderId);
    }

    public async Task<Reminder> UpdateReminderAsync(Reminder updatedReminder)
    {
        return await reminderRepo.UpdateReminder(updatedReminder);
    }
}