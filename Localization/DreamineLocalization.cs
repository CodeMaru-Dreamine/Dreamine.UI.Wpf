using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Dreamine.UI.Wpf.Localization;

/// <summary>
/// WPF 컨트롤에 다국어 텍스트를 적용하는 첨부 프로퍼티 모음.
/// </summary>
public static class DreamineLocalization
{
    #region Attached Properties

    public static readonly DependencyProperty LanguageProperty =
        DependencyProperty.RegisterAttached(
            "Language", typeof(eLanguage), typeof(DreamineLocalization),
            new FrameworkPropertyMetadata(default(eLanguage),
                FrameworkPropertyMetadataOptions.Inherits, OnLanguageChanged));

    public static void SetLanguage(DependencyObject obj, eLanguage value)  => obj.SetValue(LanguageProperty, value);
    public static eLanguage GetLanguage(DependencyObject obj)               => (eLanguage)obj.GetValue(LanguageProperty);

    public static readonly DependencyProperty LocalizationSectionProperty =
        DependencyProperty.RegisterAttached(
            "LocalizationSection", typeof(string), typeof(DreamineLocalization),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.Inherits));

    public static void SetLocalizationSection(DependencyObject obj, string value) => obj.SetValue(LocalizationSectionProperty, value);
    public static string GetLocalizationSection(DependencyObject obj)              => (string)obj.GetValue(LocalizationSectionProperty);

    public static readonly DependencyProperty LocalizationKeyProperty =
        DependencyProperty.RegisterAttached(
            "LocalizationKey", typeof(string), typeof(DreamineLocalization),
            new PropertyMetadata(null, OnLocalizationKeyChanged));

    public static void SetLocalizationKey(DependencyObject obj, string value) => obj.SetValue(LocalizationKeyProperty, value);
    public static string GetLocalizationKey(DependencyObject obj)              => (string)obj.GetValue(LocalizationKeyProperty);

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.RegisterAttached(
            "Placeholder", typeof(string), typeof(DreamineLocalization),
            new PropertyMetadata(null, OnPlaceHolderChanged));

    public static void SetPlaceholder(DependencyObject obj, string value) => obj.SetValue(PlaceholderProperty, value);
    public static string GetPlaceholder(DependencyObject obj)              => (string)obj.GetValue(PlaceholderProperty);

    public static readonly DependencyProperty TextCaseProperty =
        DependencyProperty.RegisterAttached(
            "TextCase", typeof(eTextcaseType), typeof(DreamineLocalization),
            new PropertyMetadata(eTextcaseType.Default, OnTextCaseTypeChanged));

    public static void SetTextCase(DependencyObject obj, eTextcaseType value) => obj.SetValue(TextCaseProperty, value);
    public static eTextcaseType GetTextCase(DependencyObject obj)              => (eTextcaseType)obj.GetValue(TextCaseProperty);

    #endregion

    private static void OnLanguageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)    => ApplyLocalization(d);
    private static void OnLocalizationKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ApplyLocalization(d);
    private static void OnPlaceHolderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ApplyLocalization(d);
    private static void OnTextCaseTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ApplyLocalization(d);

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

    public static string Get(string key, params string[] args)
    {
        var text = TryFindLocalization(DreamineLocalizationManager.CurrentLanguage, key) ?? key;
        return args.Length > 0 ? string.Format(text, args) : text;
    }

    private static string? TryFindLocalization(eLanguage lang, string key)
    {
        if (DreamineLocalizationManager.Languages.TryGetValue(lang, out var sections))
            foreach (var section in sections)
                if (section.Value.TryGetValue(key, out var value)) return value;
        return null;
    }

    private static string ApplyTextCase(string text, eTextcaseType textCase) => textCase switch
    {
        eTextcaseType.UpperCase     => text.ToUpper(),
        eTextcaseType.LowerCase     => text.ToLower(),
        eTextcaseType.TitleCase     => string.Join(" ", text.Split(' ').Select(w =>
                                           string.IsNullOrWhiteSpace(w) ? w :
                                           char.ToUpper(w[0], CultureInfo.CurrentCulture) +
                                           (w.Length > 1 ? w[1..].ToLower(CultureInfo.CurrentCulture) : ""))),
        eTextcaseType.SentenceCase  => string.IsNullOrWhiteSpace(text) ? text :
                                       char.ToUpper(text[0], CultureInfo.CurrentCulture) + text[1..].ToLower(CultureInfo.CurrentCulture),
        eTextcaseType.EmphasisCase  => string.IsNullOrWhiteSpace(text) ? text :
                                       text.IndexOf(' ') is int i && i >= 0
                                           ? text[..i].ToUpperInvariant() + text[i..]
                                           : text.ToUpperInvariant(),
        _ => text
    };

    private static string NormalizeNewlines(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        s = Regex.Replace(s, @"<br\s*/?>", "\n", RegexOptions.IgnoreCase);
        s = s.Replace(@"\r\n", "\n").Replace(@"\n", "\n").Replace("\r\n", "\n");
        return s.Replace("\n", Environment.NewLine);
    }

    private static bool ContainsNewline(string s) => s?.Contains(Environment.NewLine) == true;

    private static object BuildHeader(string text, bool multiLine) => multiLine
        ? new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap, TextTrimming = TextTrimming.CharacterEllipsis, MaxWidth = 300 }
        : (object)new TextBlock { Text = text };

    private static object BuildContent(string text, bool multiLine) => multiLine
        ? new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap }
        : (object)new TextBlock { Text = text };

    private static object BuildTooltip(string text)
        => new TextBlock { Text = text, TextWrapping = TextWrapping.Wrap, MaxWidth = 420 };
}
