using System.Data;
using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class MathConverter : IValueConverter
{
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

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
