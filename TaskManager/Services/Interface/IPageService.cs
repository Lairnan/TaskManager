using System;
using System.Windows.Controls;

namespace TaskManager.Services.Interface;

public interface IPageService
{
    bool CanGoBack { get; }
    event Action<Page> OnPageChanged;

    void Navigate(Page page);
    void GoBack();
}