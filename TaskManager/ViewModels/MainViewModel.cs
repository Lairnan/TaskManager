using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TaskManager.Models;
using TaskManager.Services.Interface;
using TaskManager.Views.Pages;

namespace TaskManager.ViewModels;

public class MainViewModel : BindableBase
{
    private readonly TaskManageDbContext _taskManageContext;
    private readonly IPageService _pageService;

    private Page _currentPage = new();
    private string _title = "Главное окно";

    private ICommand? _showSettingsCommand;
    private ICommand? _goToBackCommand;

    public MainViewModel(TaskManageDbContext taskManageContext, IPageService pageService)
    {
        _taskManageContext = taskManageContext;
        _pageService = pageService;
        _pageService.OnPageChanged += NavigateAction;
        
        _pageService.Navigate(new ViewTasksPage());

        NavigationCommands.BrowseBack.InputGestures.Clear();
        NavigationCommands.BrowseForward.InputGestures.Clear();
        NavigationCommands.BrowseHome.InputGestures.Clear();
        NavigationCommands.BrowseStop.InputGestures.Clear();
    }
    
    private async void NavigateAction(Page page)
    {
        Title = page.Title;
        
        await Task.Delay(350);
        CurrentPage = page;
        await Task.Delay(25);
    }
    
    public Page CurrentPage
    {
        get => _currentPage;
        private set => SetProperty(ref _currentPage, value);
    }
    public string Title
    {
        get => _title;
        private set => SetProperty(ref _title, value);
    }

    public ICommand ShowSettingsCommand => _showSettingsCommand ??= new DelegateCommand(() =>
    {
        MessageBox.Show("Work In Progress");
    });

    public ICommand GoToBackCommand => _goToBackCommand ??= new DelegateCommand(() =>
    {
        _pageService.GoBack();
    }, () => _pageService.CanGoBack);
}