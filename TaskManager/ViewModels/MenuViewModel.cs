using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Prism.Commands;
using Prism.Mvvm;

namespace TaskManager.ViewModels;

public class MenuViewModel : BindableBase
{

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

    public ICommand? CloseCommand { get; }
    public ICommand? MinimizeCommand { get; }
    public ICommand? SwitchStateCommand { get; }
    public ICommand? MouseLeftButtonDownCommand { get; }
    public ICommand? MouseEnterCommand { get; }

    private readonly Window _window;

    private string _windowCurrentState = "1";
    public string WindowCurrentState
    {
        get => _windowCurrentState;
        private set => SetProperty(ref _windowCurrentState, value);
    }

    public MenuViewModel(Window window)
    {
        _window = window;
        
        CloseCommand = new DelegateCommand(() => _window.Close());
        MinimizeCommand = new DelegateCommand(() => _window.WindowState = WindowState.Minimized);
        SwitchStateCommand = new DelegateCommand(SwitchWindowState);
        MouseLeftButtonDownCommand = new DelegateCommand<MouseButtonEventArgs>(OnMouseLeftButtonDown);
        MouseEnterCommand = new DelegateCommand(OnMouseEnter);
    }

    private void SwitchWindowState()
    {
        _window.WindowState = _window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        WindowCurrentState = _window.WindowState == WindowState.Maximized ? "2" : "1";
    }

    private void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            SwitchWindowState();
            return;
        }
        
        var helper = new WindowInteropHelper(_window);
        SendMessage(helper.Handle, 161, 2, 0);
        WindowCurrentState = _window.WindowState == WindowState.Maximized ? "2" : "1";
    }

    private void OnMouseEnter()
    {
        _window.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
    }
}