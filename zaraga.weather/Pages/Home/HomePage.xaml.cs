using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using zaraga.weather.Models;
using zaraga.weather.Services;

namespace zaraga.weather.Pages.Home;

public partial class HomePage : ContentPage
{
    HomeViewModel? viewModel;

    public HomePage()
    {
        InitializeComponent();
        BindingContext = viewModel = new HomeViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel?.GetGurrentWeatherCommand.Execute(null);
    }

}