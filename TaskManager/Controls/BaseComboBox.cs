using System.Windows;
using System.Windows.Controls;

namespace TaskManager.Controls;

public class BaseComboBox : ComboBox
{
    public BaseComboBox()
    {
        SelectionChanged += (_, _) =>
        {
            this.SelectionBoxItem = this.SelectedItem;
        };
    }
    
    private static readonly DependencyPropertyKey selectionBoxItemPropertyKey =
        DependencyProperty.RegisterReadOnly("SelectionBoxItem", typeof(object), typeof(BaseComboBox),
            new FrameworkPropertyMetadata(string.Empty));
    
    public new static readonly DependencyProperty SelectionBoxItemProperty = selectionBoxItemPropertyKey.DependencyProperty;

    public new object SelectionBoxItem
    {
        get => GetValue(SelectionBoxItemProperty);
        private protected set => SetValue(selectionBoxItemPropertyKey, value);
    }
}