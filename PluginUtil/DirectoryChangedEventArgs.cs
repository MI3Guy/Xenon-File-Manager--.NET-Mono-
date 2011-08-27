using System;
using System.Web;

namespace PluginUtil
{
	public delegate void DirectoryChangedEventHandler(object sender, DirectoryChangedEventArgs e);
	
	public class DirectoryChangedEventArgs : EventArgs {
		public readonly Uri Path;
		public readonly IDisplayInterfaceControl Control;
		public readonly string FullPath;
		public readonly string DisplayPath;
		public DirectoryChangedEventArgs(Uri path, IDisplayInterfaceControl control) {
			Path = path;
			Control = control;
			
			if(path.IsFile) {
				FullPath = path.GetScrubbedLocalPath();
				string txt = path.GetScrubbedLocalPath();
				if(System.IO.Path.GetFileName(txt) == string.Empty) {
					string tmptxt = System.IO.Path.GetDirectoryName(txt);
					
					if(tmptxt == null) {
						DisplayPath = txt;
						return;
					}
					txt = tmptxt;
				}
				DisplayPath = System.IO.Path.GetFileName(txt);
			}
			else {
				DisplayPath = path.AbsolutePath;
				FullPath = path.PathAndQuery;
			}
		}
	}
}

