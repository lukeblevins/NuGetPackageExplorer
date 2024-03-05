﻿using PackageExplorer.Client.Data;

namespace PackageExplorer.Client
{
    public interface INugetEndpoint
	{
		Task<Json<SearchResponse>> Search(string search = null, int skip = 0, int take = 25, bool prerelease = false);

		Task<Json<PackageVersionsResponse>> ListVersions(string packageId);

		Task<Stream> DownloadPackage(string packageId, string version);

		Task<Stream> DownloadPackage(CancellationToken ct, string packageId, string version, IProgress<(long ReceivedBytes, long? TotalBytes)> progress);
	}
}
