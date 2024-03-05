﻿using PackageExplorer.Business.Nupkg;

namespace PackageExplorer.Views.Converters
{
    public class FSObjectStyleSelector : StyleSelector
	{
		public Style FileStyle { get; set; }

		public Style DirectoryStyle { get; set; }

		protected override Style SelectStyleCore(object item, DependencyObject container)
		{
			switch (item)
			{
				case NupkgContentFile _: return FileStyle;
				case NupkgContentDirectory _: return DirectoryStyle;

				default:
					this.Log().Warn($"Invalid item type: {item?.GetType().ToString() ?? "<null>"}");
					return null;
			};
		}
	}
}
