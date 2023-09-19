using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using TaskManager.Services.Interface;

namespace TaskManager.Services.Implementation;

public class PageService : IPageService
{
    private readonly Stack<Page> _history = new();
    public bool CanGoBack => _history.Skip(1).Any();

    public event Action<Page>? OnPageChanged;

    public void Navigate(Page page)
    {
        OnPageChanged?.Invoke(page);
        _history.Push(page);
    }

    public void GoBack()
    {
        if (!CanGoBack) return;

        _history.Pop();
        OnPageChanged?.Invoke(_history.Peek());
    }
}