using System;
namespace PluginUtil
{
	public abstract class FileSystemHandler
	{
		public abstract bool DoLoad(PluginUIType ui, PluginOSType os);
		public abstract bool HandlesUriType(Uri uri);		
		public abstract object[] LoadDirectory(Uri uri);
		public abstract string FileNameFor(object obj);
		public abstract long FileSizeFor(object obj);
		public abstract DateTime[] FileTimesFor(object obj);
	}
}

