using System;
using PluginUtil;

namespace AboutFileSystemHandler {
	public class AboutFileSystemHandler : FileSystemHandler {
		private PluginOSType OS;
		
		public override bool DoLoad(PluginUIType ui, PluginOSType os) {
			//return os == PluginOSType.Unix || os == PluginOSType.Windows || os == PluginOSType.Mac;
			OS = os;
			return true;
		}
		
		
		public override bool HandlesUriType(Uri uri) {
			return uri.Scheme == "about";
		}
		
		public override Uri Combine(Uri uri, string addition) {
			return null;
		}
		
		public override Uri ParentDirectory(Uri uri) {
			return null;
		}
		
		public override XeFileInfo[] LoadDirectory(ref Uri uri) {
			if(!HandlesUriType(uri)) throw new ArgumentException();
			
			if(string.Compare(uri.AbsolutePath, "computer", true) == 0) {
				if(OS == PluginOSType.Windows) {
					// TODO: Computer listing
					return null;
				}
				else {
					uri = new Uri("file:///");
					return null;
				}
			}
			
			return null;
		}
	}
}

