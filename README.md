| :warning:        | This NuGet/ repo is now in maintenance mode and support will end once xamarin.forms is no longer supported. _Bug fixes only._ </br> MAUI repo - https://github.com/IeuanWalker/Maui.Breadcrumb      |
|---------------|:------------------------|


# Xamarin.Forms.Breadcrumb [![Nuget](https://img.shields.io/nuget/v/Xamarin.Forms.Breadcrumb)](https://www.nuget.org/packages/Xamarin.Forms.Breadcrumb) [![Nuget](https://img.shields.io/nuget/dt/Xamarin.Forms.Breadcrumb)](https://www.nuget.org/packages/Xamarin.Forms.Breadcrumb) 

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/15865c5fcb684e8f821d9f87544c4f36)](https://app.codacy.com/app/ieuan.walker007/Xamarin.Forms.Breadcrumb?utm_source=github.com&utm_medium=referral&utm_content=IeuanWalker/Xamarin.Forms.Breadcrumb&utm_campaign=Badge_Grade_Dashboard)


This is a breadcrumb navigation control that is completely automatic and uses the Navigation stack to get the page titles to generate the breadcrumbs.

The animation for the control is based on this article - [A Cool Breadcrumbs Bar with Xamarin Forms Animations…](https://theconfuzedsourcecode.wordpress.com/2017/02/04/a-cool-breadcrumbs-bar-with-xamarin-forms-animations/)

Basic example             |  Production Example
:-------------------------:|:-------------------------:
![Example gif](/Example.gif)  |  ![Production Example gif](/ProdExample.gif)



## How to use it?
Install the [NuGet package](https://www.nuget.org/packages/Xamarin.Forms.Breadcrumb) into all of your projects 
```
Install-Package Xamarin.Forms.Breadcrumb
```

For iOS add the following to AppDelegate.cs > FinishedLaunching
```csharp
BreadcrumbButtonRenderer.Init();
```

To add to a page the first thing we need to do is tell our XAML page where it can find the Breadcrumb control, which is done by adding the following attribute to our ContentPage:

```xml
<ContentPage x:Class="DemoApp.Pages.BasePage"
             xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:breadcrumb="clr-namespace:Breadcrumb;assembly=Breadcrumb"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d">
    <ContentPage.Content>
        ...
    </ContentPage.Content>
</ContentPage>
```

Next up, just add the breadcrumb control onto that page and you're all set.

```xml
<breadcrumb:Breadcrumb Padding="15" VerticalOptions="Start" />
```

## What can I do with it?

| Property | What it does | Extra info |
|---|---|---- |
| Separator | Sets the image source of the separator | This allows you to set the separator to `FontImageSource`, `UriImageSource` or `FileImageSource`. </br> Default is **new FontImageSource { Glyph = " / ", Color = Colors.Black, Size = 15, }** |
| FirstBreadcrumb | Allows you to override the first breadcrumb and set an image source. F.e. This is usefull if you want the first breadcrumb to be a home icon instead of the default title. | Default will be a label like all the other breadcrumbs |
| ScrollBarVisibility | Sets the HorizontalScrollBarVisibility of the scrollview | More info here [ScrollBarVisibility](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.scrollbarvisibility?view=xamarin-forms). Default value is **ScrollBarVisibility.Never**
| FontSize | Sets the text font size for the breadcrumb | Default value is **15**. <br>Support [`NamedSize`](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.namedsize?view=xamarin-forms) |
| TextColor | Sets the text color for the breadcrumb and seperator   | A `Color` object. <br> Default value is **black**. <br>*(doesnt include the last breadcrumb)* |
| CornerRadius | A `CornerRadius` object representing each individual corner's radius for each breadcrumb. | Uses the `CornerRadius` struct allowing you to specify individual corners. <br> Default value is **10**. <br> *(doesnt include the last breadcrumb)* |
| BreadcrumbMargin | A `Thickness` object used to define the spacing between the breadcrumb and separators | Uses the `Thickness` struct allowing you to specify left, top, right and bottom margin |
| BreadcrumbBackgroundColor | This is the background color for the individual breadcrumbs | A `Color` object. <br> Default value is **Transparent**. <br> *(doesnt include the last breadcrumb)* |
| LastBreadcrumbTextColor | Sets the text color for the last breadcrumb | A Color object. <br> Default value is **black**. |
| LastBreadcrumbCornerRadius | A `CornerRadius` object representing each individual corner's radius. | Uses the `CornerRadius` struct allowing you to specify individual corners. <br> Default value is **10**. |
| LastBreadcrumbBackgroundColor | Sets the background color of the last breadcrumbs |  A Color object. <br> Default value is **Transparent**. |
| AnimationSpeed | Sets the speed of the animated breadcrumb | Default value is **800**. <br> Set to 0 to disable the animation. |
| IsNavigationEnabled | Used to remove the tab gesture from breadcrumbs | Default value is **True**|

### First breadcrumb customization
You are able to change the first breadcrumb to an Icon, embedded image or url image.
It implements the Xamarin.Forms ImageSource object.

```xml
<breadcrumb:Breadcrumb Padding="15" VerticalOptions="Start">
    <breadcrumb:Breadcrumb.FirstBreadCrumb>
        <FontImageSource FontFamily="{StaticResource FontAwesome}"
                            Glyph="{x:Static icons:IconFont.Home}"
                            Size="35"
                            Color="Red" />
    </breadcrumb:Breadcrumb.FirstBreadCrumb>
</breadcrumb:Breadcrumb>
```

### Separator customization 
You are able to change the separators to an Icon, embedded image or url image.
It implements the Xamarin.Forms ImageSource object.

Font - (FontAwesome)
```xml
<breadcrumb:Breadcrumb Padding="15" VerticalOptions="Start">
    <breadcrumb:Breadcrumb.Separator>
        <FontImageSource FontFamily="{StaticResource FontAwesome}"
                            Glyph="{x:Static icons:IconFont.ChevronRight}"
                            Size="15"
                            Color="Red" />
    </breadcrumb:Breadcrumb.Separator>
</breadcrumb:Breadcrumb>
```

Image - URL
```xml
<breadcrumb:Breadcrumb Padding="15" VerticalOptions="Start">
    <breadcrumb:Breadcrumb.Separator>
        <UriImageSource Uri="https://cdn.iconscout.com/icon/free/png-256/xamarin-4-599473.png" />
    </breadcrumb:Breadcrumb.Separator>
</breadcrumb:Breadcrumb>
```

Image - Embedded
```xml
<breadcrumb:Breadcrumb Padding="15" VerticalOptions="Start">
    <breadcrumb:Breadcrumb.Separator>
        <FileImageSource File="exampleImage.png" />
    </breadcrumb:Breadcrumb.Separator>
</breadcrumb:Breadcrumb>
```
