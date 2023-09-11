using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;

namespace TaskManager.Models;

public class TaskManageContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Data Source=TaskManager.db");

    public DbSet<Task> Tasks { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
}