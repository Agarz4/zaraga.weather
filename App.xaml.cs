using Microsoft.Maui.Controls;

namespace zaraga.weather;

public partial class App : Application
{
	public static zaraga.logger.Manager Console;

	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
		Console  = zaraga.logger.Manager.Instance;

		Console.Init(filePath: BuildMetadata.LogPath, daysToRecord: 3);
	}
}