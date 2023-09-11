using TaskManager.ViewModels;

namespace TaskManager;

public class ViewModelLocator
{
    public MainViewModel MainViewModel => IoC.Resolve<MainViewModel>();
}
