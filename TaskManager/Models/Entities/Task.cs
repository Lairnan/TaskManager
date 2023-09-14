using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models.Entities;

public class Task : IDbEntity
{
    public Task() { Id = new Guid(); }
    
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    
    [StringLength(4000)]
    public string Description { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    [Range(1, 5)]
    public int Priority { get; set; }

    [Required]
    public bool Completed { get; set; }

    [Required]
    public bool Recurring { get; set; }
    public int? RecurringInterval { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("Tag")]
    public List<Tag> Tags { get; set; } = new();
    public List<Reminder> Reminders { get; set; } = new();
}