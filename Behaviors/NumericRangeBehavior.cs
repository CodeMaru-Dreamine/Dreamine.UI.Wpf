using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dreamine.UI.Wpf.Behaviors
{
	/// <summary>
	/// \if KO
	/// <para>숫자 입력이 허용 범위를 벗어났을 때 적용할 처리 방식을 지정합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Specifies how numeric input outside the permitted range is handled.</para>
	/// \endif
	/// </summary>
	public enum NumericRangeMode
	{
		/// <summary>
		/// \if KO
		/// <para>값을 가장 가까운 경계값으로 보정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Clamps the value to the nearest boundary.</para>
		/// \endif
		/// </summary>
		Clamp,
		/// <summary>
		/// \if KO
		/// <para>입력을 거부하고 마지막 유효값으로 되돌립니다.</para>
		/// \endif
		/// \if EN
		/// <para>Rejects the input and restores the last valid value.</para>
		/// \endif
		/// </summary>
		Reject
	}

	/// <summary>
	/// \if KO
	/// <para><see cref="TextBox"/>에 숫자 최소값과 최대값을 강제하는 연결 동작을 제공합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Provides an attached behavior that enforces numeric minimum and maximum values on a <see cref="TextBox"/>.</para>
	/// \endif
	/// </summary>
	/// <remarks>
	/// \if KO
	/// <para>ViewModel이나 모델을 수정하지 않고 입력 단계에서 값을 보정하거나 거부합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Values are clamped or rejected at the input layer without modifying a view model or model.</para>
	/// \endif
	/// </remarks>
	public static class NumericRangeBehavior
	{
		/// <summary>
		/// \if KO
		/// <para>숫자 범위 동작의 활성화 여부를 저장하는 연결 속성입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Identifies the attached property that stores whether numeric-range behavior is enabled.</para>
		/// \endif
		/// </summary>
		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.RegisterAttached(
				"IsEnabled",
				typeof(bool),
				typeof(NumericRangeBehavior),
				new PropertyMetadata(false, OnIsEnabledChanged));

		/// <summary>
		/// \if KO
		/// <para>선택적인 최소 허용값을 저장하는 연결 속성입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Identifies the attached property that stores the optional minimum value.</para>
		/// \endif
		/// </summary>
		public static readonly DependencyProperty MinProperty =
			DependencyProperty.RegisterAttached(
				"Min",
				typeof(double?),
				typeof(NumericRangeBehavior),
				new PropertyMetadata(null));

		/// <summary>
		/// \if KO
		/// <para>선택적인 최대 허용값을 저장하는 연결 속성입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Identifies the attached property that stores the optional maximum value.</para>
		/// \endif
		/// </summary>
		public static readonly DependencyProperty MaxProperty =
			DependencyProperty.RegisterAttached(
				"Max",
				typeof(double?),
				typeof(NumericRangeBehavior),
				new PropertyMetadata(null));

		/// <summary>
		/// \if KO
		/// <para>범위를 벗어난 입력의 처리 방식을 저장하는 연결 속성입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Identifies the attached property that stores the out-of-range handling mode.</para>
		/// \endif
		/// </summary>
		public static readonly DependencyProperty ModeProperty =
			DependencyProperty.RegisterAttached(
				"Mode",
				typeof(NumericRangeMode),
				typeof(NumericRangeBehavior),
				new PropertyMetadata(NumericRangeMode.Clamp));

		/// <summary>
		/// \if KO
		/// <para>마지막 유효 텍스트를 내부적으로 저장하는 연결 속성입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Identifies the internal attached property that stores the last valid text.</para>
		/// \endif
		/// </summary>
		private static readonly DependencyProperty LastValidTextProperty =
			DependencyProperty.RegisterAttached(
				"LastValidText",
				typeof(string),
				typeof(NumericRangeBehavior),
				new PropertyMetadata(null));

		/// <summary>
		/// \if KO
		/// <para>대상 개체의 숫자 범위 동작 활성화 여부를 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Sets whether numeric-range behavior is enabled on the target object.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>값을 설정할 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object on which to set the value.</para>
		/// \endif
		/// </param>
		/// <param name="value">
		/// \if KO
		/// <para>활성화 여부입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Whether the behavior is enabled.</para>
		/// \endif
		/// </param>
		public static void SetIsEnabled(DependencyObject d, bool value) => d.SetValue(IsEnabledProperty, value);
		/// <summary>
		/// \if KO
		/// <para>대상 개체의 숫자 범위 동작 활성화 여부를 가져옵니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets whether numeric-range behavior is enabled on the target object.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>값을 읽을 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object from which to read the value.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>활성화되어 있으면 <see langword="true"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see langword="true"/> when enabled.</para>
		/// \endif
		/// </returns>
		public static bool GetIsEnabled(DependencyObject d) => (bool)d.GetValue(IsEnabledProperty);

		/// <summary>
		/// \if KO
		/// <para>대상 개체의 최소 허용값을 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Sets the minimum permitted value on the target object.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>값을 설정할 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object on which to set the value.</para>
		/// \endif
		/// </param>
		/// <param name="value">
		/// \if KO
		/// <para>최소값이며, 제한하지 않으려면 <see langword="null"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The minimum value, or <see langword="null"/> for no lower bound.</para>
		/// \endif
		/// </param>
		public static void SetMin(DependencyObject d, double? value) => d.SetValue(MinProperty, value);
		/// <summary>
		/// \if KO
		/// <para>대상 개체의 최소 허용값을 가져옵니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets the minimum permitted value from the target object.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>값을 읽을 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object from which to read the value.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>최소값이며, 제한이 없으면 <see langword="null"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The minimum value, or <see langword="null"/> when unrestricted.</para>
		/// \endif
		/// </returns>
		public static double? GetMin(DependencyObject d) => (double?)d.GetValue(MinProperty);

		/// <summary>
		/// \if KO
		/// <para>대상 개체의 최대 허용값을 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Sets the maximum permitted value on the target object.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>값을 설정할 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object on which to set the value.</para>
		/// \endif
		/// </param>
		/// <param name="value">
		/// \if KO
		/// <para>최대값이며, 제한하지 않으려면 <see langword="null"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The maximum value, or <see langword="null"/> for no upper bound.</para>
		/// \endif
		/// </param>
		public static void SetMax(DependencyObject d, double? value) => d.SetValue(MaxProperty, value);
		/// <summary>
		/// \if KO
		/// <para>대상 개체의 최대 허용값을 가져옵니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets the maximum permitted value from the target object.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>값을 읽을 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object from which to read the value.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>최대값이며, 제한이 없으면 <see langword="null"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The maximum value, or <see langword="null"/> when unrestricted.</para>
		/// \endif
		/// </returns>
		public static double? GetMax(DependencyObject d) => (double?)d.GetValue(MaxProperty);

		/// <summary>
		/// \if KO
		/// <para>대상 개체의 범위 위반 처리 방식을 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Sets the out-of-range handling mode on the target object.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>값을 설정할 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object on which to set the value.</para>
		/// \endif
		/// </param>
		/// <param name="value">
		/// \if KO
		/// <para>적용할 처리 방식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The handling mode to apply.</para>
		/// \endif
		/// </param>
		public static void SetMode(DependencyObject d, NumericRangeMode value) => d.SetValue(ModeProperty, value);
		/// <summary>
		/// \if KO
		/// <para>대상 개체의 범위 위반 처리 방식을 가져옵니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets the out-of-range handling mode from the target object.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>값을 읽을 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object from which to read the value.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>구성된 처리 방식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The configured handling mode.</para>
		/// \endif
		/// </returns>
		public static NumericRangeMode GetMode(DependencyObject d) => (NumericRangeMode)d.GetValue(ModeProperty);

		/// <summary>
		/// \if KO
		/// <para>대상 개체에 마지막 유효 텍스트를 저장합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Stores the last valid text on the target object.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>텍스트를 저장할 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object on which to store the text.</para>
		/// \endif
		/// </param>
		/// <param name="value">
		/// \if KO
		/// <para>저장할 유효 텍스트입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The valid text to store.</para>
		/// \endif
		/// </param>
		private static void SetLastValidText(DependencyObject d, string value) => d.SetValue(LastValidTextProperty, value);
		/// <summary>
		/// \if KO
		/// <para>대상 개체에 저장된 마지막 유효 텍스트를 가져옵니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets the last valid text stored on the target object.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>텍스트를 읽을 종속성 개체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object from which to read the text.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>마지막 유효 텍스트입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The last valid text.</para>
		/// \endif
		/// </returns>
		private static string GetLastValidText(DependencyObject d) => (string)d.GetValue(LastValidTextProperty);

		/// <summary>
		/// \if KO
		/// <para>활성화 값 변경을 처리하여 텍스트 상자의 검증 이벤트를 연결하거나 해제합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Handles an enabled-value change by attaching or detaching validation events on the text box.</para>
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

		/// <summary>
		/// \if KO
		/// <para>텍스트 상자가 로드될 때 현재 텍스트를 마지막 유효값으로 저장합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Stores the current text as the last valid value when the text box is loaded.</para>
		/// \endif
		/// </summary>
		/// <param name="sender">
		/// \if KO
		/// <para>로드된 텍스트 상자입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The text box that was loaded.</para>
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
		private static void Tb_Loaded(object sender, RoutedEventArgs e)
		{
			if (sender is TextBox tb)
				SetLastValidText(tb, tb.Text);
		}

		/// <summary>
		/// \if KO
		/// <para>붙여넣을 텍스트를 미리 검증하고 구성된 방식에 따라 거부하거나 보정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Validates text before it is pasted and rejects or clamps it according to the configured mode.</para>
		/// \endif
		/// </summary>
		/// <param name="sender">
		/// \if KO
		/// <para>붙여넣기 대상 텍스트 상자입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target text box for the paste operation.</para>
		/// \endif
		/// </param>
		/// <param name="e">
		/// \if KO
		/// <para>붙여넣기 데이터와 취소 기능을 제공하는 이벤트 데이터입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Event data that supplies the pasted data and cancellation support.</para>
		/// \endif
		/// </param>
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
		/// \if KO
		/// <para>텍스트 변경을 처리하고 보정 모드에서 범위를 벗어난 값을 즉시 보정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Handles text changes and immediately clamps out-of-range values when clamp mode is active.</para>
		/// \endif
		/// </summary>
		/// <param name="sender">
		/// \if KO
		/// <para>텍스트가 변경된 텍스트 상자입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The text box whose text changed.</para>
		/// \endif
		/// </param>
		/// <param name="e">
		/// \if KO
		/// <para>텍스트 변경 이벤트 데이터입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The text-change event data.</para>
		/// \endif
		/// </param>
		/// <remarks>
		/// \if KO
		/// <para>거부 모드의 복원은 자연스러운 입력을 위해 포커스를 잃을 때 수행됩니다.</para>
		/// \endif
		/// \if EN
		/// <para>In reject mode, restoration occurs when focus is lost to preserve a natural typing experience.</para>
		/// \endif
		/// </remarks>
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
		/// \if KO
		/// <para>포커스를 잃을 때 최종 값을 검증하고 보정 또는 복원한 뒤 바인딩 원본을 갱신합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Validates the final value when focus is lost, clamps or restores it, and updates the binding source.</para>
		/// \endif
		/// </summary>
		/// <param name="sender">
		/// \if KO
		/// <para>포커스를 잃은 텍스트 상자입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The text box that lost focus.</para>
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

		/// <summary>
		/// \if KO
		/// <para>현재 선택 영역을 대체하여 문자열을 삽입했을 때 만들어질 텍스트를 계산합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Computes the text that would result from inserting a string in place of the current selection.</para>
		/// \endif
		/// </summary>
		/// <param name="tb">
		/// \if KO
		/// <para>현재 텍스트와 선택 영역을 제공하는 텍스트 상자입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The text box that supplies the current text and selection.</para>
		/// \endif
		/// </param>
		/// <param name="insert">
		/// \if KO
		/// <para>삽입할 문자열입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The string to insert.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>삽입 이후의 예상 텍스트입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The projected text after insertion.</para>
		/// \endif
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// \if KO
		/// <para>텍스트 상자의 선택 위치가 현재 문자열 범위를 벗어나면 발생할 수 있습니다.</para>
		/// \endif
		/// \if EN
		/// <para>May be thrown when the text box selection lies outside the current string.</para>
		/// \endif
		/// </exception>
		private static string GetFutureText(TextBox tb, string insert)
		{
			var text = tb.Text ?? string.Empty;
			var selStart = tb.SelectionStart;
			var selLen = tb.SelectionLength;
			if (selLen > 0) text = text.Remove(selStart, selLen);
			return text.Insert(selStart, insert);
		}

		/// <summary>
		/// \if KO
		/// <para>텍스트를 실수로 해석하여 최소·최대 범위를 검사하고 필요한 보정 문자열을 계산합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Parses text as a floating-point value, checks the minimum and maximum bounds, and computes corrected text when needed.</para>
		/// \endif
		/// </summary>
		/// <param name="tb">
		/// \if KO
		/// <para>범위 설정을 읽을 텍스트 상자입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The text box from which range settings are read.</para>
		/// \endif
		/// </param>
		/// <param name="text">
		/// \if KO
		/// <para>검증할 텍스트입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The text to validate.</para>
		/// \endif
		/// </param>
		/// <param name="corrected">
		/// \if KO
		/// <para>원본 텍스트 또는 가장 가까운 경계값 문자열을 반환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Receives the original text or a string containing the nearest boundary value.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>텍스트가 비어 있거나 유효한 범위 안의 숫자이면 <see langword="true"/>이고, 그렇지 않으면 <see langword="false"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see langword="true"/> when the text is empty or represents an in-range number; otherwise, <see langword="false"/>.</para>
		/// \endif
		/// </returns>
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
