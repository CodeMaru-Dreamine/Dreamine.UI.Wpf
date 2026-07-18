using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Dreamine.UI.Wpf.Localization;

/// <summary>
/// \if KO
/// <para>WPF 컨트롤에 지역화된 텍스트를 적용하는 연결 속성과 도우미를 제공합니다.</para>
/// \endif
/// \if EN
/// <para>Provides attached properties and helpers that apply localized text to WPF controls.</para>
/// \endif
/// </summary>
public static class DreamineLocalization
{
    #region Attached Properties

    /// <summary>
    /// \if KO
    /// <para>상속 가능한 표시 언어 연결 속성입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Identifies the inheritable attached property for the display language.</para>
    /// \endif
    /// </summary>
    public static readonly DependencyProperty LanguageProperty =
        DependencyProperty.RegisterAttached(
            "Language", typeof(Language), typeof(DreamineLocalization),
            new FrameworkPropertyMetadata(default(Language),
                FrameworkPropertyMetadataOptions.Inherits, OnLanguageChanged));

    /// <summary>
    /// \if KO
    /// <para>대상 개체의 표시 언어를 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Sets the display language on a target object.</para>
    /// \endif
    /// </summary>
    /// <param name="obj">
    /// \if KO
    /// <para>값을 설정할 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object on which to set the value.</para>
    /// \endif
    /// </param>
    /// <param name="value">
    /// \if KO
    /// <para>적용할 언어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The language to apply.</para>
    /// \endif
    /// </param>
    public static void SetLanguage(DependencyObject obj, Language value)  => obj.SetValue(LanguageProperty, value);
    /// <summary>
    /// \if KO
    /// <para>대상 개체의 표시 언어를 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the display language from a target object.</para>
    /// \endif
    /// </summary>
    /// <param name="obj">
    /// \if KO
    /// <para>값을 읽을 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object from which to read the value.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>구성된 표시 언어입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The configured display language.</para>
    /// \endif
    /// </returns>
    public static Language GetLanguage(DependencyObject obj)               => (Language)obj.GetValue(LanguageProperty);

