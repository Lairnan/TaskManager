using TaskManager.ViewModels;
using TaskManager.ViewModels.Pages;

namespace TaskManager;

public class ViewModelLocator
{
    // Windows
    public MainViewModel MainViewModel => IoC.Resolve<MainViewModel>();
    public AdditionalViewModel AdditionalViewModel => IoC.Resolve<AdditionalViewModel>();
    
    // Pages
    public ViewTasksViewModel ViewTasksViewModel => IoC.Resolve<ViewTasksViewModel>();
}