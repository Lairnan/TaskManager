using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;

namespace TaskManager.Models;

public abstract class TaskManageDbContext : DbContext
{
    protected TaskManageDbContext(DbContextOptions<TaskManageContext> options) : base(options) { }
    
    public DbSet<Task>? Tasks { get; set; }
    public DbSet<Tag>? Tags { get; set; }
    public DbSet<Reminder>? Reminders { get; set; }
}