    /// <summary>
    /// \if KO
    /// <para>지역화 키를 조회할 선택적 섹션 이름 연결 속성입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Identifies the attached property for the optional section used to resolve a localization key.</para>
    /// \endif
    /// </summary>
    public static readonly DependencyProperty LocalizationSectionProperty =
        DependencyProperty.RegisterAttached(
            "LocalizationSection", typeof(string), typeof(DreamineLocalization),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// \if KO
    /// <para>대상 개체의 지역화 섹션을 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Sets the localization section on a target object.</para>
    /// \endif
    /// </summary>
    /// <param name="obj">
    /// \if KO
    /// <para>값을 설정할 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object on which to set the value.</para>
    /// \endif
    /// </param>
    /// <param name="value">
    /// \if KO
    /// <para>섹션 이름입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The section name.</para>
    /// \endif
    /// </param>
    public static void SetLocalizationSection(DependencyObject obj, string value) => obj.SetValue(LocalizationSectionProperty, value);
    /// <summary>
    /// \if KO
    /// <para>대상 개체의 지역화 섹션을 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the localization section from a target object.</para>
    /// \endif
    /// </summary>
    /// <param name="obj">
    /// \if KO
    /// <para>값을 읽을 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object from which to read the value.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>구성된 섹션 이름입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The configured section name.</para>
    /// \endif
    /// </returns>
    public static string GetLocalizationSection(DependencyObject obj)              => (string)obj.GetValue(LocalizationSectionProperty);

    /// <summary>
    /// \if KO
    /// <para>표시할 문자열을 조회하는 지역화 키 연결 속성입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Identifies the attached property for the localization key used to resolve display text.</para>
    /// \endif
    /// </summary>
    public static readonly DependencyProperty LocalizationKeyProperty =
        DependencyProperty.RegisterAttached(
            "LocalizationKey", typeof(string), typeof(DreamineLocalization),
            new PropertyMetadata(null, OnLocalizationKeyChanged));

    /// <summary>
    /// \if KO
    /// <para>대상 개체의 지역화 키를 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Sets the localization key on a target object.</para>
    /// \endif
    /// </summary>
    /// <param name="obj">
    /// \if KO
    /// <para>값을 설정할 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object on which to set the value.</para>
    /// \endif
    /// </param>
    /// <param name="value">
    /// \if KO
    /// <para>지역화 키입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The localization key.</para>
    /// \endif
    /// </param>
    public static void SetLocalizationKey(DependencyObject obj, string value) => obj.SetValue(LocalizationKeyProperty, value);
    /// <summary>
    /// \if KO
    /// <para>대상 개체의 지역화 키를 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the localization key from a target object.</para>
    /// \endif
    /// </summary>
    /// <param name="obj">
    /// \if KO
    /// <para>값을 읽을 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object from which to read the value.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>구성된 지역화 키입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The configured localization key.</para>
    /// \endif
    /// </returns>
    public static string GetLocalizationKey(DependencyObject obj)              => (string)obj.GetValue(LocalizationKeyProperty);

    /// <summary>
    /// \if KO
    /// <para>지역화 문자열에 결합할 자리표시자 연결 속성입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Identifies the attached property for placeholder text combined with the localized string.</para>
    /// \endif
    /// </summary>
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.RegisterAttached(
            "Placeholder", typeof(string), typeof(DreamineLocalization),
            new PropertyMetadata(null, OnPlaceHolderChanged));

    /// <summary>
    /// \if KO
    /// <para>대상 개체의 자리표시자 텍스트를 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Sets placeholder text on a target object.</para>
    /// \endif
    /// </summary>
    /// <param name="obj">
    /// \if KO
    /// <para>값을 설정할 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object on which to set the value.</para>
    /// \endif
    /// </param>
    /// <param name="value">
    /// \if KO
    /// <para>자리표시자 텍스트입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The placeholder text.</para>
    /// \endif
    /// </param>
    public static void SetPlaceholder(DependencyObject obj, string value) => obj.SetValue(PlaceholderProperty, value);
    /// <summary>
    /// \if KO
    /// <para>대상 개체의 자리표시자 텍스트를 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets placeholder text from a target object.</para>
    /// \endif
    /// </summary>
    /// <param name="obj">
    /// \if KO
    /// <para>값을 읽을 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object from which to read the value.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>구성된 자리표시자 텍스트입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The configured placeholder text.</para>
    /// \endif
    /// </returns>
    public static string GetPlaceholder(DependencyObject obj)              => (string)obj.GetValue(PlaceholderProperty);

    /// <summary>
    /// \if KO
    /// <para>지역화 문자열의 대소문자 변환 방식 연결 속성입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Identifies the attached property for localized-text casing.</para>
    /// \endif
    /// </summary>
    public static readonly DependencyProperty TextCaseProperty =
        DependencyProperty.RegisterAttached(
            "TextCase", typeof(TextcaseType), typeof(DreamineLocalization),
            new PropertyMetadata(TextcaseType.Default, OnTextCaseTypeChanged));

    /// <summary>
    /// \if KO
    /// <para>대상 개체의 텍스트 대소문자 변환 방식을 설정합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Sets the text-casing mode on a target object.</para>
    /// \endif
    /// </summary>
    /// <param name="obj">
    /// \if KO
    /// <para>값을 설정할 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object on which to set the value.</para>
    /// \endif
    /// </param>
    /// <param name="value">
    /// \if KO
    /// <para>적용할 대소문자 변환 방식입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The casing mode to apply.</para>
    /// \endif
    /// </param>
    public static void SetTextCase(DependencyObject obj, TextcaseType value) => obj.SetValue(TextCaseProperty, value);
    /// <summary>
    /// \if KO
    /// <para>대상 개체의 텍스트 대소문자 변환 방식을 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the text-casing mode from a target object.</para>
    /// \endif
    /// </summary>
    /// <param name="obj">
    /// \if KO
    /// <para>값을 읽을 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object from which to read the value.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>구성된 대소문자 변환 방식입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The configured casing mode.</para>
    /// \endif
    /// </returns>
    public static TextcaseType GetTextCase(DependencyObject obj)              => (TextcaseType)obj.GetValue(TextCaseProperty);

    #endregion

    /// <summary>
    /// \if KO
    /// <para>표시 언어 변경 시 대상의 지역화를 다시 적용합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Reapplies localization when the display language changes.</para>
    /// \endif
    /// </summary>
    /// <param name="d">
    /// \if KO
    /// <para>값이 변경된 대상입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The target whose value changed.</para>
    /// \endif
    /// </param>
    /// <param name="e">
    /// \if KO
    /// <para>속성 변경 데이터입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The property-change data.</para>
    /// \endif
    /// </param>
    private static void OnLanguageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ApplyLocalization(d);
    /// <summary>
    /// \if KO
    /// <para>지역화 키 변경 시 대상의 지역화를 다시 적용합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Reapplies localization when the localization key changes.</para>
    /// \endif
    /// </summary>
    /// <param name="d">
    /// \if KO
    /// <para>값이 변경된 대상입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The target whose value changed.</para>
    /// \endif
    /// </param>
    /// <param name="e">
    /// \if KO
    /// <para>속성 변경 데이터입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The property-change data.</para>
    /// \endif
    /// </param>
    private static void OnLocalizationKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ApplyLocalization(d);
    /// <summary>
    /// \if KO
    /// <para>자리표시자 변경 시 대상의 지역화를 다시 적용합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Reapplies localization when placeholder text changes.</para>
    /// \endif
    /// </summary>
    /// <param name="d">
    /// \if KO
    /// <para>값이 변경된 대상입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The target whose value changed.</para>
    /// \endif
    /// </param>
    /// <param name="e">
    /// \if KO
    /// <para>속성 변경 데이터입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The property-change data.</para>
    /// \endif
    /// </param>
    private static void OnPlaceHolderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ApplyLocalization(d);
    /// <summary>
    /// \if KO
    /// <para>대소문자 변환 방식 변경 시 대상의 지역화를 다시 적용합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Reapplies localization when the casing mode changes.</para>
    /// \endif
    /// </summary>
    /// <param name="d">
    /// \if KO
    /// <para>값이 변경된 대상입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The target whose value changed.</para>
    /// \endif
    /// </param>
    /// <param name="e">
    /// \if KO
    /// <para>속성 변경 데이터입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The property-change data.</para>
    /// \endif
    /// </param>
    private static void OnTextCaseTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ApplyLocalization(d);

    /// <summary>
    /// \if KO
    /// <para>연결 속성 설정에 따라 문자열을 조회·가공하여 지원되는 WPF 대상에 적용합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Resolves and transforms text from attached-property settings, then applies it to a supported WPF target.</para>
    /// \endif
    /// </summary>
    /// <param name="d">
    /// \if KO
    /// <para>지역화할 종속성 개체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The dependency object to localize.</para>
    /// \endif
    /// </param>
    private static void ApplyLocalization(DependencyObject d)
    {
        var key         = GetLocalizationKey(d);
        var lang        = GetLanguage(d);
        var placeholder = GetPlaceholder(d);
        var textCase    = GetTextCase(d);
        var section     = GetLocalizationSection(d);

        if (string.IsNullOrWhiteSpace(key)) return;

        string? localizedText = section != null
            ? DreamineLocalizationManager.Get(lang, section, key)
            : TryFindLocalization(lang, key);

        if (string.IsNullOrWhiteSpace(localizedText)) localizedText = key;

        localizedText = NormalizeNewlines(localizedText);

        if (localizedText.Contains("{0}"))
            localizedText = localizedText.Replace("{0}", placeholder);
        else if (!string.IsNullOrEmpty(placeholder))
            localizedText += $" {placeholder}";

        localizedText = ApplyTextCase(localizedText, textCase);

        bool multiLine = ContainsNewline(localizedText);

        switch (d)
        {
            case Window w:                              w.Title                = localizedText; break;
            case MenuItem m:                            m.Header               = BuildHeader(localizedText, multiLine); break;
            case TreeViewItem tv:                       tv.Header              = BuildHeader(localizedText, multiLine); break;
            case GroupBox gb:                           gb.Header              = BuildHeader(localizedText, multiLine); break;
            case TabItem ti:                            ti.Header              = BuildHeader(localizedText, multiLine); break;
            case HeaderedContentControl hcc:            hcc.Header             = BuildHeader(localizedText, multiLine); break;
            case HeaderedItemsControl hic:              hic.Header             = BuildHeader(localizedText, multiLine); break;
            case ListViewItem li:                       li.Content             = BuildContent(localizedText, multiLine); break;
            case ComboBoxItem ci:                       ci.Content             = BuildContent(localizedText, multiLine); break;
            case Label lbl:                             lbl.Content            = BuildContent(localizedText, multiLine); break;
            case Button btn:                            btn.Content            = BuildContent(localizedText, multiLine); break;
            case CheckBox cb:                           cb.Content             = BuildContent(localizedText, multiLine); break;
            case RadioButton rb:                        rb.Content             = BuildContent(localizedText, multiLine); break;
            case ContentControl cc:                     cc.Content             = BuildContent(localizedText, multiLine); break;
            case TextBlock tb:
                tb.Text = localizedText.Contains("null:vs") ? string.Empty : localizedText;
                if (multiLine) tb.TextWrapping = TextWrapping.Wrap;
                break;
            case TextBox tbx:
                tbx.Text = localizedText;
                if (multiLine) { tbx.AcceptsReturn = true; tbx.TextWrapping = TextWrapping.Wrap; tbx.VerticalScrollBarVisibility = ScrollBarVisibility.Auto; }
                break;
            case Run run:                               run.Text               = localizedText; break;
            case DataGridTextColumn dtc:                dtc.Header             = BuildHeader(localizedText, multiLine); break;
            case DataGridCheckBoxColumn dcc:            dcc.Header             = BuildHeader(localizedText, multiLine); break;
            case DataGridComboBoxColumn dcb:            dcb.Header             = BuildHeader(localizedText, multiLine); break;
            case DataGridTemplateColumn dtp:            dtp.Header             = BuildHeader(localizedText, multiLine); break;
            case FrameworkElement fe:
                fe.ToolTip = BuildTooltip(localizedText);
                break;
        }
    }

    /// <summary>
    /// \if KO
    /// <para>현재 언어에서 키를 조회하고 선택적 형식 인자를 적용합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Resolves a key in the current language and applies optional formatting arguments.</para>
    /// \endif
    /// </summary>
    /// <param name="key">
    /// \if KO
    /// <para>조회할 지역화 키입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The localization key to resolve.</para>
    /// \endif
    /// </param>
    /// <param name="args">
    /// \if KO
    /// <para>문자열 형식에 적용할 인자입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Arguments applied to the composite format string.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>지역화 및 형식화된 문자열이며, 찾지 못하면 키 자체입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The localized and formatted text, or the key itself when no value is found.</para>
    /// \endif
    /// </returns>
    /// <exception cref="FormatException">
    /// \if KO
    /// <para>조회된 문자열의 형식 항목이 <paramref name="args"/>와 호환되지 않으면 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when format items in the resolved text are incompatible with <paramref name="args"/>.</para>
    /// \endif
    /// </exception>
    public static string Get(string key, params string[] args)
    {
        var text = TryFindLocalization(DreamineLocalizationManager.CurrentLanguage, key) ?? key;
        return args.Length > 0 ? string.Format(text, args) : text;
    }

    /// <summary>
    /// \if KO
    /// <para>모든 섹션을 순서대로 검색하여 지정 언어의 키 값을 찾습니다.</para>
    /// \endif
    /// \if EN
    /// <para>Searches all sections in order for a key in the specified language.</para>
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
    /// <param name="key">
    /// \if KO
    /// <para>검색할 키입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The key to find.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>찾은 문자열이며, 없으면 <see langword="null"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The resolved text, or <see langword="null"/> when not found.</para>
    /// \endif
    /// </returns>
    private static string? TryFindLocalization(Language lang, string key)
    {
        if (DreamineLocalizationManager.Languages.TryGetValue(lang, out var sections))
            foreach (var section in sections)
                if (section.Value.TryGetValue(key, out var value)) return value;
        return null;
    }

    /// <summary>
    /// \if KO
    /// <para>지정한 대소문자 변환 규칙을 텍스트에 적용합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Applies the specified casing rule to text.</para>
    /// \endif
    /// </summary>
    /// <param name="text">
    /// \if KO
    /// <para>변환할 텍스트입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The text to transform.</para>
    /// \endif
    /// </param>
    /// <param name="textCase">
    /// \if KO
    /// <para>적용할 변환 규칙입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The casing rule to apply.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>변환된 텍스트입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The transformed text.</para>
    /// \endif
    /// </returns>
    private static string ApplyTextCase(string text, TextcaseType textCase) => textCase switch
    {
        TextcaseType.UpperCase     => text.ToUpper(),
        TextcaseType.LowerCase     => text.ToLower(),
        TextcaseType.TitleCase     => string.Join(" ", text.Split(' ').Select(w =>
                                           string.IsNullOrWhiteSpace(w) ? w :
                                           char.ToUpper(w[0], CultureInfo.CurrentCulture) +
                                           (w.Length > 1 ? w[1..].ToLower(CultureInfo.CurrentCulture) : ""))),
        TextcaseType.SentenceCase  => string.IsNullOrWhiteSpace(text) ? text :
                                       char.ToUpper(text[0], CultureInfo.CurrentCulture) + text[1..].ToLower(CultureInfo.CurrentCulture),
        TextcaseType.EmphasisCase  => string.IsNullOrWhiteSpace(text) ? text :
                                       text.IndexOf(' ') is int i && i >= 0
                                           ? text[..i].ToUpperInvariant() + text[i..]
                                           : text.ToUpperInvariant(),
        _ => text
    };

    /// <summary>
    /// \if KO
    /// <para>HTML 줄바꿈 표기와 이스케이프된 줄바꿈을 현재 플랫폼 줄바꿈으로 정규화합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Normalizes HTML and escaped newline representations to the current platform newline.</para>
    /// \endif
    /// </summary>
    /// <param name="s">
    /// \if KO
    /// <para>정규화할 문자열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The string to normalize.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>줄바꿈이 정규화된 문자열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The string with normalized newlines.</para>
    /// \endif
    /// </returns>
    private static string NormalizeNewlines(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        s = Regex.Replace(s, @"<br\s*/?>", "\n", RegexOptions.IgnoreCase);
        s = s.Replace(@"\r\n", "\n").Replace(@"\n", "\n").Replace("\r\n", "\n");
        return s.Replace("\n", Environment.NewLine);
    }

    /// <summary>
    /// \if KO
    /// <para>문자열에 현재 플랫폼 줄바꿈이 포함되어 있는지 확인합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Determines whether a string contains the current platform newline.</para>
    /// \endif
    /// </summary>
    /// <param name="s">
    /// \if KO
    /// <para>검사할 문자열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The string to inspect.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>줄바꿈이 포함되어 있으면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> when a newline is present.</para>
    /// \endif
    /// </returns>
    private static bool ContainsNewline(string s) => s?.Contains(Environment.NewLine) == true;

    /// <summary>
    /// \if KO
    /// <para>헤더에 표시할 텍스트 블록을 만듭니다.</para>
    /// \endif
    /// \if EN
    /// <para>Creates a text block suitable for display as a header.</para>
    /// \endif
    /// </summary>
    /// <param name="text">
    /// \if KO
    /// <para>표시할 텍스트입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The text to display.</para>
    /// \endif
    /// </param>
    /// <param name="multiLine">
    /// \if KO
    /// <para>여러 줄 표시를 구성하려면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> to configure multiline display.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>헤더 콘텐츠로 사용할 텍스트 블록입니다.</para>
    /// \endif
    /// \if EN
    /// <para>A text block for use as header content.</para>
    /// \endif
    /// </returns>
    private static object BuildHeader(string text, bool multiLine) => multiLine
        ? new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap, TextTrimming = TextTrimming.CharacterEllipsis, MaxWidth = 300 }
        : (object)new TextBlock { Text = text };

