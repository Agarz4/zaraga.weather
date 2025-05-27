using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using zaraga.weather.Models;
using zaraga.weather.Services;
using zaraga.weather.Layouts;

namespace zaraga.weather.Pages.Home;

public partial class HomePage : LoadingContentPage
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