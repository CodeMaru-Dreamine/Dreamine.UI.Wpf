using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using Dreamine.UI.Wpf.Localization;

namespace Dreamine.UI.Wpf.Converters
{
	/// <summary>
	/// <see cref="eLanguage"/> → <see cref="XmlLanguage"/> 변환 컨버터.
	/// WPF <see cref="System.Windows.FrameworkElement.Language"/> 바인딩에 사용.
	/// </summary>
	public sealed class LanguageToXmlLanguageConverter : IValueConverter
	{
		/// <summary>
		/// eLanguage → XmlLanguage 변환.
		/// </summary>
		/// <Param name="value">언어 타입(enum).</Param>
		/// <Param name="targetType">대상 타입.</Param>
		/// <Param name="parameter">미사용.</Param>
		/// <Param name="culture">호출 문화권.</Param>
		/// <returns>해당 언어의 <see cref="XmlLanguage"/>.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var lang = value as eLanguage? ?? eLanguage.Korean;

			// 필요 시 세부 로케일 문자열 수정 가능 (예: zh-Hant-TW 등)
			var tag = lang switch
			{
				eLanguage.English => "en-US",
				eLanguage.Korean => "ko-KR",
				eLanguage.Chinese => "zh-CN",
				eLanguage.Vietnamese => "vi-VN",
				_ => "ko-KR"
			};

			return XmlLanguage.GetLanguage(tag);
		}

		/// <summary>
		/// XmlLanguage → eLanguage 역변환.
		/// </summary>
		/// <Param name="value">XmlLanguage 값.</Param>
		/// <Param name="targetType">대상 타입.</Param>
		/// <Param name="parameter">미사용.</Param>
		/// <Param name="culture">호출 문화권.</Param>
		/// <returns>해당되는 <see cref="eLanguage"/>; 없으면 기본값.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is XmlLanguage xml)
			{
				var tag = xml.IetfLanguageTag.ToLowerInvariant();
				if (tag.StartsWith("en")) return eLanguage.English;
				if (tag.StartsWith("ko")) return eLanguage.Korean;
				if (tag.StartsWith("zh")) return eLanguage.Chinese;
				if (tag.StartsWith("vi")) return eLanguage.Vietnamese;
			}
			return eLanguage.Korean;
		}
	}
}