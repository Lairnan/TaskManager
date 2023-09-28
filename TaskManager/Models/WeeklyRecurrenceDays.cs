using System;
using Prism.Mvvm;

namespace TaskManager.Models;

public class WeeklyRecurrenceDays : BindableBase
{
    private DayOfWeek _day;
    private bool _isActive;

    public WeeklyRecurrenceDays()
    {
        Day = DayOfWeek.Monday;
        IsActive = false;
    }

    public WeeklyRecurrenceDays(DayOfWeek day)
    {
        Day = day;
        IsActive = false;
    }

    public WeeklyRecurrenceDays(DayOfWeek day, bool isActive)
    {
        Day = day;
        IsActive = isActive;
    }

    public DayOfWeek Day
    {
        get => _day;
        set => SetProperty(ref _day, value);
    }

    public bool IsActive
    {
        get => _isActive;
        set => SetProperty(ref _isActive, value);
    }
}