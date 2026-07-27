using System.ComponentModel;
using System.IO;
using System.Windows;

namespace Dreamine.UI.Wpf.Localization;

/// <summary>
/// \if KO
/// <para>지원되는 사용자 인터페이스 언어를 지정합니다.</para>
/// \endif
/// \if EN
/// <para>Specifies a supported user-interface language.</para>
/// \endif
/// </summary>
public enum Language
{
    /// <summary>
    /// \if KO
    /// <para>영어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>English.</para>
    /// \endif
    /// </summary>
    English,
    /// <summary>
    /// \if KO
    /// <para>한국어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Korean.</para>
    /// \endif
    /// </summary>
    Korean,
    /// <summary>
    /// \if KO
    /// <para>중국어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Chinese.</para>
    /// \endif
    /// </summary>
    Chinese,
    /// <summary>
    /// \if KO
    /// <para>베트남어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Vietnamese.</para>
    /// \endif
    /// </summary>
    Vietnamese
}

/// <summary>
/// \if KO
/// <para>지역화된 텍스트에 적용할 대소문자 변환 방식을 지정합니다.</para>
/// \endif
/// \if EN
/// <para>Specifies a casing transformation for localized text.</para>
/// \endif
/// </summary>
public enum TextcaseType
{
    /// <summary>
    /// \if KO
    /// <para>텍스트를 변경하지 않습니다.</para>
    /// \endif
    /// \if EN
    /// <para>Leaves text unchanged.</para>
    /// \endif
    /// </summary>
    Default,
    /// <summary>
    /// \if KO
    /// <para>문장의 첫 글자만 대문자로 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Capitalizes only the first character of a sentence.</para>
    /// \endif
    /// </summary>
    SentenceCase,
    /// <summary>
    /// \if KO
    /// <para>각 단어의 첫 글자를 대문자로 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Capitalizes the first character of each word.</para>
    /// \endif
    /// </summary>
    TitleCase,
    /// <summary>
    /// \if KO
    /// <para>모든 글자를 대문자로 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts all characters to uppercase.</para>
    /// \endif
    /// </summary>
    UpperCase,
    /// <summary>
    /// \if KO
    /// <para>모든 글자를 소문자로 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts all characters to lowercase.</para>
    /// \endif
    /// </summary>
    LowerCase,
    /// <summary>
    /// \if KO
    /// <para>첫 단어를 대문자로 강조합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Emphasizes the first word in uppercase.</para>
    /// \endif
    /// </summary>
    EmphasisCase
}

/// <summary>
/// \if KO
/// <para>INI 파일 또는 미리 파싱된 사전을 사용하는 다국어 문자열 저장소를 관리합니다.</para>
/// \endif
/// \if EN
/// <para>Manages a multilingual string store backed by INI files or pre-parsed dictionaries.</para>
/// \endif
/// </summary>
public static class DreamineLocalizationManager
{
    /// <summary>
    /// \if KO
    /// <para>언어 열거형의 유효한 값 집합입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Contains the valid values of the language enumeration.</para>
    /// \endif
    /// </summary>
    private static readonly HashSet<Language> _valid = Enum.GetValues<Language>().ToHashSet();

    /// <summary>
    /// \if KO
    /// <para>언어, 섹션, 키 순서로 구성된 지역화 문자열 저장소입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Stores localized strings indexed by language, section, and key.</para>
    /// \endif
    /// </summary>
    private static readonly Dictionary<Language, Dictionary<string, Dictionary<string, string>>> _langDict = new();

    /// <summary>
    /// \if KO
    /// <para>현재 선택된 언어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Stores the currently selected language.</para>
    /// \endif
    /// </summary>
    private static Language _currentLanguage = Language.Korean;
    /// <summary>
    /// \if KO
    /// <para>직전에 선택되었던 언어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Stores the previously selected language.</para>
    /// \endif
    /// </summary>
    private static Language _oldLanguage     = Language.Korean;

    /// <summary>
    /// \if KO
    /// <para>직전에 선택되었던 언어를 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the previously selected language.</para>
    /// \endif
    /// </summary>
    public static Language OldLanguage => _oldLanguage;

