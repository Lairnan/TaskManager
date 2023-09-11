using System;
using System.Collections.Generic;

namespace TaskManager.Models.Entities;

public class Task : IDbEntity
{
    public Task() { Id = new Guid(); }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
    public bool Completed { get; set; }
    public bool Recurring { get; set; }
    public int? RecurringInterval { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Tag { get; set; }

    public List<Tag> Tags { get; set; } = new();
    public List<Reminder> Reminders { get; set; } = new();
}