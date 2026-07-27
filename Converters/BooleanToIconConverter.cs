using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// \if KO
	/// <para>부울 값을 선택 또는 미선택 상태의 아이콘 이름으로 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts a Boolean value to an icon name representing selected or unselected state.</para>
	/// \endif
	/// </summary>
	public class BooleanToIconConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>부울 입력을 아이콘 이름으로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a Boolean input to an icon name.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>원본 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source value.</para>
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
		/// <para>참이면 "CheckCircle", 아니면 "Circle"입니다.</para>
		/// \endif
		/// \if EN
		/// <para>"CheckCircle" for true; otherwise, "Circle".</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool isChecked)
			{
				return isChecked ? "CheckCircle" : "Circle";
			}
			return "Circle";
		}

		/// <summary>
		/// \if KO
		/// <para>아이콘 이름의 역변환은 구현되어 있지 않습니다.</para>
		/// \endif
		/// \if EN
		/// <para>Reverse conversion of an icon name is not implemented.</para>
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
		/// <exception cref="NotImplementedException">
		/// \if KO
		/// <para>호출할 때 항상 발생합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Always thrown when called.</para>
		/// \endif
		/// </exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
