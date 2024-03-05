﻿namespace PackageExplorer.Views.Converters
{
    public class FSObjectTemplateSelector : DataTemplateSelector
	{
		public DataTemplate FileTemplate { get; set; }
		public DataTemplate DirectoryTemplate { get; set; }

		protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) => SelectTemplateCore(item);

		protected override DataTemplate SelectTemplateCore(object item)
		{
			if (item is PackageFile) return FileTemplate;
			if (item is PackageFolder) return DirectoryTemplate;

			this.Log().Warn($"Invalid item type: {item?.GetType().ToString() ?? "<null>"}");
			return null;
		}
	}
}
