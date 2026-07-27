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
	/// \if KO
	/// <para>애플리케이션 <see cref="Language"/> 값과 WPF <see cref="XmlLanguage"/> 사이를 변환합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Converts between application <see cref="Language"/> values and WPF <see cref="XmlLanguage"/> values.</para>
	/// \endif
	/// </summary>
	public sealed class LanguageToXmlLanguageConverter : IValueConverter
	{
		/// <summary>
		/// \if KO
		/// <para>언어 열거형을 대응하는 IETF 언어 태그의 XML 언어로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts a language enumeration to an XML language with the corresponding IETF language tag.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>원본 언어 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The source language value.</para>
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
		/// <para>대응하는 XML 언어이며 입력이 없으면 한국어입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The corresponding XML language, defaulting to Korean.</para>
		/// \endif
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var lang = value as Language? ?? Language.Korean;

			// 필요 시 세부 로케일 문자열 수정 가능 (예: zh-Hant-TW 등)
			var tag = lang switch
			{
				Language.English => "en-US",
				Language.Korean => "ko-KR",
				Language.Chinese => "zh-CN",
				Language.Vietnamese => "vi-VN",
				_ => "ko-KR"
			};

			return XmlLanguage.GetLanguage(tag);
		}

		/// <summary>
		/// \if KO
		/// <para>XML 언어 태그의 언어 접두사를 애플리케이션 언어 열거형으로 변환합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Converts an XML language tag prefix to the application language enumeration.</para>
		/// \endif
		/// </summary>
		/// <param name="value">
		/// \if KO
		/// <para>대상 XML 언어입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The target XML language.</para>
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
		/// <para>대응하는 언어이며 지원되지 않으면 한국어입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The corresponding language, defaulting to Korean when unsupported.</para>
		/// \endif
		/// </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is XmlLanguage xml)
			{
				var tag = xml.IetfLanguageTag.ToLowerInvariant();
				if (tag.StartsWith("en")) return Language.English;
				if (tag.StartsWith("ko")) return Language.Korean;
				if (tag.StartsWith("zh")) return Language.Chinese;
				if (tag.StartsWith("vi")) return Language.Vietnamese;
			}
			return Language.Korean;
		}
	}
}
