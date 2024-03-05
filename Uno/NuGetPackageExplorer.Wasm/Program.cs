using Uno.UI;

namespace PackageExplorer.Wasm;

public class Program
{
    private static App? _app;

    public static int Main(string[] args)
    {
        FeatureConfiguration.ApiInformation.NotImplementedLogLevel = Uno.Foundation.Logging.LogLevel.Debug;

        Microsoft.UI.Xaml.Application.Start(_ => _app = new AppHead());

        return 0;
    }
}
