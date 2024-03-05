using NuGetPackageExplorer.Types;

namespace NuGetPackageExplorer.MefServices
{
    class PackageEditorService : IPackageEditorService
    {
        void IPackageEditorService.BeginEdit()
        {
            throw new NotImplementedException();
        }

        void IPackageEditorService.CancelEdit()
        {
            throw new NotImplementedException();
        }

        bool IPackageEditorService.CommitEdit()
        {
            throw new NotImplementedException();
        }
    }
}
