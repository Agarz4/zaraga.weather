using Microsoft.Maui.Controls;

namespace zaraga.weather;

public partial class App : Application
{

    private static zaraga.logger.Manager? _console;
    protected internal static zaraga.logger.Manager Console
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

    public App()
    {
        InitializeComponent();

        //start logger
        Console.Init(filePath: BuildMetadata.LogPath, daysToRecord: 3);

        MainPage = new AppShell();
    }
}