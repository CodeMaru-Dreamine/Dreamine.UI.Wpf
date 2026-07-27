using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Dreamine.UI.Wpf.Behaviors;

/// <summary>
/// \if KO
/// <para><see cref="ListBox"/>가 최신 항목을 표시하도록 자동 스크롤하는 연결 동작을 제공합니다.</para>
/// \endif
/// \if EN
/// <para>Provides an attached behavior that automatically scrolls a <see cref="ListBox"/> to its latest item.</para>
/// \endif
/// </summary>
/// <remarks>
/// \if KO
/// <para>활성화하면 <see cref="INotifyCollectionChanged"/>를 구현하는 항목 소스의 변경을 구독합니다.</para>
/// \endif
/// \if EN
/// <para>When enabled, the behavior observes changes from an items source that implements <see cref="INotifyCollectionChanged"/>.</para>
/// \endif
/// </remarks>
public static class AutoScrollListBoxBehavior
{
	/// <summary>
	/// \if KO
	/// <para>자동 스크롤 활성화 여부를 저장하는 연결 속성입니다.</para>
	/// \endif
	/// \if EN
	/// <para>Identifies the attached property that stores whether automatic scrolling is enabled.</para>
	/// \endif
	/// </summary>
	public static readonly DependencyProperty IsEnabledProperty =
		DependencyProperty.RegisterAttached(
			"IsEnabled",
			typeof(bool),
			typeof(AutoScrollListBoxBehavior),
			new PropertyMetadata(false, OnIsEnabledChanged));

	/// <summary>
	/// \if KO
	/// <para>대상 종속성 개체의 자동 스크롤 활성화 여부를 설정합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Sets whether automatic scrolling is enabled for the target dependency object.</para>
	/// \endif
	/// </summary>
	/// <param name="element">
	/// \if KO
	/// <para>값을 설정할 종속성 개체입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The dependency object on which to set the value.</para>
	/// \endif
	/// </param>
	/// <param name="value">
	/// \if KO
	/// <para>활성화하려면 <see langword="true"/>입니다.</para>
	/// \endif
	/// \if EN
	/// <para><see langword="true"/> to enable the behavior.</para>
	/// \endif
	/// </param>
	/// <exception cref="ArgumentNullException">
	/// \if KO
	/// <para><paramref name="element"/>가 <see langword="null"/>인 경우 발생합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Thrown when <paramref name="element"/> is <see langword="null"/>.</para>
	/// \endif
	/// </exception>
	public static void SetIsEnabled(DependencyObject element, bool value)
		=> element.SetValue(IsEnabledProperty, value);

	/// <summary>
	/// \if KO
	/// <para>대상 종속성 개체에서 자동 스크롤 활성화 여부를 가져옵니다.</para>
	/// \endif
	/// \if EN
	/// <para>Gets whether automatic scrolling is enabled for the target dependency object.</para>
	/// \endif
	/// </summary>
	/// <param name="element">
	/// \if KO
	/// <para>값을 읽을 종속성 개체입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The dependency object from which to read the value.</para>
	/// \endif
	/// </param>
	/// <returns>
	/// \if KO
	/// <para>활성화되어 있으면 <see langword="true"/>이고, 그렇지 않으면 <see langword="false"/>입니다.</para>
	/// \endif
	/// \if EN
	/// <para><see langword="true"/> when enabled; otherwise, <see langword="false"/>.</para>
	/// \endif
	/// </returns>
	/// <exception cref="ArgumentNullException">
	/// \if KO
	/// <para><paramref name="element"/>가 <see langword="null"/>인 경우 발생합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Thrown when <paramref name="element"/> is <see langword="null"/>.</para>
	/// \endif
	/// </exception>
	public static bool GetIsEnabled(DependencyObject element)
		=> (bool)element.GetValue(IsEnabledProperty);

