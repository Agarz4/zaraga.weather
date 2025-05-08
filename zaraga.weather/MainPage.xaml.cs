using System;
using System.Reflection;
using Microsoft.Maui.Accessibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using zaraga.weather.Attributes;

namespace zaraga.weather;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        lblTitle.Text = $"{App.WeatherApikey}";
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

        App.Console.Write($"Clicked {count} times");
    }
}