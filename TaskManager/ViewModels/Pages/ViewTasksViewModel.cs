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
    private DelegateCommand? _mouseLeftButtonCommand;

    public ViewTasksViewModel(TaskManageDbContext taskManageContext, IPageService pageService)
    {
        _taskManageContext = taskManageContext;
        _pageService = pageService;
        
        FilterTasks(FilterText);
    }

    public string FilterText
    {
        get => _filterText;
        set => SetProperty(ref _filterText, value, () => FilterTasks(value));
    }

    public ObservableCollection<Task> TasksCollection
    {
        get => _tasksCollection;
        private set => SetProperty(ref _tasksCollection, value);
    }

    private async void FilterTasks(string value)
    {
        if (await _taskManageContext.Tasks.AnyAsync())
            TasksCollection = new ObservableCollection<Task>(await _taskManageContext.Tasks
                .Include(s => s.Tags)
                .Where(s => s.Name.Trim().ToLower().Contains(value.Trim().ToLower()))
                .ToListAsync());
        else TasksCollection = new ObservableCollection<Task>();
    }

    public Task? SelectedTask
    {
        get => _selectedTask;
        set => SetProperty(ref _selectedTask, value);
    }

    public ICommand MouseLeftButtonCommand => _mouseLeftButtonCommand ??= new DelegateCommand(() =>
    {
        if (_selectedTask != null && _selectedTask.Equals(_lastSelectedTask))
        {
            SelectedTask = null;
            _lastSelectedTask = null;
        }
        else _lastSelectedTask = _selectedTask;
    });
}