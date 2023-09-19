using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prism.Mvvm;

namespace TaskManager.Models.Entities;

public class Reminder : BindableBase, IDbEntity
{
    private DateTime _createdAt;
    private DateTime _dueDate;
    private Guid _id;

    public Reminder()
    {
        Id = new Guid();
    }

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

    [Required] [ForeignKey("Task")] public Guid TaskId { get; set; }

    public Task Task { get; set; }
}