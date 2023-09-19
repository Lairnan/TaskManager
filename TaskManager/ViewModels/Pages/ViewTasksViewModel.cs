using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using TaskManager.Models;
using TaskManager.Models.Entities;
using TaskManager.Services.Interface;
using AsyncTask = System.Threading.Tasks.Task;

namespace TaskManager.ViewModels.Pages;

public class ViewTasksViewModel : BindableBase
{
    private readonly TaskManageDbContext _taskManageContext;
    private readonly IPageService _pageService;

    private string _filterText = string.Empty;
    private ObservableCollection<Task> _tasksCollection = null!;
    private Task? _lastSelectedTask;
    private Task? _selectedTask;
    private DelegateCommand<Task>? _mouseLeftButtonUpCommand;
    private IList<Tag> _filterTag = new List<Tag>();
    private DateTime? _filterDate;
    private DelegateCommand<Tag>? _removeTagFromFilterCommand;
    private DelegateCommand<Tag>? _addTagToFilterCommand;

    public ViewTasksViewModel(TaskManageDbContext taskManageContext, IPageService pageService)
    {
        _taskManageContext = taskManageContext;
        _pageService = pageService;
        
        FilterTasks(FilterText, FilterTag, FilterDate);
    }

    public string FilterText
    {
        get => _filterText;
        set => SetProperty(ref _filterText, value, () => FilterTasks(value, _filterTag, _filterDate));
    }

    public IList<Tag> FilterTag
    {
        get => _filterTag;
        set => SetProperty(ref _filterTag, value, () => FilterTasks(_filterText, value, _filterDate));
    }

    public DateTime? FilterDate
    {
        get => _filterDate;
        set => SetProperty(ref _filterDate, value, () => FilterTasks(_filterText, _filterTag, value));
    }

    public ObservableCollection<Task> TasksCollection
    {
        get => _tasksCollection;
        private set => SetProperty(ref _tasksCollection, value);
    }

    private async void FilterTasks(string text, IList<Tag> tags, DateTime? date)
    {
        if (!await _taskManageContext.Tasks.AnyAsync())
            TasksCollection = new ObservableCollection<Task>();
        
        IQueryable<Task> list = _taskManageContext.Tasks.Include(s => s.Tags);

        if (!string.IsNullOrWhiteSpace(text))
            list = list.Where(s => s.Name.Trim().ToLower().Contains(text.Trim().ToLower()));

        if (tags.Any())
        {
            list = list.Where(s =>
                s.Tags.Any(x =>
                    tags.Select(c => c.Id)
                        .Contains(x.Id)));
        }

        if (date != null) list = list.Where(s => s.DueDate.Equals(date.Value));

        TasksCollection = new ObservableCollection<Task>(await list.ToListAsync());
    }

    public Task? SelectedTask
    {
        get => _selectedTask;
        set => SetProperty(ref _selectedTask, value);
    }

    public ICommand MouseLeftButtonUpCommand => _mouseLeftButtonUpCommand ??= new DelegateCommand<Task>(task =>
    {
        if (task != null && task.Equals(_lastSelectedTask))
        {
            SelectedTask = null;
            _lastSelectedTask = null;
        }
        else
        {
            _lastSelectedTask = task;
            SelectedTask = task;
        }
    });

    public ICommand RemoveTagFromFilterCommand => _removeTagFromFilterCommand ??= new DelegateCommand<Tag>(tag =>
    {
        if (FilterTag.Select(s => s.Id).Contains(tag.Id)) FilterTag.Remove(tag);
    });

    public ICommand AddTagToFilterCommand => _addTagToFilterCommand ??= new DelegateCommand<Tag>(tag =>
    {
        if(!FilterTag.Select(s => s.Id).Contains(tag.Id)) FilterTag.Add(tag);
    });
    
    
}