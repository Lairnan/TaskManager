using System.Linq;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TaskManager.Models;

namespace TaskManager.ViewModels;

public class MainViewModel : BindableBase, ITransient
{
    private readonly TaskManageContext _taskManageContext;

    public ICommand ShowSettingsCommand => new DelegateCommand(ShowSetting);
    
    public MainViewModel(TaskManageContext taskManageContext)
    {
        _taskManageContext = taskManageContext;
    }

    private void ShowSetting()
    {
        MessageBox.Show("Work In Progress");
    }
}