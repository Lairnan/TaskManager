using System;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;

namespace TaskManager.Models;

public interface IDbEntity { Guid Id { get; set; } }

public class TaskManageContext : DbContext
{
    public TaskManageContext(DbContextOptions<TaskManageContext> options) : base(options) { }

    public DbSet<Task> Tasks { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
}