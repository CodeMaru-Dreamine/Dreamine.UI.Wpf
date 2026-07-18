using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dreamine.UI.Wpf.Behaviors
{
	/// <summary>
	/// \if KO
	/// <para><see cref="DataGrid"/>에 적용할 수 있는 연결 속성과 동작을 제공합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Provides attached properties and behaviors for <see cref="DataGrid"/> controls.</para>
	/// \endif
	/// </summary>
	public static class DataGridBehaviors
	{
		/// <summary>
		/// \if KO
		/// <para>선택된 행을 다시 클릭할 때 선택을 해제하는 기능의 연결 속성입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Identifies the attached property that enables deselection when the selected row is clicked again.</para>
		/// \endif
		/// </summary>
		public static readonly DependencyProperty EnableClickToDeselectProperty =
			DependencyProperty.RegisterAttached(
				"EnableClickToDeselect",
				typeof(bool),
				typeof(DataGridBehaviors),
				new PropertyMetadata(false, OnEnableClickToDeselectChanged));

		/// <summary>
		/// \if KO
		/// <para>클릭하여 선택 해제하는 기능의 활성화 여부를 가져옵니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets whether click-to-deselect behavior is enabled.</para>
		/// \endif
		/// </summary>
		/// <param name="obj">
		/// \if KO
		/// <para>값을 읽을 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object from which to read the value.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>기능이 활성화되어 있으면 <see langword="true"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see langword="true"/> when the behavior is enabled.</para>
		/// \endif
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// \if KO
		/// <para><paramref name="obj"/>가 <see langword="null"/>인 경우 발생합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Thrown when <paramref name="obj"/> is <see langword="null"/>.</para>
		/// \endif
		/// </exception>
		public static bool GetEnableClickToDeselect(DependencyObject obj)
			=> (bool)obj.GetValue(EnableClickToDeselectProperty);

		/// <summary>
		/// \if KO
		/// <para>클릭하여 선택 해제하는 기능의 활성화 여부를 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Sets whether click-to-deselect behavior is enabled.</para>
		/// \endif
		/// </summary>
		/// <param name="obj">
		/// \if KO
		/// <para>값을 설정할 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object on which to set the value.</para>
		/// \endif
		/// </param>
		/// <param name="value">
		/// \if KO
		/// <para>기능을 활성화하려면 <see langword="true"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see langword="true"/> to enable the behavior.</para>
		/// \endif
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// \if KO
		/// <para><paramref name="obj"/>가 <see langword="null"/>인 경우 발생합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Thrown when <paramref name="obj"/> is <see langword="null"/>.</para>
		/// \endif
		/// </exception>
		public static void SetEnableClickToDeselect(DependencyObject obj, bool value)
			=> obj.SetValue(EnableClickToDeselectProperty, value);

		/// <summary>
		/// \if KO
		/// <para>연결 속성 값 변경을 처리하여 마우스 이벤트 구독을 갱신합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Handles an attached-property change by updating the mouse-event subscription.</para>
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
		private static void OnEnableClickToDeselectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is not DataGrid dg) return;

			if ((bool)e.NewValue)
			{
				dg.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
				dg.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
			}
			else
			{
				dg.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
			}
		}

		/// <summary>
		/// \if KO
		/// <para>이미 선택된 행에서 발생한 마우스 왼쪽 버튼 입력을 처리하여 해당 행의 선택을 해제합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Handles a left mouse button press on an already selected row by clearing that row's selection.</para>
		/// \endif
		/// </summary>
		/// <param name="sender">
		/// \if KO
		/// <para>이벤트를 발생시킨 데이터 그리드입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The data grid that raised the event.</para>
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
		private static void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is not DataGrid dg) return;

			// HitTest로 DataGridRow 찾기
			var dep = e.OriginalSource as DependencyObject;
			while (dep != null && dep is not DataGridRow)
				dep = VisualTreeHelper.GetParent(dep);

			if (dep is not DataGridRow row)
				return;

			// 이미 선택된 행을 다시 클릭한 경우 → 선택 해제
			if (row.IsSelected)
			{
				// 포커스가 셀/행에 있을 때만 동작하도록 처리(원치 않는 해제 방지)
				row.IsSelected = false;
				e.Handled = true; // 기본 선택 동작 막기 → RowDetails 닫힘
			}
			// 아니면 기본 동작(선택) 수행 → RowDetails 열림(VisibleWhenSelected)
		}
	}
}
