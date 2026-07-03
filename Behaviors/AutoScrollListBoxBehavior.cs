using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Dreamine.UI.Wpf.Behaviors;

/// <summary>
/// \file AutoScrollListBoxBehavior.cs
/// \brief Attached behavior that keeps a ListBox scrolled to the latest item.
/// Details:
///  - When enabled, it listens to ItemsSource changes and automatically scrolls to the last item.
///  - Works with ObservableCollection via INotifyCollectionChanged.
///  - This behavior is UI-only and does not require code-behind usage.
/// </summary>
public static class AutoScrollListBoxBehavior
{
	/// <summary>
	/// \brief Attached property to enable/disable auto-scroll.
	/// </summary>
	public static readonly DependencyProperty IsEnabledProperty =
		DependencyProperty.RegisterAttached(
			"IsEnabled",
			typeof(bool),
			typeof(AutoScrollListBoxBehavior),
			new PropertyMetadata(false, OnIsEnabledChanged));

	/// <summary>
	/// \brief Sets whether auto-scroll is enabled for the target ListBox.
	/// </summary>
	/// <param name="element">Target dependency object</param>
	/// <param name="value">True to enable</param>
	public static void SetIsEnabled(DependencyObject element, bool value)
		=> element.SetValue(IsEnabledProperty, value);

	/// <summary>
	/// \brief Gets whether auto-scroll is enabled for the target ListBox.
	/// </summary>
	/// <param name="element">Target dependency object</param>
	/// <returns>True if enabled</returns>
	public static bool GetIsEnabled(DependencyObject element)
		=> (bool)element.GetValue(IsEnabledProperty);

	/// <summary>
	/// \brief Attached property for holding the subscription handler reference.
	/// Details:
	///  - Used to properly detach events when behavior is disabled.
	/// </summary>
	private static readonly DependencyProperty SubscriptionProperty =
		DependencyProperty.RegisterAttached(
			"Subscription",
			typeof(NotifyCollectionChangedEventHandler),
			typeof(AutoScrollListBoxBehavior),
			new PropertyMetadata(null));

	/// <summary>
	/// \brief Called when IsEnabled changes.
	/// </summary>
	/// <param name="d">Dependency object</param>
	/// <param name="e">Change event args</param>
	private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is not ListBox listBox)
			return;

		if ((bool)e.NewValue)
		{
			listBox.Loaded += OnLoaded;
			listBox.Unloaded += OnUnloaded;
		}
		else
		{
			listBox.Loaded -= OnLoaded;
			listBox.Unloaded -= OnUnloaded;
			Detach(listBox);
		}
	}

	/// <summary>
	/// \brief Loaded handler to attach to ItemsSource changes.
	/// </summary>
	private static void OnLoaded(object sender, RoutedEventArgs e)
	{
		if (sender is not ListBox listBox)
			return;

		Attach(listBox);
		ScrollToLast(listBox);
	}

	/// <summary>
	/// \brief Unloaded handler to detach events.
	/// </summary>
	private static void OnUnloaded(object sender, RoutedEventArgs e)
	{
		if (sender is not ListBox listBox)
			return;

		Detach(listBox);
	}

	/// <summary>
	/// \brief Attaches collection changed event to ItemsSource.
	/// </summary>
	private static void Attach(ListBox listBox)
	{
		Detach(listBox);

		if (listBox.ItemsSource is not INotifyCollectionChanged incc)
			return;

		NotifyCollectionChangedEventHandler handler = (_, __) =>
		{
			listBox.Dispatcher.InvokeAsync(() => ScrollToLast(listBox));
		};

		incc.CollectionChanged += handler;
		listBox.SetValue(SubscriptionProperty, handler);
	}

	/// <summary>
	/// \brief Detaches previous subscription if any.
	/// </summary>
	private static void Detach(ListBox listBox)
	{
		if (listBox.ItemsSource is INotifyCollectionChanged incc &&
			listBox.GetValue(SubscriptionProperty) is NotifyCollectionChangedEventHandler handler)
		{
			incc.CollectionChanged -= handler;
		}

		listBox.ClearValue(SubscriptionProperty);
	}

	/// <summary>
	/// \brief Scrolls to the last item in the ListBox.
	/// </summary>
	private static void ScrollToLast(ListBox listBox)
	{
		if (listBox.Items.Count <= 0)
			return;

		var last = listBox.Items[listBox.Items.Count - 1];
		listBox.ScrollIntoView(last);
	}
}
