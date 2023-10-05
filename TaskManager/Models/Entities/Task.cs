using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prism.Mvvm;

namespace TaskManager.Models.Entities;

public class Task : BindableBase, IDbEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private bool _completed;
    private DateTime _createdAt;
    private string _description;
    private DateTime _dueDate;
    private RecurringInterval? _interval;
    private string _name;
    private int _priority;
    private bool _recurring;
    private HashSet<DayOfWeek>? _weeklyRecurrenceDays = new();
    private Guid _id = Guid.NewGuid();
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Key]
    public Guid Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    [Required]
    [StringLength(255)]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    [StringLength(4000)]
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    [Required]
    public DateTime DueDate
    {
        get => _dueDate;
        set => SetProperty(ref _dueDate, value);
    }

    [Required]
    [Range(1, 5)]
    public int Priority
    {
        get => _priority;
        set => SetProperty(ref _priority, value);
    }

    [Required]
    public bool Completed
    {
        get => _completed;
        set => SetProperty(ref _completed, value);
    }

    [Required]
    public bool Recurring
    {
        get => _recurring;
        set => SetProperty(ref _recurring, value);
    }

    public RecurringInterval? Interval
    {
        get => _interval;
        set => SetProperty(ref _interval, value);
    }

    public HashSet<DayOfWeek>? WeeklyRecurrenceDays
    {
        get => _weeklyRecurrenceDays;
        set => SetProperty(ref _weeklyRecurrenceDays, value);
    }

    [Required]
    public DateTime CreatedAt
    {
        get => _createdAt;
        set => SetProperty(ref _createdAt, value);
    }

    [ForeignKey("Tag")] public List<Tag> Tags { get; } = new();

    public IEnumerable<Reminder> Reminders { get; } = new List<Reminder>();
}

public enum RecurringInterval
{
    [Description("Ежедневно")] Daily = 0,
    [Description("Еженедельно")] Weekly = 1,
    [Description("Ежемесячно")] Monthly = 2,
    [Description("Ежегодно")] Yearly = 3
}