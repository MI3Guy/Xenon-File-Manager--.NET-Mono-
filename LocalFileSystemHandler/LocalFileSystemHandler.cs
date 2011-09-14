using System;
using System.Collections;
using System.IO;
using System.Web;
using Xenon.PluginUtil;

namespace Xenon.Plugin.LocalFileSystemHandler
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
			int extra = ((bool)SettingsUtil.MainSettings["show..item"].data && di.Parent != null) ? 1 : 0;
			XeFileInfo[] fi2 = new XeFileInfo[di2.Length + fi.Length + extra];
			if(extra != 0) {
				fi2[0] = new XeFileInfo(di.Parent);
				fi2[0].Name = "..";
			}
			int i = extra;
			for(int j = 0; j < di2.Length; ++j, ++i) {
				fi2[i] = new XeFileInfo(di2[j]);
			}
			for(int j = 0; j < fi.Length; ++j, ++i) {
				fi2[i] = new XeFileInfo(fi[j]);
			}
			return fi2;
		}
		
		public override bool Exists(Uri uri) {
			return uri.IsFile && File.Exists(uri.GetScrubbedLocalPath());
		}
	}
}
