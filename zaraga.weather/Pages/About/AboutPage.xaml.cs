using Microsoft.Maui.Controls;

namespace zaraga.weather.Pages.About;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        lblApikey.Text = $"{App.WeatherApikey}";
    }
}