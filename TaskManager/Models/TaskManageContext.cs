using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManager.Models.Entities;

namespace TaskManager.Models;

public interface IDbEntity { }

public class TaskManageContext : TaskManageDbContext
{
    public TaskManageContext(DbContextOptions<TaskManageContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        if (!Database.EnsureCreated()) return;
        
        var tag = new Tag { Name = "New Tag" };
        var tag2 = new Tag { Name = "New Tag 2" };
        var tag3 = new Tag { Name = "New Tag 3" };

        var task = new Task
        {
            Name = "Test",
            Description = "TEst",
            DueDate = DateTime.Parse("14.09.2023"),
            Priority = 1,
            Completed = false,
            Recurring = false,
            Interval = RecurringInterval.Weekly,
            WeeklyRecurrenceDays = new System.Collections.Generic.HashSet<DayOfWeek>
            {
				DayOfWeek.Monday,
				DayOfWeek.Wednesday,
				DayOfWeek.Tuesday
			},
            CreatedAt = DateTime.Now
        };

        Tags.Add(tag);
        
        task.Tags.Add(tag);
        task.Tags.Add(tag2);
        task.Tags.Add(tag3);
        
        Tasks.Add(task);

        var newTask = new Task
        {
            Name = "Test2",
            Description = "TEst2",
            DueDate = DateTime.Parse("17.09.2023"),
            Priority = 1,
            Completed = false,
            Recurring = true,
            Interval = RecurringInterval.Monthly,
            WeeklyRecurrenceDays = new System.Collections.Generic.HashSet<DayOfWeek>
            {
                DayOfWeek.Monday
            },
            CreatedAt = DateTime.Now
        };
        
        Tasks.Add(newTask);

        SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>().
            Property(p => p.DueDate)
            .HasConversion(new DateTimeToTicksConverter());
        
        modelBuilder.Entity<Task>().
            Property(p => p.CreatedAt)
            .HasConversion(new DateTimeToTicksConverter())
            .HasDefaultValue(DateTime.Now);
        
        modelBuilder.Entity<Reminder>().
            Property(p => p.DueDate)
            .HasConversion(new DateTimeToTicksConverter());
        
        modelBuilder.Entity<Reminder>().
            Property(p => p.CreatedAt)
            .HasConversion(new DateTimeToTicksConverter())
            .HasDefaultValue(DateTime.Now);
    }
}