    /// <summary>
    /// \if KO
    /// <para>일반 콘텐츠에 표시할 텍스트 블록을 만듭니다.</para>
    /// \endif
    /// \if EN
    /// <para>Creates a text block suitable for general content.</para>
    /// \endif
    /// </summary>
    /// <param name="text">
    /// \if KO
    /// <para>표시할 텍스트입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The text to display.</para>
    /// \endif
    /// </param>
    /// <param name="multiLine">
    /// \if KO
    /// <para>여러 줄 표시를 구성하려면 <see langword="true"/>입니다.</para>
    /// \endif
    /// \if EN
    /// <para><see langword="true"/> to configure multiline display.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>콘텐츠로 사용할 텍스트 블록입니다.</para>
    /// \endif
    /// \if EN
    /// <para>A text block for use as content.</para>
    /// \endif
    /// </returns>
    private static object BuildContent(string text, bool multiLine) => multiLine
        ? new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap }
        : (object)new TextBlock { Text = text };

    /// <summary>
    /// \if KO
    /// <para>제한된 너비에서 줄 바꿈되는 도구 설명 콘텐츠를 만듭니다.</para>
    /// \endif
    /// \if EN
    /// <para>Creates tooltip content that wraps within a bounded width.</para>
    /// \endif
    /// </summary>
    /// <param name="text">
    /// \if KO
    /// <para>표시할 도구 설명 텍스트입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The tooltip text to display.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>도구 설명 콘텐츠로 사용할 텍스트 블록입니다.</para>
    /// \endif
    /// \if EN
    /// <para>A text block for use as tooltip content.</para>
    /// \endif
    /// </returns>
    private static object BuildTooltip(string text)
        => new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap, MaxWidth = 420 };
}
