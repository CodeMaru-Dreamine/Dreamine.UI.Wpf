
using System.Globalization;
using System.Windows.Data;

namespace Dreamine.UI.Wpf.Converters;

public class NumberComparisonConverter : IValueConverter
{
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

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
