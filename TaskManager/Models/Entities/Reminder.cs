using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models.Entities;

public class Reminder : IDbEntity
{
    public Reminder() { Id = new Guid(); }
    
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    [ForeignKey("Task")]
    public Guid TaskId { get; set; }
    
    public Task Task { get; set; }
}