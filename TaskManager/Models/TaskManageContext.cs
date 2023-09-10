using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;

namespace TaskManager.Models;

public class TaskManageContext : DbContext
{
    public const string DbName = "TaskManager.db";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite($"Data Source={DbName}");

    public DbSet<Task> Tasks { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<SyncService> SyncServices { get; set; }
    public DbSet<User> Users { get; set; }
}