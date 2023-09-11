using System.Linq;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TaskManager.Models;

namespace TaskManager.ViewModels;

public class MainViewModel : BindableBase, ISingleton
{
    private readonly TaskManageContext _taskManageContext;

    public ICommand? ShowSettingsCommand { get; }
    public ICommand? TestCommand { get; }
    
    public MainViewModel(TaskManageContext taskManageContext)
    {
        _taskManageContext = taskManageContext;

        ShowSettingsCommand = new DelegateCommand(ShowSetting);
        TestCommand = new DelegateCommand(Test);
        //TODO: Команды не работают, доделать
    }

    private void ShowSetting()
    {
        MessageBox.Show("Work In Progress");
    }

    private async void Test()
    {
        if (_taskManageContext.Tags.Any())
            MessageBox.Show(_taskManageContext.Tags.First().Name);
        else
        {
            var tag = new Models.Entities.Tag { Name = "New Tag" };
            await _taskManageContext.AddAsync(tag);
            await _taskManageContext.SaveChangesAsync();
            MessageBox.Show(_taskManageContext.Tags.First().Name);
        }
    }
}