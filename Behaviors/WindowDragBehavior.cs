using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace Dreamine.UI.Wpf.Behaviors;

/// <summary>
/// \if KO
/// <para>연결된 요소에서 마우스 왼쪽 버튼을 누르면 해당 요소를 포함하는 창을 끌 수 있게 합니다.</para>
/// \endif
/// \if EN
/// <para>Enables dragging the containing window when the left mouse button is pressed on the associated element.</para>
/// \endif
/// </summary>
public class WindowDragBehavior : Behavior<FrameworkElement>
{
    /// <summary>
    /// \if KO
    /// <para>창 끌기 동작을 사용할지 여부를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets whether window dragging is enabled.</para>
    /// \endif
    /// </summary>
    public bool IsDragEnabled { get; set; } = true;

    /// <summary>
    /// \if KO
    /// <para>동작이 요소에 연결될 때 마우스 입력 이벤트를 구독합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Subscribes to mouse input when the behavior is attached to an element.</para>
    /// \endif
    /// </summary>
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
    }

    /// <summary>
    /// \if KO
    /// <para>동작이 요소에서 분리될 때 마우스 입력 이벤트 구독을 해제합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Unsubscribes from mouse input when the behavior is detached from an element.</para>
    /// \endif
    /// </summary>
    protected override void OnDetaching()
    {
        AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
        base.OnDetaching();
    }

    /// <summary>
    /// \if KO
    /// <para>마우스 왼쪽 버튼 입력을 처리하여 연결된 요소의 상위 창을 끕니다.</para>
    /// \endif
    /// \if EN
    /// <para>Handles a left mouse button press by dragging the window that contains the associated element.</para>
    /// \endif
    /// </summary>
    /// <param name="sender">
    /// \if KO
    /// <para>이벤트를 발생시킨 요소입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The element that raised the event.</para>
    /// \endif
    /// </param>
    /// <param name="e">
    /// \if KO
    /// <para>마우스 버튼 이벤트 데이터입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The mouse button event data.</para>
    /// \endif
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// \if KO
    /// <para>마우스 왼쪽 버튼이 눌린 상태가 아니거나 창을 끌 수 없는 경우 <see cref="Window.DragMove"/>에서 발생할 수 있습니다.</para>
    /// \endif
    /// \if EN
    /// <para>May be thrown by <see cref="Window.DragMove"/> when the left mouse button is not pressed or the window cannot be dragged.</para>
    /// \endif
    /// </exception>
    private void OnMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (!IsDragEnabled) return;
        var window = Window.GetWindow(AssociatedObject);
        window?.DragMove();
    }
}
