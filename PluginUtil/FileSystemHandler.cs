using System;

namespace PluginUtil {
	public abstract class FileSystemHandler : PluginHandler {
		public abstract bool HandlesUriType(Uri uri);
		public abstract Uri Combine(Uri uri, string addition);
		public abstract Uri ParentDirectory(Uri uri);
		public abstract XeFileInfo[] LoadDirectory(ref Uri uri);
		//public abstract string FileNameFor(object obj);
		//public abstract long FileSizeFor(object obj);
		//public abstract DateTime[] FileTimesFor(object obj);
	}
}

