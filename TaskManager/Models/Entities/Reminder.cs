using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prism.Mvvm;

namespace TaskManager.Models.Entities;

public class Reminder : BindableBase, IDbEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DateTime _createdAt;
    private DateTime _dueDate;
    private Guid _id = Guid.NewGuid();
    private Task _task;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Key]
    public Guid Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    [Required]
    public DateTime DueDate
    {
        get => _dueDate;
        set => SetProperty(ref _dueDate, value);
    }

    [Required]
    public DateTime CreatedAt
    {
        get => _createdAt;
        set => SetProperty(ref _createdAt, value);
    }

    [Required]
    [ForeignKey("Task")]
    public Guid TaskId { get; set; }
    
    public required Task Task
    {
        get => _task;
        set => SetProperty(ref _task, value);
    }
}