    /// <summary>
    /// \if KO
    /// <para>현재 선택된 언어를 가져오거나 설정합니다. 유효한 새 값으로 변경되면 <see cref="LanguageChanged"/>가 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets the current language and raises <see cref="LanguageChanged"/> when changed to a new valid value.</para>
    /// \endif
    /// </summary>
    public static Language CurrentLanguage
    {
        get => _currentLanguage;
        set
        {
            if (_valid.Contains(value) && _currentLanguage != value)
            {
                _oldLanguage     = _currentLanguage;
                _currentLanguage = value;
                LanguageChanged?.Invoke(null, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// \if KO
    /// <para>현재 언어가 변경된 후 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Occurs after the current language changes.</para>
    /// \endif
    /// </summary>
    public static event EventHandler? LanguageChanged;

    /// <summary>
    /// \if KO
    /// <para>로드된 지역화 데이터를 언어, 섹션, 키 순서의 읽기 전용 사전으로 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets loaded localization data as a read-only dictionary indexed by language, section, and key.</para>
    /// \endif
    /// </summary>
    public static IReadOnlyDictionary<Language, IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> Languages
        => _langDict.ToDictionary(
            l => l.Key,
            l => (IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>)
                 l.Value.ToDictionary(s => s.Key, s => (IReadOnlyDictionary<string, string>)s.Value));

    /// <summary>
    /// \if KO
    /// <para>미리 파싱된 사전으로 전체 언어 데이터를 교체하고 초기 언어를 선택합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Replaces all language data with a pre-parsed dictionary and selects the initial language.</para>
    /// \endif
    /// </summary>
    /// <param name="language">
    /// \if KO
    /// <para>선택할 초기 언어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The initial language to select.</para>
    /// \endif
    /// </param>
    /// <param name="data">
    /// \if KO
    /// <para>언어, 섹션, 키 순서로 구성된 데이터입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Data indexed by language, section, and key.</para>
    /// \endif
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// \if KO
    /// <para><paramref name="data"/>가 <see langword="null"/>인 경우 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="data"/> is <see langword="null"/>.</para>
    /// \endif
    /// </exception>
    public static void SetLanguageData(
        Language language,
        Dictionary<Language, Dictionary<string, Dictionary<string, string>>> data)
    {
        _langDict.Clear();
        foreach (var kv in data)
            _langDict[kv.Key] = kv.Value;
        CurrentLanguage = language;
    }

    /// <summary>
    /// \if KO
    /// <para>외부 INI 로더 함수를 사용하여 존재하는 언어 파일을 지역화 저장소에 로드합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Loads existing language files into the localization store by using an external INI loader function.</para>
    /// \endif
    /// </summary>
    /// <param name="loadFn">
    /// \if KO
    /// <para>파일 경로를 받아 섹션과 키 사전을 반환하는 함수입니다.</para>
    /// \endif
    /// \if EN
    /// <para>A function that accepts a file path and returns a dictionary indexed by section and key.</para>
    /// \endif
    /// </param>
    /// <param name="language">
    /// \if KO
    /// <para>로드 후 선택할 언어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The language to select for use.</para>
    /// \endif
    /// </param>
    /// <param name="basePath">
    /// \if KO
    /// <para>언어 파일 디렉터리이며, 생략하면 애플리케이션의 Languages 폴더입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The language-file directory, or the application's Languages directory when omitted.</para>
    /// \endif
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// \if KO
    /// <para><paramref name="loadFn"/>이 <see langword="null"/>인데 읽을 언어 파일이 있으면 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="loadFn"/> is <see langword="null"/> and a language file is found.</para>
    /// \endif
    /// </exception>
    public static void Load(
        Func<string, Dictionary<string, Dictionary<string, string>>> loadFn,
        Language language = Language.English,
        string? basePath = null)
    {
        CurrentLanguage = language;
        string dir = basePath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Languages");

        foreach (Language lang in Enum.GetValues<Language>())
        {
            string langName = lang.ToString();
            string filePath = Path.Combine(dir, $"Ui{langName}.INI");
            if (!File.Exists(filePath))
                filePath = Path.Combine(dir, $"UI_{langName}.INI");
            if (!File.Exists(filePath))
                continue;

            _langDict[lang] = loadFn(filePath);
        }
    }

    /// <summary>
    /// \if KO
    /// <para>현재 언어의 섹션과 키에 해당하는 번역 문자열을 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the translated string for a section and key in the current language.</para>
    /// \endif
    /// </summary>
    /// <param name="section">
    /// \if KO
    /// <para>지역화 섹션입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The localization section.</para>
    /// \endif
    /// </param>
    /// <param name="key">
    /// \if KO
    /// <para>지역화 키입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The localization key.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>번역 문자열이며, 찾지 못하면 런타임상 <see langword="null"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The translated string, or runtime <see langword="null"/> when not found.</para>
    /// \endif
    /// </returns>
    public static string Get(string section, string key)
        => Get(CurrentLanguage, section, key);

    /// <summary>
    /// \if KO
    /// <para>지정한 언어의 섹션과 키에 해당하는 번역 문자열을 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the translated string for a section and key in a specified language.</para>
    /// \endif
    /// </summary>
    /// <param name="lang">
    /// \if KO
    /// <para>조회할 언어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The language to query.</para>
    /// \endif
    /// </param>
    /// <param name="section">
    /// \if KO
    /// <para>지역화 섹션입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The localization section.</para>
    /// \endif
    /// </param>
    /// <param name="key">
    /// \if KO
    /// <para>지역화 키입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The localization key.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>번역 문자열이며, 찾지 못하면 런타임상 <see langword="null"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The translated string, or runtime <see langword="null"/> when not found.</para>
    /// \endif
    /// </returns>
    public static string Get(Language lang, string section, string key)
    {
        if (_langDict.TryGetValue(lang, out var sectionDict) &&
            sectionDict.TryGetValue(section, out var keyDict) &&
            keyDict.TryGetValue(key, out var value))
            return value;

        return null!;
    }

    /// <summary>
    /// \if KO
    /// <para>지정한 언어와 섹션에서 번역값에 대응하는 첫 번째 키를 찾습니다.</para>
    /// \endif
    /// \if EN
    /// <para>Finds the first key associated with a translated value in a language and section.</para>
    /// \endif
    /// </summary>
    /// <param name="lang">
    /// \if KO
    /// <para>검색할 언어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The language to search.</para>
    /// \endif
    /// </param>
    /// <param name="section">
    /// \if KO
    /// <para>검색할 섹션입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The section to search.</para>
    /// \endif
    /// </param>
    /// <param name="value">
    /// \if KO
    /// <para>역조회할 번역값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The translated value to reverse-lookup.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>대응하는 키이며, 찾지 못하면 <see langword="null"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The matching key, or <see langword="null"/> when not found.</para>
    /// \endif
    /// </returns>
    public static string? GetKeyFromValue(Language lang, string section, string value)
    {
        if (_langDict.TryGetValue(lang, out var sectionDict) &&
            sectionDict.TryGetValue(section, out var keyDict))
        {
            foreach (var pair in keyDict)
                if (pair.Value == value) return pair.Key;
        }
        return null;
    }

    /// <summary>
    /// \if KO
    /// <para>이전 언어의 표시값을 같은 키에 해당하는 현재 언어의 값으로 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts a display value from the previous language to the current-language value for the same key.</para>
    /// \endif
    /// </summary>
    /// <param name="section">
    /// \if KO
    /// <para>값을 조회할 지역화 섹션입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The localization section in which to resolve the value.</para>
    /// \endif
    /// </param>
    /// <param name="value">
    /// \if KO
    /// <para>이전 언어로 표시된 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The value displayed in the previous language.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>현재 언어의 대응값이며, 변환할 수 없으면 원본 값입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The equivalent current-language value, or the original value when it cannot be converted.</para>
    /// \endif
    /// </returns>
    public static string GetLocalizedEquivalent(string section, string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        var key     = GetKeyFromValue(_oldLanguage, section, value);
        if (string.IsNullOrEmpty(key)) return value;
        var newLang = Get(CurrentLanguage, section, key);
        return string.IsNullOrEmpty(newLang) ? value : newLang;
    }
}

/// <summary>
/// \if KO
/// <para>정적 지역화 관리자를 XAML 바인딩에 노출하는 싱글턴 프록시입니다.</para>
/// \endif
/// \if EN
/// <para>Provides a singleton proxy that exposes the static localization manager to XAML binding.</para>
/// \endif
/// </summary>
public class StaticLocalizationProxy : DependencyObject, INotifyPropertyChanged
{
    /// <summary>
    /// \if KO
    /// <para>공유 프록시 인스턴스를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets the shared proxy instance.</para>
    /// \endif
    /// </summary>
    public static StaticLocalizationProxy Instance { get; set; } = new();

    /// <summary>
    /// \if KO
    /// <para>프록시를 만들고 언어 변경 알림을 속성 변경 알림으로 연결합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Initializes the proxy and forwards language changes as property-change notifications.</para>
    /// \endif
    /// </summary>
    public StaticLocalizationProxy()
    {
        DreamineLocalizationManager.LanguageChanged += (_, _) => OnPropertyChanged(nameof(CurrentLanguage));
    }

    /// <summary>
    /// \if KO
    /// <para>현재 지역화 언어를 가져오거나 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets or sets the current localization language.</para>
    /// \endif
    /// </summary>
    public Language CurrentLanguage
    {
        get => DreamineLocalizationManager.CurrentLanguage;
        set => DreamineLocalizationManager.CurrentLanguage = value;
    }

    /// <summary>
    /// \if KO
    /// <para>바인딩된 속성 값이 변경될 때 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Occurs when a bound property value changes.</para>
    /// \endif
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// \if KO
    /// <para>지정한 속성 이름으로 <see cref="PropertyChanged"/> 이벤트를 발생시킵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Raises <see cref="PropertyChanged"/> for the specified property name.</para>
    /// \endif
    /// </summary>
    /// <param name="name">
    /// \if KO
    /// <para>변경된 속성 이름입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The name of the changed property.</para>
    /// \endif
    /// </param>
    protected void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
