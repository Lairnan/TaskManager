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
    private readonly IPageService _pageService;
    private readonly TaskManageDbContext _taskManageContext;
    private DelegateCommand<Tag>? _addTagToFilterCommand;
    private DelegateCommand? _clearTagsCommand;
    private DateTime? _filterDate;
    private Tag? _filterTag;
    private ObservableCollection<Tag> _filterTagsCollection = new();

    private string _filterText = string.Empty;
    private Task? _lastSelectedTask;
    private DelegateCommand<Task>? _mouseLeftButtonUpCommand;
    private DelegateCommand<Task>? _openTaskTagsWindowCommand;
    private DelegateCommand<Tag>? _removeTagFromFilterCommand;
    private Task? _selectedTask;
    private ObservableCollection<Tag> _tagsCollection = new();
    private ObservableCollection<Task> _tasksCollection = null!;

    public ViewTasksViewModel(TaskManageDbContext taskManageContext, IPageService pageService)
    {
        _taskManageContext = taskManageContext;
        _pageService = pageService;

        LoadTagsAsync(taskManageContext);

        FilterTasks(FilterText, FilterTagsCollection, FilterDate);
        FilterTagsCollection.CollectionChanged += (s, e) => FilterTasks(FilterText, FilterTagsCollection, FilterDate);
    }

    public string FilterText
    {
        get => _filterText;
        set => SetProperty(ref _filterText, value, () => FilterTasks(value, _filterTagsCollection, _filterDate));
    }

    public Tag? FilterTag
    {
        get => _filterTag;
        set => SetProperty(ref _filterTag, value);
    }

    public ObservableCollection<Tag> FilterTagsCollection
    {
        get => _filterTagsCollection;
        set => SetProperty(ref _filterTagsCollection, value, () => FilterTasks(_filterText, value, _filterDate));
    }

    public ObservableCollection<Tag> TagsCollection
    {
        get => _tagsCollection;
        private set => SetProperty(ref _tagsCollection, value);
    }

    public DateTime? FilterDate
    {
        get => _filterDate;
        set => SetProperty(ref _filterDate, value, () => FilterTasks(_filterText, _filterTagsCollection, value));
    }

    public ObservableCollection<Task> TasksCollection
    {
        get => _tasksCollection;
        private set => SetProperty(ref _tasksCollection, value);
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
        if (FilterTagsCollection.Select(s => s.Id).Contains(tag.Id)) FilterTagsCollection.Remove(tag);
    });

    public ICommand AddTagToFilterCommand => _addTagToFilterCommand ??=
        new DelegateCommand<Tag>(tag => { FilterTagsCollection.Add(tag); },
                tag => tag != null && !FilterTagsCollection.Select(s => s.Id).Contains(tag.Id))
            .ObservesProperty(() => FilterTagsCollection.Count);

    public ICommand ClearTagsCommand => _clearTagsCommand ??=
        new DelegateCommand(() => { FilterTagsCollection.Clear(); }, () => FilterTagsCollection.Any()).ObservesProperty(
            () => FilterTagsCollection.Count);

    public ICommand OpenTaskTagsWindowCommand => _openTaskTagsWindowCommand ??= new DelegateCommand<Task>(task =>
    {
        // TODO: Сделать открытие нового окна с редактированием списка тегов задачи.
    });

    private async void LoadTagsAsync(TaskManageDbContext taskManageContext)
    {
        var list = await taskManageContext.Tags.ToListAsync();
        TagsCollection = new ObservableCollection<Tag>(list);
    }

    private async void FilterTasks(string text, IList<Tag> tags, DateTime? date)
    {
        if (!await _taskManageContext.Tasks.AnyAsync())
            TasksCollection = new ObservableCollection<Task>();

        IQueryable<Task> list = _taskManageContext.Tasks.Include(s => s.Tags);

        if (!string.IsNullOrWhiteSpace(text))
            list = list.Where(s => s.Name.Trim().ToLower().Contains(text.Trim().ToLower()));

        if (tags.Any())
            list = list.Where(s =>
                s.Tags.Any(x =>
                    tags.Select(c => c.Id)
                        .Contains(x.Id)));

        if (date != null) list = list.Where(s => s.DueDate.Equals(date.Value));

        TasksCollection = new ObservableCollection<Task>(await list.ToListAsync());
    }
}