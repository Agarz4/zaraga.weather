using System;
using System.IO;
using Microsoft.Maui.ApplicationModel;

namespace zaraga.weather;

internal static class BuildMetadata
{
    internal static readonly string BaseWeatherapiUrl = "http://api.weatherapi.com/v1/";
    internal static readonly string PackageName = AppInfo.PackageName ?? "za_weather";
    internal static readonly string LogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{PackageName}.txt");
}