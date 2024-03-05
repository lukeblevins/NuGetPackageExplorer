using NuGet.Packaging.Core;
using NuGet.Versioning;

using Windows.Storage.Pickers;

namespace PackageExplorer.Presentation.Content
{
    public class HomePageViewModel : ViewModelBase
    {
        
        public NavigationService NavigationService { get; set; }

        public ICommand OpenLocalPackageCommand => this.GetCommand(OpenLocalPackage);
        public ICommand OpenRemotePackageCommand => this.GetCommand(OpenRemotePackage);
        public ICommand OpenTestPackageCommand => this.GetCommand(OpenTestPackage);

        public HomePageViewModel()
        {
            Title = $"Home | {PackageExplorer.Constants.AppName}";
            Location = "/";
        }

        public async Task OpenLocalPackage()
        {
            // TODO: move file interaction to DialogService or UIService
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.Downloads;
            picker.FileTypeFilter.Add(Constants.PackageExtension);
            picker.FileTypeFilter.Add(Constants.SymbolPackageExtension);

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var vm = await InspectPackageViewModel.CreateFromLocalPackage(file);

                NavigationService.NavigateTo(vm);
            }
        }

        public void OpenRemotePackage()
        {
            NavigationService.NavigateTo<FeedPackagePickerViewModel>();
        }

        public async Task OpenTestPackage()
        {
            var identity = new PackageIdentity("Uno.Core.Build", NuGetVersion.Parse("2.3.0"));
            var vm = await InspectPackageViewModel.CreateFromRemotePackage(identity);

            NavigationService.NavigateTo(vm);
        }
    }
}
