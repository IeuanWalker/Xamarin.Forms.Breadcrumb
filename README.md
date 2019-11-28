# Xamarin.Forms.Breadcrumb
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/15865c5fcb684e8f821d9f87544c4f36)](https://app.codacy.com/app/ieuan.walker007/Xamarin.Forms.Breadcrumb?utm_source=github.com&utm_medium=referral&utm_content=IeuanWalker/Xamarin.Forms.Breadcrumb&utm_campaign=Badge_Grade_Dashboard)
[![Nuget](https://img.shields.io/nuget/v/Xamarin.Forms.Breadcrumb)](https://www.nuget.org/packages/Xamarin.Forms.Breadcrumb) [![Nuget](https://img.shields.io/nuget/dt/Xamarin.Forms.Breadcrumb)](https://www.nuget.org/packages/Xamarin.Forms.Breadcrumb) [![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT) [![Build Status](https://dev.azure.com/ieuanwalker/Xamarin.Forms.Breadcrumb/_apis/build/status/IeuanWalker.Xamarin.Forms.Breadcrumb?branchName=master)](https://dev.azure.com/ieuanwalker/Xamarin.Forms.Breadcrumb/_build/latest?definitionId=9&branchName=master) ![Dependabot](https://api.dependabot.com/badges/status?host=github&repo=IeuanWalker/Xamarin.Forms.Breadcrumb)

This is a breadcrumb navigation control that is completely automatic and uses the Navigation stack to get the page titles to generate the breadcrumbs.

The animation for the control is based on this article - [A Cool Breadcrumbs Bar with Xamarin Forms Animations…](https://theconfuzedsourcecode.wordpress.com/2017/02/04/a-cool-breadcrumbs-bar-with-xamarin-forms-animations/)

Also incorporated [Xamarin.Forms.Pancake](https://github.com/sthewissen/Xamarin.Forms.PancakeView) for more customisation options.

![Example gif](https://github.com/IeuanWalker/Xamarin.Forms.Breadcrumb/blob/master/Example.gif)

## How to use it?
Install the [NuGet package](https://www.nuget.org/packages/Xamarin.Forms.Breadcrumb) into your shared project project 
```
Install-Package Xamarin.Forms.Breadcrumb -Version 2.0.0
```

To add to a page the first thing we need to do is tell our XAML page where it can find the Breadcrumb control, which is done by adding the following attribute to our ContentPage:

```xaml
<ContentPage x:Class="DemoApp.Pages.BasePage"
             xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:breadcrumb="clr-namespace:Breadcrumb;assembly=Xamarin.Forms.Breadcrumb"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d">
    <ContentPage.Content>
        ...
    </ContentPage.Content>
</ContentPage>
```

Next up, just add the breadcrumb control onto that page and you're all set.

```xaml
<breadcrumb:Breadcrumb Padding="15" VerticalOptions="Start" />
```

## What can I do with it?

| Property | What it does | Extra info |
|---|---|---- |
| ScrollBarVisibility | Sets the HorizontalScrollBarVisibility of the scrollview | More info here [ScrollBarVisibility](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.scrollbarvisibility?view=xamarin-forms). Default value is **ScrollBarVisibility.Never**
| TextColor | Sets the text color for the breadcrumb and seperator   | A `Color` object. <br> Default value is **black**. <br>*(doesnt include the last breadcrumb)* |
| CornerRadius | A `CornerRadius` object representing each individual corner's radius for each breadcrumb. <br> This property exposed from [PancakeView](https://github.com/sthewissen/Xamarin.Forms.PancakeView) | Uses the `CornerRadius` struct allowing you to specify individual corners. <br> Default value is **10**. <br> *(doesnt include the last breadcrumb)* |
| BreadcrumbBackgroundColor | This is the background color for the individual breadcrumbs | A `Color` object. <br> Default value is **Transparent**. <br> *(doesnt include the last breadcrumb)* |
| LastBreadcrumbTextColor | Sets the text color for the last breadcrumb | A Color object. <br> Default value is **black**. |
| LastBreadcrumbCornerRadius | A `CornerRadius` object representing each individual corner's radius. <br> This is property exposed from [PancakeView](https://github.com/sthewissen/Xamarin.Forms.PancakeView) | Uses the `CornerRadius` struct allowing you to specify individual corners. <br> Default value is **10**. |
| LastBreadcrumbBackgroundColor | Sets the background color of the last breadcrumbs |  A Color object. <br> Default value is **Transparent**. |
| AnimationSpeed | Sets the speed of the animated breadcrumb | Default value is **800**. <br> Set to 0 to disable the animation. |
| IsNavigationEnabled | Used to remove the tab gesture from breadcrumbs | Default value is **True**|



