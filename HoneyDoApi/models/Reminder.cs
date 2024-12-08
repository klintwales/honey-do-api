namespace HoneyDoApi.models;

public class Reminder: IReminder
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool Complete { get; set; }
}