namespace PackageExplorer.Business.Nupkg
{
    public class NupkgContentDirectory : INupkgFileSystemObject
	{
		public string Name { get; }

		public string FullName { get; }

		public List<INupkgFileSystemObject> Items { get; }

		public NupkgContentDirectory(string fullname, IEnumerable<INupkgFileSystemObject> items = null)
		{
			this.Name = Path.GetFileName(fullname);
			this.FullName = fullname;
			this.Items = items?.ToList() ?? new List<INupkgFileSystemObject>();
		}
	}
}
