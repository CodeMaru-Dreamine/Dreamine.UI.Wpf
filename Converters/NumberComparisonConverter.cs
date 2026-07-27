
using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

/// <summary>
/// \if KO
/// <para>숫자를 AND와 OR로 결합된 비교 조건식과 평가합니다.</para>
/// \endif
/// \if EN
/// <para>Evaluates a number against comparison conditions joined with AND and OR operators.</para>
/// \endif
/// </summary>
public class NumberComparisonConverter : IValueConverter
{
    /// <summary>
    /// \if KO
    /// <para>원본 숫자를 "||" 및 "&amp;&amp;" 조건식에 대해 평가합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Evaluates the source number against an expression using "||" and "&amp;&amp;".</para>
    /// \endif
    /// </summary>
    /// <param name="value">
    /// \if KO
    /// <para>평가할 숫자입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The numeric value to evaluate.</para>
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
    /// <para>비교 조건식입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The comparison expression.</para>
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
    /// <para>OR 그룹 중 하나의 모든 조건이 참이면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> when every condition in at least one OR group is true.</para>
    /// \endif
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        if (!double.TryParse(value.ToString(), CultureInfo.InvariantCulture, out double number))
            return false;

        string expr = parameter.ToString()!.Trim();

        var orParts = expr.Split(["||"], StringSplitOptions.RemoveEmptyEntries);

        foreach (var orPart in orParts)
        {
            var andParts = orPart.Split(["&&"], StringSplitOptions.RemoveEmptyEntries);

            bool andResult = true;
            foreach (var cond in andParts)
            {
                if (!EvaluateCondition(number, cond.Trim()))
                {
                    andResult = false;
                    break;
                }
            }

            if (andResult)
                return true;
        }

        return false;
    }

    /// <summary>
    /// \if KO
    /// <para>단일 "연산자 임계값" 조건을 평가합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Evaluates a single "operator threshold" condition.</para>
    /// \endif
    /// </summary>
    /// <param name="number">
    /// \if KO
    /// <para>비교할 숫자입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The number to compare.</para>
    /// \endif
    /// </param>
    /// <param name="condition">
    /// \if KO
    /// <para>연산자와 임계값으로 구성된 조건입니다.</para>
    /// \endif
    /// \if EN
    /// <para>A condition containing an operator and threshold.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>조건이 유효하고 만족되면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> when the condition is valid and satisfied.</para>
    /// \endif
    /// </returns>
    private bool EvaluateCondition(double number, string condition)
    {
        var tokens = condition.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length != 2)
            return false;

        string op = tokens[0];
        if (!double.TryParse(tokens[1], CultureInfo.InvariantCulture, out double threshold))
            return false;

        return op switch
        {
            ">" => number > threshold,
            ">=" => number >= threshold,
            "<" => number < threshold,
            "<=" => number <= threshold,
            "==" => Math.Abs(number - threshold) < double.Epsilon,
            "!=" => Math.Abs(number - threshold) > double.Epsilon,
            _ => false
        };
    }

    /// <summary>
    /// \if KO
    /// <para>역변환을 수행하지 않습니다.</para>
    /// \endif
    /// \if EN
    /// <para>Does not perform reverse conversion.</para>
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
    /// <para>항상 <see cref="Binding.DoNothing"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Always <see cref="Binding.DoNothing"/>.</para>
    /// \endif
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
