﻿using System.Reactive.Linq;

using NuGet.Packaging.Core;
using NuGet.Versioning;

using PackageExplorer.Client;
using PackageExplorer.Client.Data;

namespace PackageExplorer.Presentation.Content
{
    public class FeedPackagePickerViewModel : ViewModelBase
	{
		
		public NavigationService NavigationService { get; set; }

		public string SearchTerm
		{
			get => GetProperty<string>();
			set => SetProperty(value);
		}
		public bool IncludePrerelease
		{
			get => GetProperty<bool>();
			set => SetProperty(value);
		}
		public PaginatedCollection<PackageData> NugetPackages
		{
			get => GetProperty<PaginatedCollection<PackageData>>();
			set => SetProperty(value);
		}
		public PackageData SelectedPackage
		{
			get => GetProperty<PackageData>();
			set => SetProperty(value);
		}
		public string[] SelectedPackageVersions
		{
			get => GetProperty<string[]>();
			set => SetProperty(value);
		}
		public string SelectedPackageVersion
		{
			get => GetProperty<string>();
			set => SetProperty(value);
		}

		public ICommand RefreshCommand => GetCommand(Refresh);
		public ICommand LoadPackageVersionsCommand => GetCommand(LoadPackageVersions);
		public ICommand OpenPackageFromFeedCommand => GetCommand(OpenPackageFromFeed);

		public FeedPackagePickerViewModel() : this(null) { }
		public FeedPackagePickerViewModel(string searchTerm = null)
		{
			Title = $"Packages | {Constants.AppName}";
			Location = !string.IsNullOrWhiteSpace(searchTerm)
				? $"/packages?q={Uri.EscapeDataString(searchTerm)}"
				: $"/packages";

			using (SuppressPropertyChangedNotifications())
			{
				SearchTerm = searchTerm;
			}

			// auto refresh when SearchTerm changed
			this.WhenAnyValue(x => x.SearchTerm, x => x.IncludePrerelease)
				.DistinctUntilChanged()
				.Throttle(TimeSpan.FromMilliseconds(300))
				.SubscribeToCommand(RefreshCommand);

			this.WhenAnyValue(x => x.SelectedPackage)
				.Subscribe(package =>
				{
					SelectedPackageVersion = null;
					SelectedPackageVersions = null;
				});

			Task.Run(Refresh);
		}

		public async Task Refresh()
		{
			var nuget = Container.GetExportedValue<INugetEndpoint>();

			SelectedPackage = null;
			NugetPackages = new PaginatedCollection<PackageData>(
				async (start, size) =>
				{
					var response = await nuget.Search(SearchTerm, skip: start, take: size, prerelease: IncludePrerelease);

					return response.Content.Data;
				},
				pageSize: 25
			);
		}

		public async Task LoadPackageVersions()
		{
			if (SelectedPackage == null) throw new ArgumentNullException(nameof(SelectedPackage));

			async Task<IEnumerable<string>> GetVersions()
			{
				if (IncludePrerelease)
				{
					var nuget = Container.GetExportedValue<INugetEndpoint>();
					var response = await nuget.ListVersions(SelectedPackage.Id);

					return response.Content.Versions;
				}
				else
				{
					// PackageData.Versions contains only stable versions
					return SelectedPackage.Versions.Select(x => x.Version);
				}
			}

			SelectedPackageVersions = (await GetVersions())
				.Reverse() // sort latest on top
				.ToArray();
			SelectedPackageVersion =
				SelectedPackageVersions.FirstOrDefault(x => string.Equals(x, SelectedPackage.Version, StringComparison.InvariantCultureIgnoreCase)) ??
				SelectedPackageVersions.FirstOrDefault();
		}

		public async Task OpenPackageFromFeed(object? parameter)
		{
			// parameter is not null, when invoked from double-clicking on the listview
			var package = parameter as PackageData ?? SelectedPackage ?? throw new ArgumentNullException(nameof(SelectedPackage));
			var version = parameter is PackageData
				? package.Version // ignore any selected version, when double-clicking
				: (SelectedPackageVersion ?? package.Version);
			var identity = new PackageIdentity(package.Id, NuGetVersion.Parse(version));
			var inspectVM = await InspectPackageViewModel.CreateFromRemotePackage(identity);

			NavigationService.NavigateTo(inspectVM);
		}
	}
}
