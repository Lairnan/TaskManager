using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;

namespace TaskManager.Models;

public interface ITaskManageContext
{
    DbSet<Task> Tasks { get; set; }
    DbSet<Tag> Tags { get; set; }
    DbSet<Reminder> Reminders { get; set; }
}