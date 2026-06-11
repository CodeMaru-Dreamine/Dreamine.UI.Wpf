using System.ComponentModel;
using System.Windows;

namespace Dreamine.UI.Wpf.Localization;

/// <summary>지원 언어.</summary>
public enum eLanguage
{
    English,
    Korean,
    Chinese,
    Vietnamese
}

/// <summary>텍스트 케이스 변환 방식.</summary>
public enum eTextcaseType
{
    Default,
    SentenceCase,
    TitleCase,
    UpperCase,
    LowerCase,
    EmphasisCase
}

/// <summary>
/// INI/딕셔너리 기반 다국어 문자열 관리자.
/// Load() 대신 SetLanguageData()로 미리 파싱된 데이터를 주입할 수 있다.
/// </summary>
public static class DreamineLocalizationManager
{
    private static readonly HashSet<eLanguage> _valid = Enum.GetValues<eLanguage>().ToHashSet();

    private static readonly Dictionary<eLanguage, Dictionary<string, Dictionary<string, string>>> _langDict = new();

    private static eLanguage _currentLanguage = eLanguage.Korean;
    private static eLanguage _oldLanguage     = eLanguage.Korean;

    public static eLanguage OldLanguage => _oldLanguage;

    /// <summary>현재 선택된 언어. 변경 시 LanguageChanged 이벤트 발생.</summary>
    public static eLanguage CurrentLanguage
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

    /// <summary>CurrentLanguage 변경 시 발생.</summary>
    public static event EventHandler? LanguageChanged;

    /// <summary>로드된 언어 딕셔너리. [Language][Section][Key] = LocalizedValue</summary>
    public static IReadOnlyDictionary<eLanguage, IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> Languages
        => _langDict.ToDictionary(
            l => l.Key,
            l => (IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>)
                 l.Value.ToDictionary(s => s.Key, s => (IReadOnlyDictionary<string, string>)s.Value));

    /// <summary>
    /// 미리 파싱된 딕셔너리로 언어 데이터를 주입한다.
    /// INI 파서 없이도 동작하도록 Dreamine 방식으로 단순화된 진입점.
    /// </summary>
    /// <param name="language">초기 언어.</param>
    /// <param name="data">언어별 [Section][Key]=Value 딕셔너리.</param>
    public static void SetLanguageData(
        eLanguage language,
        Dictionary<eLanguage, Dictionary<string, Dictionary<string, string>>> data)
    {
        _langDict.Clear();
        foreach (var kv in data)
            _langDict[kv.Key] = kv.Value;
        CurrentLanguage = language;
    }

    /// <summary>
    /// 외부 INI 로더 훅을 통해 데이터를 로드한다.
    /// loadFn: (filePath) → [Section][Key]=Value
    /// </summary>
    public static void Load(
        Func<string, Dictionary<string, Dictionary<string, string>>> loadFn,
        eLanguage language = eLanguage.English,
        string? basePath = null)
    {
        CurrentLanguage = language;
        string dir = basePath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Languages");

        foreach (eLanguage lang in Enum.GetValues<eLanguage>())
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

    /// <summary>현재 언어에서 섹션/키로 번역 문자열을 반환.</summary>
    public static string Get(string section, string key)
        => Get(CurrentLanguage, section, key);

    /// <summary>지정 언어에서 섹션/키로 번역 문자열을 반환.</summary>
    public static string Get(eLanguage lang, string section, string key)
    {
        if (_langDict.TryGetValue(lang, out var sectionDict) &&
            sectionDict.TryGetValue(section, out var keyDict) &&
            keyDict.TryGetValue(key, out var value))
            return value;

        return null!;
    }

    /// <summary>값으로 키를 역조회.</summary>
    public static string? GetKeyFromValue(eLanguage lang, string section, string value)
    {
        if (_langDict.TryGetValue(lang, out var sectionDict) &&
            sectionDict.TryGetValue(section, out var keyDict))
        {
            foreach (var pair in keyDict)
                if (pair.Value == value) return pair.Key;
        }
        return null;
    }

    /// <summary>이전 언어의 값을 현재 언어로 변환.</summary>
    public static string GetLocalizedEquivalent(string section, string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        var key     = GetKeyFromValue(_oldLanguage, section, value);
        if (string.IsNullOrEmpty(key)) return value;
        var newLang = Get(CurrentLanguage, section, key);
        return string.IsNullOrEmpty(newLang) ? value : newLang;
    }
}

/// <summary>XAML 바인딩을 위한 싱글턴 프록시.</summary>
public class StaticLocalizationProxy : DependencyObject, INotifyPropertyChanged
{
    public static StaticLocalizationProxy Instance { get; set; } = new();

    public StaticLocalizationProxy()
    {
        DreamineLocalizationManager.LanguageChanged += (_, _) => OnPropertyChanged(nameof(CurrentLanguage));
    }

    public eLanguage CurrentLanguage
    {
        get => DreamineLocalizationManager.CurrentLanguage;
        set => DreamineLocalizationManager.CurrentLanguage = value;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
