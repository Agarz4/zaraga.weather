using Microsoft.Maui.Controls;
using Microsoft.Maui.Networking;
using System.Reflection.Metadata.Ecma335;

namespace zaraga.weather.Layouts;

public class LoadingContentPage : ContentPage
{
    public static readonly BindableProperty ShowLoadingBarProperty = BindableProperty.Create("ShowLoadingBar", typeof(bool), typeof(LoadingContentPage), false);

    public bool ShowLoadingBar { get => (bool)GetValue(ShowLoadingBarProperty); set => SetValue(ShowLoadingBarProperty, value); }

    public LoadingContentPage()
    {
        if (Application.Current != null)
            ControlTemplate = (ControlTemplate)Application.Current.Resources["LoadingContentPage"];
    }
}