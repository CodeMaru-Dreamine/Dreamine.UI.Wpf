using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace Dreamine.UI.Wpf.Behaviors;

public class WindowDragBehavior : Behavior<FrameworkElement>
{
    public bool IsDragEnabled { get; set; } = true;

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
        base.OnDetaching();
    }

    private void OnMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (!IsDragEnabled) return;
        var window = Window.GetWindow(AssociatedObject);
        window?.DragMove();
    }
}
