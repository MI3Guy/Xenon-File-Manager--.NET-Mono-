using System;
using System.Collections;
using System.IO;
using PluginUtil;

namespace LocalFileSystemHandler
{
	public class LocalFileSystemHandler : FileSystemHandler
	{
		public override bool DoLoad(PluginUIType ui, PluginOSType os) {
			return os == PluginOSType.Linux || os == PluginOSType.Windows;
		}
		
		
		public override bool HandlesUriType(Uri uri) {
			return uri.IsFile && Directory.Exists(uri.AbsolutePath);
		}
		
		public override object[] LoadDirectory(Uri uri) {
			if(!HandlesUriType(uri)) throw new ArgumentException();
			
			DirectoryInfo di = new DirectoryInfo(uri.AbsolutePath);
			return di.GetFiles();
		}
		
		public override string FileNameFor(object obj) {
			return ((FileInfo)obj).FullName;
		}
		
		public override long FileSizeFor(object obj) {
			return ((FileInfo)obj).Length;
		}
		
		public override  DateTime[] FileTimesFor(object obj) {
			FileInfo fi = ((FileInfo)obj);
			return new DateTime[] { fi.LastWriteTime, fi.LastAccessTime, fi.CreationTime };
		}
	}
}
