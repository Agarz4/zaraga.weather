using System;
using System.IO;
using Microsoft.Maui.ApplicationModel;

namespace zaraga.weather;

public static class BuildMetadata
{
	public static readonly string? PackageName = Platform.CurrentActivity?.PackageName ?? "za_weather";
	public static readonly string? LogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{PackageName}.txt");
}