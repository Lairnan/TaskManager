using TaskManager.ViewModels;
using TaskManager.ViewModels.Pages;

namespace TaskManager;

public class ViewModelLocator
{
    public MainViewModel MainViewModel => IoC.Resolve<MainViewModel>();
    public ViewTasksViewModel ViewTasksViewModel => IoC.Resolve<ViewTasksViewModel>();
}