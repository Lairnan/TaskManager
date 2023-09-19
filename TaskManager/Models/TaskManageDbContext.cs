using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;

namespace TaskManager.Models;

public abstract class TaskManageDbContext : DbContext
{
    protected TaskManageDbContext(DbContextOptions<TaskManageContext> options) : base(options)
    {
    }

    public required DbSet<Task> Tasks { get; set; }
    public required DbSet<Tag> Tags { get; set; }
    public required DbSet<Reminder> Reminders { get; set; }
}