	/// <summary>
	/// \if KO
	/// <para>구독 해제에 필요한 컬렉션 변경 처리기를 보관하는 내부 연결 속성입니다.</para>
	/// \endif
	/// \if EN
	/// <para>Identifies the internal attached property that retains the collection-change handler for later unsubscription.</para>
	/// \endif
	/// </summary>
	private static readonly DependencyProperty SubscriptionProperty =
		DependencyProperty.RegisterAttached(
			"Subscription",
			typeof(NotifyCollectionChangedEventHandler),
			typeof(AutoScrollListBoxBehavior),
			new PropertyMetadata(null));

	/// <summary>
	/// \if KO
	/// <para>자동 스크롤 활성화 값 변경을 처리하여 수명 주기 이벤트를 연결하거나 해제합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Handles changes to the enabled value by attaching or detaching lifecycle events.</para>
	/// \endif
	/// </summary>
	/// <param name="d">
	/// \if KO
	/// <para>값이 변경된 종속성 개체입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The dependency object whose value changed.</para>
	/// \endif
	/// </param>
	/// <param name="e">
	/// \if KO
	/// <para>종속성 속성 변경 데이터입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The dependency-property change data.</para>
	/// \endif
	/// </param>
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
	/// \if KO
	/// <para>목록이 로드되면 항목 소스 변경을 구독하고 마지막 항목으로 이동합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Subscribes to items-source changes and scrolls to the last item when the list is loaded.</para>
	/// \endif
	/// </summary>
	/// <param name="sender">
	/// \if KO
	/// <para>로드된 목록입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The list that was loaded.</para>
	/// \endif
	/// </param>
	/// <param name="e">
	/// \if KO
	/// <para>라우트 이벤트 데이터입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The routed-event data.</para>
	/// \endif
	/// </param>
	private static void OnLoaded(object sender, RoutedEventArgs e)
	{
		if (sender is not ListBox listBox)
			return;

		Attach(listBox);
		ScrollToLast(listBox);
	}

	/// <summary>
	/// \if KO
	/// <para>목록이 언로드되면 항목 소스 변경 구독을 해제합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Detaches the items-source subscription when the list is unloaded.</para>
	/// \endif
	/// </summary>
	/// <param name="sender">
	/// \if KO
	/// <para>언로드된 목록입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The list that was unloaded.</para>
	/// \endif
	/// </param>
	/// <param name="e">
	/// \if KO
	/// <para>라우트 이벤트 데이터입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The routed-event data.</para>
	/// \endif
	/// </param>
	private static void OnUnloaded(object sender, RoutedEventArgs e)
	{
		if (sender is not ListBox listBox)
			return;

		Detach(listBox);
	}

	/// <summary>
	/// \if KO
	/// <para>목록의 항목 소스가 지원하는 컬렉션 변경 이벤트를 구독합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Subscribes to collection-change events exposed by the list's items source.</para>
	/// \endif
	/// </summary>
	/// <param name="listBox">
	/// \if KO
	/// <para>구독을 연결할 목록입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The list box whose source is observed.</para>
	/// \endif
	/// </param>
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
	/// \if KO
	/// <para>목록에 저장된 이전 컬렉션 변경 구독을 해제합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Detaches any previous collection-change subscription stored on the list.</para>
	/// \endif
	/// </summary>
	/// <param name="listBox">
	/// \if KO
	/// <para>구독을 해제할 목록입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The list box from which to detach the subscription.</para>
	/// \endif
	/// </param>
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
	/// \if KO
	/// <para>항목이 있으면 목록의 마지막 항목이 보이도록 스크롤합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Scrolls the list so its final item is visible when at least one item exists.</para>
	/// \endif
	/// </summary>
	/// <param name="listBox">
	/// \if KO
	/// <para>스크롤할 목록입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The list box to scroll.</para>
	/// \endif
	/// </param>
	private static void ScrollToLast(ListBox listBox)
	{
		if (listBox.Items.Count <= 0)
			return;

		var last = listBox.Items[listBox.Items.Count - 1];
		listBox.ScrollIntoView(last);
	}
}
