using System;
namespace PluginUtil
{
	public delegate void DirectoryChangedEventHandler(object sender, DirectoryChangedEventArgs e);
	
	public class DirectoryChangedEventArgs : EventArgs {
		public readonly Uri Path;
		public DirectoryChangedEventArgs(Uri path) {
			Path = path;
		}
	}
}

