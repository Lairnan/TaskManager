using System.Windows;
using System.Windows.Controls;
using TaskManager.ViewModels;

namespace TaskManager.Controls;

public class MenuBarPanel : Control
{
    static MenuBarPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuBarPanel), new FrameworkPropertyMetadata(typeof(MenuBarPanel)));
    }

    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(object), typeof(MenuBarPanel), new PropertyMetadata(null));

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(MenuBarPanel), new PropertyMetadata(null));

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        Loaded += MenuBarPanel_Loaded;
    }

    private static void MenuBarPanel_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is MenuBarPanel panel)
        {
            panel.DataContext = new MenuViewModel(Window.GetWindow(panel)!);
        }
    }
}