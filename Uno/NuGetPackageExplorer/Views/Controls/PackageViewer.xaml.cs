// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

using NuGetPackageExplorer.Types;

namespace PackageExplorer
{
    public sealed partial class PackageViewer : UserControl
    {
        private readonly ISettingsManager _settings;
        private readonly IUIServices _messageBoxServices;

        public PackageViewer(ISettingsManager settings, IUIServices messageBoxServices, IPackageChooser packageChooser)
        {
            this.InitializeComponent();

            _settings = settings;
            _messageBoxServices = messageBoxServices;

        }
    }
}
