﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NupkgExplorer.Client.Data
{
	public class PackageVersion
	{
        public string Version { get; set; } = null!;
		public long Downloads { get; set; }
	}
}
