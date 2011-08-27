using System;
using System.Collections;
using System.IO;
using System.Web;
using PluginUtil;

namespace LocalFileSystemHandler
{
	public class LocalFileSystemHandler : FileSystemHandler
	{
		public override bool DoLoad(PluginUIType ui, PluginOSType os) {
			//return os == PluginOSType.Unix || os == PluginOSType.Windows || os == PluginOSType.Mac;
			return true;
		}
		
		
		public override bool HandlesUriType(Uri uri) {
			return uri.IsFile && Directory.Exists(uri.GetScrubbedLocalPath());
		}
		
		public override Uri Combine(Uri uri, string addition) {
			return new Uri("file://" + Path.Combine(uri.GetScrubbedLocalPath(), addition));
		}
		
		public override Uri ParentDirectory(Uri uri) {
			return new Uri("file://" + Path.Combine(uri.GetScrubbedLocalPath(), ".."));
		}
		
		public override XeFileInfo[] LoadDirectory(ref Uri uri) {
			if(!HandlesUriType(uri)) throw new ArgumentException();
			
			DirectoryInfo di = new DirectoryInfo(uri.GetScrubbedLocalPath());
			Console.WriteLine("file://" + di.FullName.TrimEnd('\\', '/') + Path.DirectorySeparatorChar);
			uri = new Uri("file://" + di.FullName.TrimEnd('\\', '/') + Path.DirectorySeparatorChar);
			Console.WriteLine(uri.ToString());
			DirectoryInfo[] di2 = di.GetDirectories();
			FileInfo[] fi = di.GetFiles();
			XeFileInfo[] fi2 = new XeFileInfo[di2.Length + fi.Length];
			int i;
			for(i = 0; i < di2.Length; ++i) {
				fi2[i] = new XeFileInfo(di2[i]);
			}
			for(int j = 0; j < fi.Length; ++j, ++i) {
				fi2[i] = new XeFileInfo(fi[j]);
			}
			return fi2;
		}
	}
}
