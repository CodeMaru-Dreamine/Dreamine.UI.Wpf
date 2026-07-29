<!--!
\file README.md
\brief Dreamine.UI.Wpf - Foundation WPF utilities: converters, behaviors, localization, and base controls for Dreamine UI.
\author Dreamine Core Team
\date 2026-06-12
\version 1.0.0
-->

# Dreamine.UI.Wpf

[![CI](https://github.com/CodeMaru-Dreamine/Dreamine.UI.Wpf/actions/workflows/ci.yml/badge.svg)](https://github.com/CodeMaru-Dreamine/Dreamine.UI.Wpf/actions/workflows/ci.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.UI.Wpf&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.UI.Wpf)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.UI.Wpf&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.UI.Wpf)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.UI.Wpf&metric=coverage)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.UI.Wpf)
[![license](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8-512BD4.svg)](https://dotnet.microsoft.com/)
[![WPF](https://img.shields.io/badge/WPF-0078D7.svg)](https://learn.microsoft.com/dotnet/desktop/wpf/)
[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2022%20%7C%202026-5C2D91.svg)](https://visualstudio.microsoft.com/)
[![nuget](https://img.shields.io/nuget/v/Dreamine.UI.Wpf.svg)](https://www.nuget.org/packages/Dreamine.UI.Wpf)
[![downloads](https://img.shields.io/nuget/dt/Dreamine.UI.Wpf.svg)](https://www.nuget.org/packages/Dreamine.UI.Wpf)
[![Docs](https://img.shields.io/badge/Docs-dreamine.kr-38BDF8.svg)](https://dreamine.kr)
[![Guide](https://img.shields.io/badge/Guide-dreamine.kr-0EA5E9.svg)](https://dreamine.kr)
[![Playground](https://img.shields.io/badge/Playground-dreamine.kr-7C3AED.svg)](https://dreamine.kr)
[![Book](https://img.shields.io/badge/Book-Practical%20MVVM%20Architecture-111827.svg)](https://dreamine.kr)

**Dreamine.UI.Wpf** is the WPF foundation layer of the Dreamine UI stack.

It provides shared utilities consumed by all higher-level Dreamine UI packages:

- Value converters
- WPF behaviors
- Localization infrastructure
- Base LED control primitives

[➡️ 한국어 문서 보기](./README_KO.md)

---

## What this library solves

WPF UI packages need a shared foundation for:

- Binding converters reused across many XAML files
- XAML-attachable behaviors (drag, numeric range, etc.)
- Centralized multi-language text management
- Shared enumerations and primitives for LED controls

Without this layer, each UI package would duplicate converters and utilities, causing divergence and maintenance burden.

---

## Key Features

- **Converters**: null-to-visibility, bool-to-int, LED geometry, value/unit combination, format-to-example, language-to-XML
- **Behaviors**: `WindowDragBehavior`, `NumericRangeBehavior`
- **Localization**: `DreamineLocalizationManager` — multi-language text lookup with hot-reload support
- **LED primitives**: `LedCorner` enum shared across LED-related controls
- Targets `net8.0-windows`

---

## Requirements

- **Target Framework**: `net8.0-windows`
- **Dependencies**:
  - `Dreamine.MVVM.ViewModels`
  - `Microsoft.Xaml.Behaviors.Wpf`
  - `System.Drawing.Common`

---

## Installation

### NuGet

```bash
dotnet add package Dreamine.UI.Wpf
```

### PackageReference

```xml
<PackageReference Include="Dreamine.UI.Wpf" />
```

---

## Project Structure

```text
Dreamine.UI.Wpf
├── Behaviors/
│   ├── NumericRangeBehavior.cs        — clamp numeric input to min/max
│   └── WindowDragBehavior.cs          — drag window by mouse-down on element
├── Controls/
│   └── LedCorner.cs                   — corner radius enum for LED controls
├── Converters/
│   ├── BoolToIntDynamicConverter.cs   — bool → configurable int value
│   ├── FormatToExampleConverter.cs    — input format → example string
│   ├── LedInnerDiameterConverter.cs   — LED inner circle size (MultiBinding)
│   ├── LedPositionConverter.cs        — LED dot offset within bounding box
│   ├── NullToVisibilityConverter.cs   — null → Collapsed, non-null → Visible
│   └── ValueUnitCombinationConverter.cs — combine value + unit label
└── Localization/
    ├── DreamineLocalization.cs        — XAML attached property entry point
    └── DreamineLocalizationManager.cs — runtime text lookup and language switch
```

---

## Architecture Role

```text
Dreamine.MVVM.ViewModels
        │
Dreamine.UI.Wpf              ← this package
        │
Dreamine.UI.Wpf.Controls
Dreamine.UI.Wpf.Equipment
Dreamine.UI.Wpf.Themes
```

All higher-level UI packages reference this package.  
It must not reference them.

---

## Quick Start

### Converters in XAML

```xml
xmlns:conv="clr-namespace:Dreamine.UI.Wpf.Converters;assembly=Dreamine.UI.Wpf"

<conv:NullToVisibilityConverter x:Key="NullToVis" />

<TextBlock Visibility="{Binding Icon, Converter={StaticResource NullToVis}}" />
```

### WindowDragBehavior

```xml
xmlns:b="clr-namespace:Dreamine.UI.Wpf.Behaviors;assembly=Dreamine.UI.Wpf"
xmlns:i="http://schemas.microsoft.com/xaml/behaviors"

<Border Background="#FF2D2D2D">
    <i:Interaction.Behaviors>
        <b:WindowDragBehavior />
    </i:Interaction.Behaviors>
</Border>
```

### Localization

```csharp
// Set language at startup
DreamineLocalizationManager.SetLanguage(Language.ko_KR);
```

```xml
xmlns:vsl="clr-namespace:Dreamine.UI.Wpf.Localization;assembly=Dreamine.UI.Wpf"

<TextBlock vsl:DreamineLocalization.Key="MainMenu.Title" />
```

---

## Converter Reference

| Converter | Input | Output |
|---|---|---|
| `NullToVisibilityConverter` | `object?` | `Visibility` |
| `BoolToIntDynamicConverter` | `bool` | `int` (configurable) |
| `LedInnerDiameterConverter` | `double, double` (MultiBinding) | `double` |
| `LedPositionConverter` | `double, LedCorner` (MultiBinding) | `double` |
| `ValueUnitCombinationConverter` | `string` | `string` |
| `FormatToExampleConverter` | `InputFormat` | `string` |

---

## Design Notes

- This package never references higher-level UI packages
- Converters are stateless and safe to use as static resources
- `DreamineLocalizationManager` loads text from XML files at runtime — the file path is configurable
- `WindowDragBehavior` uses `Window.GetWindow(AssociatedObject).DragMove()` internally

---

## License

MIT License
