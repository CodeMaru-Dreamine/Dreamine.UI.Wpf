using System.Data;
using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>정수에 변환기 매개변수로 표현한 산술 연산을 적용합니다.</para>
/// \endif
/// \if EN
/// <para>Applies an arithmetic operation expressed by the converter parameter to an integer.</para>
/// \endif
/// </summary>
public class MathConverter : IValueConverter
{
	/// <summary>
	/// \if KO
	/// <para>원본 정수와 연산 문자열을 식으로 결합하여 계산합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Combines the source integer and operation text into an expression and evaluates it.</para>
	/// \endif
	/// </summary>
	/// <param name="value">
	/// \if KO
	/// <para>원본 정수입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The source integer.</para>
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
	/// <para>정수 뒤에 붙일 산술 연산 문자열입니다.</para>
	/// \endif
	/// \if EN
	/// <para>Arithmetic operation text appended to the integer.</para>
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
	/// <para>계산된 32비트 정수이며 계산 실패 시 원본 값입니다. 입력이 정수가 아니면 0입니다.</para>
	/// \endif
	/// \if EN
	/// <para>The calculated 32-bit integer, the original value on evaluation failure, or zero for noninteger input.</para>
	/// \endif
	/// </returns>
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is not int number)
			return 0;

		if (parameter is not string operate || string.IsNullOrWhiteSpace(operate))
			return number;

		string expr = $"{number}{operate}";

		object resultObj;
		try
		{
			// null 대신 string.Empty로 주면 nullable 경고/분석 꼬임이 줄어듭니다.
			resultObj = new DataTable().Compute(expr, string.Empty);
		}
		catch
		{
			// 연산 문자열이 이상하면 원본 반환
			return number;
		}

		// Compute 결과가 null/DBNull일 수 있으니 방어
		if (resultObj is null || resultObj is DBNull)
			return number;

		// Compute은 보통 double/decimal 등을 뱉을 수 있음 → int로 안전 변환
		if (resultObj is IConvertible)
		{
			try
			{
				return System.Convert.ToInt32(resultObj, CultureInfo.InvariantCulture);
			}
			catch
			{
				return number;
			}
		}

		return number;
	}

	/// <summary>
	/// \if KO
	/// <para>역변환은 구현되어 있지 않습니다.</para>
	/// \endif
	/// \if EN
	/// <para>Reverse conversion is not implemented.</para>
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
