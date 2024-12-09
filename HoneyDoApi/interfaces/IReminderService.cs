using HoneyDoApi.models;

namespace HoneyDoApi.interfaces;

public interface IReminderService
{
    public Task<Reminder> CreateReminder(Reminder newReminder);
}