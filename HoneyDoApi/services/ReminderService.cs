using HoneyDoApi.interfaces;
using HoneyDoApi.models;

namespace HoneyDoApi.services;

public class ReminderService(IReminderRepo reminderRepo) : IReminderService
{
    public async Task<Reminder> CreateReminder(Reminder newReminder)
    {
        reminderRepo.CreateReminder(newReminder);
        return await reminderRepo.GetReminderById(newReminder.Id);
    }
}