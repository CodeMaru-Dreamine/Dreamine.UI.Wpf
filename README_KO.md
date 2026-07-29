<!--!
\file README_KO.md
\brief Dreamine.UI.Wpf - Dreamine UI의 WPF 기반 유틸리티: 컨버터, 비헤이비어, 지역화, 기본 컨트롤 프리미티브
\author Dreamine Core Team
\date 2026-06-12
\version 1.0.0
-->

# Dreamine.UI.Wpf

[![CI](https://github.com/CodeMaru-Dreamine/Dreamine.UI.Wpf/actions/workflows/ci.yml/badge.svg)](https://github.com/CodeMaru-Dreamine/Dreamine.UI.Wpf/actions/workflows/ci.yml)
[![품질 게이트](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.UI.Wpf&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.UI.Wpf)
[![보안 등급](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.UI.Wpf&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.UI.Wpf)
[![커버리지](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.UI.Wpf&metric=coverage)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.UI.Wpf)
[![라이선스](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8-512BD4.svg)](https://dotnet.microsoft.com/)
[![WPF](https://img.shields.io/badge/WPF-0078D7.svg)](https://learn.microsoft.com/dotnet/desktop/wpf/)
[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2022%20%7C%202026-5C2D91.svg)](https://visualstudio.microsoft.com/)
[![NuGet](https://img.shields.io/nuget/v/Dreamine.UI.Wpf.svg)](https://www.nuget.org/packages/Dreamine.UI.Wpf)
[![다운로드](https://img.shields.io/nuget/dt/Dreamine.UI.Wpf.svg)](https://www.nuget.org/packages/Dreamine.UI.Wpf)
[![문서](https://img.shields.io/badge/%EB%AC%B8%EC%84%9C-dreamine.kr-38BDF8.svg)](https://dreamine.kr)
[![가이드](https://img.shields.io/badge/%EA%B0%80%EC%9D%B4%EB%93%9C-dreamine.kr-0EA5E9.svg)](https://dreamine.kr)
[![플레이그라운드](https://img.shields.io/badge/%ED%94%8C%EB%A0%88%EC%9D%B4%EA%B7%B8%EB%9D%BC%EC%9A%B4%EB%93%9C-dreamine.kr-7C3AED.svg)](https://dreamine.kr)
[![책](https://img.shields.io/badge/%EC%B1%85-Practical%20MVVM%20Architecture-111827.svg)](https://dreamine.kr)

**Dreamine.UI.Wpf**는 Dreamine UI 스택의 WPF 기반 레이어입니다.

상위 모든 Dreamine UI 패키지가 공유하는 유틸리티를 제공합니다.

- 값 변환기(Converter)
- WPF 비헤이비어(Behavior)
- 지역화 인프라
- LED 컨트롤 기본 프리미티브

[➡️ 영어 문서 보기](./README.md)

---

## 이 라이브러리가 해결하는 문제

WPF UI 패키지에는 공유 기반이 필요합니다.

- 여러 XAML 파일에서 재사용되는 바인딩 컨버터
- XAML로 부착 가능한 비헤이비어(드래그, 숫자 범위 등)
- 중앙화된 다국어 텍스트 관리
- LED 컨트롤을 위한 공유 열거형과 프리미티브

이 레이어 없이는 각 UI 패키지가 컨버터와 유틸리티를 중복 구현하게 되어 유지보수 부담이 생깁니다.

---

## 주요 기능

- **컨버터**: null-to-visibility, bool-to-int, LED 지오메트리, 값/단위 조합, 포맷-to-예시, 언어-to-XML
- **비헤이비어**: `WindowDragBehavior`, `NumericRangeBehavior`
- **지역화**: `DreamineLocalizationManager` — 핫-리로드 지원 다국어 텍스트 조회
- **LED 프리미티브**: LED 관련 컨트롤 전반에서 공유되는 `LedCorner` 열거형
- `net8.0-windows` 대상

---

## 요구 사항

- **대상 프레임워크**: `net8.0-windows`
- **의존 패키지**:
  - `Dreamine.MVVM.ViewModels`
  - `Microsoft.Xaml.Behaviors.Wpf`
  - `System.Drawing.Common`

---

## 설치

### NuGet

```bash
dotnet add package Dreamine.UI.Wpf
```

### PackageReference

```xml
<PackageReference Include="Dreamine.UI.Wpf" />
```

---

## 프로젝트 구조

```text
Dreamine.UI.Wpf
├── Behaviors/
│   ├── NumericRangeBehavior.cs        — 숫자 입력을 min/max 범위로 제한
│   └── WindowDragBehavior.cs          — 요소 마우스다운으로 창 드래그
├── Controls/
│   └── LedCorner.cs                   — LED 컨트롤용 코너 반경 열거형
├── Converters/
│   ├── BoolToIntDynamicConverter.cs   — bool → 설정 가능한 int 값
│   ├── FormatToExampleConverter.cs    — 입력 포맷 → 예시 문자열
│   ├── LedInnerDiameterConverter.cs   — LED 내부 원 크기 (MultiBinding)
│   ├── LedPositionConverter.cs        — 경계 박스 내 LED 점 오프셋
│   ├── NullToVisibilityConverter.cs   — null → Collapsed, non-null → Visible
│   └── ValueUnitCombinationConverter.cs — 값 + 단위 레이블 조합
└── Localization/
    ├── DreamineLocalization.cs        — XAML 첨부 프로퍼티 진입점
    └── DreamineLocalizationManager.cs — 런타임 텍스트 조회 및 언어 전환
```

---

## 아키텍처 역할

```text
Dreamine.MVVM.ViewModels
        │
Dreamine.UI.Wpf              ← 이 패키지
        │
Dreamine.UI.Wpf.Controls
Dreamine.UI.Wpf.Equipment
Dreamine.UI.Wpf.Themes
```

모든 상위 UI 패키지가 이 패키지를 참조합니다.  
이 패키지는 상위 패키지를 참조하지 않습니다.

---

## 빠른 시작

### XAML에서 컨버터 사용

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

### 지역화

```csharp
// 시작 시 언어 설정
DreamineLocalizationManager.SetLanguage(Language.ko_KR);
```

```xml
xmlns:vsl="clr-namespace:Dreamine.UI.Wpf.Localization;assembly=Dreamine.UI.Wpf"

<TextBlock vsl:DreamineLocalization.Key="MainMenu.Title" />
```

---

## 컨버터 참조

| 컨버터 | 입력 | 출력 |
|---|---|---|
| `NullToVisibilityConverter` | `object?` | `Visibility` |
| `BoolToIntDynamicConverter` | `bool` | `int` (설정 가능) |
| `LedInnerDiameterConverter` | `double, double` (MultiBinding) | `double` |
| `LedPositionConverter` | `double, LedCorner` (MultiBinding) | `double` |
| `ValueUnitCombinationConverter` | `string` | `string` |
| `FormatToExampleConverter` | `InputFormat` | `string` |

---

## 설계 노트

- 이 패키지는 상위 UI 패키지를 절대 참조하지 않습니다
- 컨버터는 상태 없음(stateless) — 정적 리소스로 안전하게 사용 가능
- `DreamineLocalizationManager`는 런타임에 XML 파일에서 텍스트를 로드하며 경로는 설정 가능
- `WindowDragBehavior`는 내부적으로 `Window.GetWindow(AssociatedObject).DragMove()`를 사용

---

## 라이선스

MIT License
