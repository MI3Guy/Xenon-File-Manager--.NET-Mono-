using System;
using System.Web;

namespace Xenon.PluginUtil
{
	public delegate void DirectoryChangedEventHandler(object sender, DirectoryChangedEventArgs e);
	
	public class DirectoryChangedEventArgs : EventArgs {
		public readonly Uri Path;
		public readonly IDisplayInterfaceControl Control;
		public readonly string DisplayPath;
		public readonly string ShortPath;
		public DirectoryChangedEventArgs(Uri path, IDisplayInterfaceControl control) {
			Path = path;
			Control = control;
			
			DisplayPath = CommonUtil.FileSystem.DisplayPath(path);
			ShortPath = CommonUtil.FileSystem.ShortPath(path);
		}
	}
}

