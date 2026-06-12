using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dreamine.UI.Wpf.Behaviors
{
	/// <summary>
	/// \brief 숫자 범위 위반 시 처리 모드.
	/// </summary>
	public enum NumericRangeMode
	{
		/// <summary>\brief 범위 밖이면 자동 보정(Clamp) </summary>
		Clamp,
		/// <summary>\brief 범위 밖이면 마지막 유효값으로 되돌림(Reject) </summary>
		Reject
	}

	/// <summary>
	/// \brief TextBox에 부착해 숫자 범위를 강제하는 Attached Behavior.
	/// \details ViewModel/Model 수정 없이 UX 레벨에서 안전한 입력을 보장합니다.
	/// </summary>
	public static class NumericRangeBehavior
	{
		/// <summary>\brief 동작 활성화 여부 (기본: false) </summary>
		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.RegisterAttached(
				"IsEnabled",
				typeof(bool),
				typeof(NumericRangeBehavior),
				new PropertyMetadata(false, OnIsEnabledChanged));

		/// <summary>\brief 최소값(미설정 시 제한 없음) </summary>
		public static readonly DependencyProperty MinProperty =
			DependencyProperty.RegisterAttached(
				"Min",
				typeof(double?),
				typeof(NumericRangeBehavior),
				new PropertyMetadata(null));

		/// <summary>\brief 최대값(미설정 시 제한 없음) </summary>
		public static readonly DependencyProperty MaxProperty =
			DependencyProperty.RegisterAttached(
				"Max",
				typeof(double?),
				typeof(NumericRangeBehavior),
				new PropertyMetadata(null));

		/// <summary>\brief 범위 밖 처리 모드(Clamp/Reject) </summary>
		public static readonly DependencyProperty ModeProperty =
			DependencyProperty.RegisterAttached(
				"Mode",
				typeof(NumericRangeMode),
				typeof(NumericRangeBehavior),
				new PropertyMetadata(NumericRangeMode.Clamp));

		/// <summary>\brief 마지막 유효 텍스트 내부 저장용(DP) </summary>
		private static readonly DependencyProperty LastValidTextProperty =
			DependencyProperty.RegisterAttached(
				"LastValidText",
				typeof(string),
				typeof(NumericRangeBehavior),
				new PropertyMetadata(null));

		/// <summary>\brief Getter/Setter </summary>
		public static void SetIsEnabled(DependencyObject d, bool value) => d.SetValue(IsEnabledProperty, value);
		/// <summary>\brief Getter/Setter </summary>
		public static bool GetIsEnabled(DependencyObject d) => (bool)d.GetValue(IsEnabledProperty);

		/// <summary>\brief Getter/Setter </summary>
		public static void SetMin(DependencyObject d, double? value) => d.SetValue(MinProperty, value);
		/// <summary>\brief Getter/Setter </summary>
		public static double? GetMin(DependencyObject d) => (double?)d.GetValue(MinProperty);

		/// <summary>\brief Getter/Setter </summary>
		public static void SetMax(DependencyObject d, double? value) => d.SetValue(MaxProperty, value);
		/// <summary>\brief Getter/Setter </summary>
		public static double? GetMax(DependencyObject d) => (double?)d.GetValue(MaxProperty);

		/// <summary>\brief Getter/Setter </summary>
		public static void SetMode(DependencyObject d, NumericRangeMode value) => d.SetValue(ModeProperty, value);
		/// <summary>\brief Getter/Setter </summary>
		public static NumericRangeMode GetMode(DependencyObject d) => (NumericRangeMode)d.GetValue(ModeProperty);

		private static void SetLastValidText(DependencyObject d, string value) => d.SetValue(LastValidTextProperty, value);
		private static string GetLastValidText(DependencyObject d) => (string)d.GetValue(LastValidTextProperty);

		/// <summary>
		/// \brief IsEnabled 변경 시 핸들러 연결/해제.
		/// </summary>
		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is not TextBox tb) return;

			if ((bool)e.NewValue)
			{
				tb.Loaded += Tb_Loaded;
				tb.TextChanged += Tb_TextChanged;
				tb.LostFocus += Tb_LostFocus;
				DataObject.AddPastingHandler(tb, OnPasting);
			}
			else
			{
				tb.Loaded -= Tb_Loaded;
				tb.TextChanged -= Tb_TextChanged;
				tb.LostFocus -= Tb_LostFocus;
				DataObject.RemovePastingHandler(tb, OnPasting);
			}
		}

		/// <summary>\brief 초기 로드시 현재 텍스트를 유효값으로 저장. </summary>
		private static void Tb_Loaded(object sender, RoutedEventArgs e)
		{
			if (sender is TextBox tb)
				SetLastValidText(tb, tb.Text);
		}

		/// <summary>
		/// \brief 붙여넣기 시에도 검증/보정 수행.
		/// </summary>
		private static void OnPasting(object sender, DataObjectPastingEventArgs e)
		{
			if (sender is not TextBox tb) return;

			if (e.DataObject.GetDataPresent(DataFormats.UnicodeText))
			{
				var pasteText = e.DataObject.GetData(DataFormats.UnicodeText) as string ?? string.Empty;
				var future = GetFutureText(tb, pasteText);
				if (!TryValidate(tb, future, out var corrected))
				{
					if (GetMode(tb) == NumericRangeMode.Reject)
					{
						e.CancelCommand();
						tb.Text = GetLastValidText(tb) ?? string.Empty;
						tb.CaretIndex = tb.Text.Length;
						return;
					}
					e.CancelCommand(); // Clamp
					tb.Text = corrected;
					tb.CaretIndex = tb.Text.Length;
				}
			}
		}

		/// <summary>
		/// \brief 입력 중 보정(Clamp 모드일 때만 실시간 보정).
		/// \details Reject는 LostFocus에서 되돌리는 것이 UX가 부드럽습니다.
		/// </summary>
		private static void Tb_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (sender is not TextBox tb) return;

			if (GetMode(tb) == NumericRangeMode.Clamp)
			{
				if (!TryValidate(tb, tb.Text, out var corrected))
				{
					tb.TextChanged -= Tb_TextChanged;
					tb.Text = corrected;
					tb.CaretIndex = tb.Text.Length;
					tb.TextChanged += Tb_TextChanged;
				}
				else
				{
					SetLastValidText(tb, tb.Text);
				}
			}
		}

		/// <summary>
		/// \brief 포커스 아웃 시 최종 검증: Clamp 또는 Reject 반영, 그리고 바인딩 원본 업데이트.
		/// </summary>
		private static void Tb_LostFocus(object sender, RoutedEventArgs e)
		{
			if (sender is not TextBox tb) return;

			var mode = GetMode(tb);
			if (!TryValidate(tb, tb.Text, out var corrected))
			{
				tb.Text = (mode == NumericRangeMode.Reject)
					? (GetLastValidText(tb) ?? corrected)
					: corrected;
			}
			SetLastValidText(tb, tb.Text);

			// 바인딩 원본 반영
			var expr = tb.GetBindingExpression(TextBox.TextProperty);
			expr?.UpdateSource();
		}

		/// <summary>\brief 현재 텍스트에 삽입했을 때 미래 텍스트 계산. </summary>
		private static string GetFutureText(TextBox tb, string insert)
		{
			var text = tb.Text ?? string.Empty;
			var selStart = tb.SelectionStart;
			var selLen = tb.SelectionLength;
			if (selLen > 0) text = text.Remove(selStart, selLen);
			return text.Insert(selStart, insert);
		}

		/// <summary>
		/// \brief 텍스트 → Double 파싱 후 Min/Max 검증 및 보정 문자열 산출.
		/// \Param corrected 보정 결과 문자열(Clamp 시 사용)
		/// \return 유효하면 true, 아니면 false
		/// </summary>
		private static bool TryValidate(TextBox tb, string text, out string corrected)
		{
			corrected = text ?? string.Empty;
			if (string.IsNullOrWhiteSpace(text))
				return true; // 빈값 허용 여부는 별도 정책에서 결정

			var style = NumberStyles.Float | NumberStyles.AllowThousands;
			var culture = CultureInfo.CurrentCulture;

			if (!double.TryParse(text, style, culture, out var value))
			{
				return false; // 숫자 아님
			}

			var min = GetMin(tb);
			var max = GetMax(tb);

			if (min.HasValue && value < min.Value)
			{
				corrected = min.Value.ToString(culture);
				return false;
			}
			if (max.HasValue && value > max.Value)
			{
				corrected = max.Value.ToString(culture);
				return false;
			}

			return true;
		}
	}
}
