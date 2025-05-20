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
    public HomePage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            IsBusy = true;


            await Task.Delay(3000);
            var resp = await ApiService.Instance.GetRequest<ErrorResponse>("current.json?key=7d848ee5e8d34b84bf1211825250205&q=19.387313970312963, -99.06179532153836&aqi=no", new ErrorResponse());
            Debug.WriteLine(resp.message);
            IsBusy = false;
        }
        catch (Exception ex)
        {
            IsBusy = false;
            await Shell.Current.DisplayAlert("Error", ex.Message, "ok");
        }
    }
}