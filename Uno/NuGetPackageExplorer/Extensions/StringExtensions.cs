namespace NuGetPackageExplorer.Extensions
{
    public static class StringExtensions
    {
        public static string? NullIfEmpty(this string value) => string.IsNullOrEmpty(value) ? null : value;
    }
}
