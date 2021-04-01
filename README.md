# Xamarin.Forms.Breadcrumb [![Nuget](https://img.shields.io/nuget/v/Xamarin.Forms.Breadcrumb)](https://www.nuget.org/packages/Xamarin.Forms.Breadcrumb) [![Nuget](https://img.shields.io/nuget/dt/Xamarin.Forms.Breadcrumb)](https://www.nuget.org/packages/Xamarin.Forms.Breadcrumb) 

[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FIeuanWalker%2FXamarin.Forms.Breadcrumb.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FIeuanWalker%2FXamarin.Forms.Breadcrumb?ref=badge_shield)

[![Release](https://github.com/IeuanWalker/Xamarin.Forms.Breadcrumb/actions/workflows/release.yml/badge.svg)](https://github.com/IeuanWalker/Xamarin.Forms.Breadcrumb/actions/workflows/release.yml) ![Dependabot](https://api.dependabot.com/badges/status?host=github&repo=IeuanWalker/Xamarin.Forms.Breadcrumb)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/15865c5fcb684e8f821d9f87544c4f36)](https://app.codacy.com/app/ieuan.walker007/Xamarin.Forms.Breadcrumb?utm_source=github.com&utm_medium=referral&utm_content=IeuanWalker/Xamarin.Forms.Breadcrumb&utm_campaign=Badge_Grade_Dashboard)


This is a breadcrumb navigation control that is completely automatic and uses the Navigation stack to get the page titles to generate the breadcrumbs.

The animation for the control is based on this article - [A Cool Breadcrumbs Bar with Xamarin Forms Animations…](https://theconfuzedsourcecode.wordpress.com/2017/02/04/a-cool-breadcrumbs-bar-with-xamarin-forms-animations/)

Also incorporated [Xamarin.Forms.Pancake](https://github.com/sthewissen/Xamarin.Forms.PancakeView) for more customisation options.

Basic example             |  Production Example
:-------------------------:|:-------------------------:
![Example gif](https://github.com/IeuanWalker/Xamarin.Forms.Breadcrumb/blob/master/Example.gif)  |  ![Production Example gif](https://github.com/IeuanWalker/Xamarin.Forms.Breadcrumb/blob/master/ProdExample.gif)



## How to use it?
Install the [NuGet package](https://www.nuget.org/packages/Xamarin.Forms.Breadcrumb) into your shared project project 
```
Install-Package Xamarin.Forms.Breadcrumb
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
| BreadcrumbMargin | A `Thickness` object used to define the spacing between the breadcrumb and separators | Uses the `Thickness` struct allowing you to specify left, top, right and bottom margin |
| BreadcrumbBackgroundColor | This is the background color for the individual breadcrumbs | A `Color` object. <br> Default value is **Transparent**. <br> *(doesnt include the last breadcrumb)* |
| LastBreadcrumbTextColor | Sets the text color for the last breadcrumb | A Color object. <br> Default value is **black**. |
| LastBreadcrumbCornerRadius | A `CornerRadius` object representing each individual corner's radius. <br> This is property exposed from [PancakeView](https://github.com/sthewissen/Xamarin.Forms.PancakeView) | Uses the `CornerRadius` struct allowing you to specify individual corners. <br> Default value is **10**. |
| LastBreadcrumbBackgroundColor | Sets the background color of the last breadcrumbs |  A Color object. <br> Default value is **Transparent**. |
| AnimationSpeed | Sets the speed of the animated breadcrumb | Default value is **800**. <br> Set to 0 to disable the animation. |
| IsNavigationEnabled | Used to remove the tab gesture from breadcrumbs | Default value is **True**|

### First breadcrumb customization
You are able to change the first breadcrumb to an Icon, embedded image or url image.
It implements the Xamarin.Forms ImageSource object.

```xaml
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
```xaml
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
```xaml
<breadcrumb:Breadcrumb Padding="15" VerticalOptions="Start">
    <breadcrumb:Breadcrumb.Separator>
        <UriImageSource Uri="https://cdn.iconscout.com/icon/free/png-256/xamarin-4-599473.png" />
    </breadcrumb:Breadcrumb.Separator>
</breadcrumb:Breadcrumb>
```

Image - Embedded
```xaml
<breadcrumb:Breadcrumb Padding="15" VerticalOptions="Start">
    <breadcrumb:Breadcrumb.Separator>
        <FileImageSource File="exampleImage.png" />
    </breadcrumb:Breadcrumb.Separator>
</breadcrumb:Breadcrumb>
```








## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FIeuanWalker%2FXamarin.Forms.Breadcrumb.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FIeuanWalker%2FXamarin.Forms.Breadcrumb?ref=badge_large)
