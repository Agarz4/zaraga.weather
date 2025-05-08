using Microsoft.Maui.Controls;
using System;
using System.Reflection;
using zaraga.weather.Attributes;

namespace zaraga.weather;

public partial class App : Application
{
    private static zaraga.logger.Manager? _console;
    private static zaraga.api.Client? _apiClient;

    internal static string WeatherApikey => Assembly.GetExecutingAssembly()?.GetCustomAttribute<WeatherApiKeyAttribute>()?.WeatherKey.ToString() ?? "";
    //App Log Manager 
    internal static zaraga.logger.Manager Console
    {
        get
        {
            if (_console == null)
            {
                _console = zaraga.logger.Manager.Instance;
            }
            return _console;
        }
    }
    //Rest API Client
    internal static zaraga.api.Client ApiClient
    {
        get
        {
            if (_apiClient == null)
            {
                _apiClient = new zaraga.api.Client("https://api.openweathermap.org/data/2.5/");
            }
            return _apiClient;
        }
    }



    public App()
    {
        InitializeComponent();

        //Init logger
        Console.Init(filePath: BuildMetadata.LogPath, daysToRecord: 3);

        MainPage = new AppShell();
    }
}