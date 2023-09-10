using System;

namespace TaskManager.Models.Entities;

public class Reminder
{
    public Reminder() { Id = new Guid(); }
    
    public Guid Id { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid TaskId { get; set; }
    
    public Task Task { get; set; }
}