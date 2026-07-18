using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>값의 null 여부를 WPF 가시성으로 변환하고 선택적 반전을 지원합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts a value's null state to WPF visibility with optional inversion.</para>
	/// \endif
	/// </summary>
	public class NullToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>null 판정 결과를 반전할지 여부를 가져오거나 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets or sets whether the null-test result is inverted.</para>
		/// \endif
		/// </summary>
		public bool Inverse { get; set; } = false;

		/// <summary>
		/// \if KO
		/// <para>원본이 null인지 확인하여 Visible 또는 Collapsed로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Tests whether the source is null and converts it to Visible or Collapsed.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>검사할 원본 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source value to inspect.</para>
		/// \endif
		/// </param>
		/// <param name="targetType">
		/// \if KO
		/// <para>대상 형식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target type.</para>
		/// \endif
		/// </param>
		/// <param name="parameter">
		/// \if KO
		/// <para>사용하지 않는 매개변수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>An unused parameter.</para>
		/// \endif
		/// </param>
		/// <param name="culture">
		/// \if KO
		/// <para>사용하지 않는 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>An unused culture.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>null 여부와 반전 설정에 따른 가시성입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Visibility based on null state and inversion setting.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool visible = value != null;
			if (Inverse)
				visible = !visible;

			return visible ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <summary>
		/// \if KO
		/// <para>역변환은 지원하지 않습니다.</para>
		/// \endif
		/// \if EN
		/// <para>Reverse conversion is not supported.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>대상 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target value.</para>
		/// \endif
		/// </param>
		/// <param name="targetType">
		/// \if KO
		/// <para>원본 형식입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source type.</para>
		/// \endif
		/// </param>
		/// <param name="parameter">
		/// \if KO
		/// <para>변환기 매개변수입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The converter parameter.</para>
		/// \endif
		/// </param>
		/// <param name="culture">
		/// \if KO
		/// <para>변환 문화권입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The conversion culture.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>정상적으로 반환되지 않습니다.</para>
		/// \endif
		/// \if EN
		/// <para>This method does not return normally.</para>
		/// \endif
		/// </returns>
		/// <exception cref="NotSupportedException">
		/// \if KO
		/// <para>호출할 때 항상 발생합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Always thrown when called.</para>
		/// \endif
		/// </exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("NullToVisibilityConverter does not support ConvertBack.");
		}
	}
}
