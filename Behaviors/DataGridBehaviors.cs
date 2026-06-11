using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dreamine.UI.Wpf.Behaviors
{
	/*!
	* \class DataGridBehaviors
	* \brief DataGrid용 Attached Property/Behavior 모음
	*/
	public static class DataGridBehaviors
	{
		/*! \brief 선택 행을 다시 클릭하면 선택 해제(=RowDetails 접힘) */
		public static readonly DependencyProperty EnableClickToDeselectProperty =
			DependencyProperty.RegisterAttached(
				"EnableClickToDeselect",
				typeof(bool),
				typeof(DataGridBehaviors),
				new PropertyMetadata(false, OnEnableClickToDeselectChanged));

		/*!
         * \brief Getter
         */
		public static bool GetEnableClickToDeselect(DependencyObject obj)
			=> (bool)obj.GetValue(EnableClickToDeselectProperty);

		/*!
         * \brief Setter
         */
		public static void SetEnableClickToDeselect(DependencyObject obj, bool value)
			=> obj.SetValue(EnableClickToDeselectProperty, value);

		/*!
         * \brief 속성 변경 콜백
         */
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

		/*!
         * \brief 마우스 왼쪽 버튼 다운 시, 이미 선택된 행을 다시 클릭하면 선택 해제
         */
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
