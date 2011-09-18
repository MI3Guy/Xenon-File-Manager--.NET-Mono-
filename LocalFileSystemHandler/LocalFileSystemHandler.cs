using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using Xenon.PluginUtil;

namespace Xenon.Plugin.LocalFileSystemHandler
{
	public class LocalFileSystemHandler : FileSystemHandler
	{
		public override bool DoLoad(PluginUIType ui, PluginOSType os) {
			//return os == PluginOSType.Unix || os == PluginOSType.Windows || os == PluginOSType.Mac;
			OS = os;
			return true;
		}
		
		PluginOSType OS;
		
		public override bool HandlesUriType(Uri uri) {
			return Exists(uri);
		}
		
		public override bool LoadsUriType(Uri uri) {
			return uri.IsFile && Directory.Exists(uri.GetScrubbedLocalPath());
		}
		
		public override Uri Combine(Uri uri, string addition) {
			return new Uri("file://" + Path.Combine(uri.GetScrubbedLocalPath(), addition));
		}
		
		public override Uri ParentDirectory(Uri uri) {
			return new Uri("file://" + Path.Combine(uri.GetScrubbedLocalPath(), ".."));
		}
		
		public override XeFileInfo[] LoadDirectory(ref Uri uri) {
			if(!LoadsUriType(uri)) throw new ArgumentException();
			//System.Threading.Thread.Sleep(1000);
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
			return uri.IsFile && (File.Exists(uri.GetScrubbedLocalPath()) || Directory.Exists(uri.GetScrubbedLocalPath()));
		}
		
		public override void LoadFile(Uri path) {
			switch(OS) {
				case PluginOSType.Unix:
					Process.Start("xdg-open", string.Format("'{0}'", path.GetScrubbedLocalPath()));
					break;
				case PluginOSType.Windows:
					Process.Start(path.GetScrubbedLocalPath());
					break;
			}
		}
		
		public override void CreateDirectory(Uri path) {
			Directory.CreateDirectory(path.GetScrubbedLocalPath());
		}
		
		public override void Copy(Uri src, Uri dest) {
			string src2 = src.GetScrubbedLocalPath();
			string dest2 = dest.GetScrubbedLocalPath();
			if(File.Exists(src2)) {
				if(Directory.Exists(dest2)) {
					dest2 = Path.Combine(dest2, Path.GetFileName(src2));
				}
				File.Copy(src2, dest2);
			}
			else if(Directory.Exists(src2)) {
				CopyDirectory(new DirectoryInfo(src2), new DirectoryInfo(dest2));
			}
		}
		
		public override void Move(Uri src, Uri dest) {
			string src2 = src.GetScrubbedLocalPath();
			string dest2 = dest.GetScrubbedLocalPath();
			if(File.Exists(src2)) {
				if(Directory.Exists(dest2)) {
					dest2 = Path.Combine(dest2, Path.GetFileName(src2));
				}
				File.Move(src2, dest2);
			}
			else if(Directory.Exists(src2)) {
				try {
					Directory.Move(src2, dest2);
				}
				catch(IOException) {
					CopyDirectory(new DirectoryInfo(src2), new DirectoryInfo(dest2));
					Directory.Delete(src2, true);
				}
			}
		}
		
		private void CopyDirectory(DirectoryInfo diSourceDir, DirectoryInfo diDestDir) {
			if(!diDestDir.Exists) diDestDir.Create();
			FileInfo[] fiSrcFiles = diSourceDir.GetFiles();
			foreach(FileInfo fiSrcFile in fiSrcFiles) {
				fiSrcFile.CopyTo(Path.Combine(diDestDir.FullName, fiSrcFile.Name));
			}
			DirectoryInfo[] diSrcDirectories = diSourceDir.GetDirectories();
			foreach(DirectoryInfo diSrcDirectory in diSrcDirectories) {
			    CopyDirectory(diSrcDirectory, new DirectoryInfo(Path.Combine(diDestDir.FullName, diSrcDirectory.Name)));
			}
		}
		
		public override void Delete(Uri uri) {
			string path = uri.GetScrubbedLocalPath();
			if(File.Exists(path)) {
				File.Delete(path);
			}
			else if(Directory.Exists(path)) {
				Directory.Delete(path, true);
			}
			else {
				throw new FileNotFoundException();
			}
		}
		
		public override void Recycle(Uri uri) {
			switch(OS) {
				case PluginOSType.Unix:
					Send2Trash.Send2Trash.Put(uri.GetScrubbedLocalPath());
					break;
				
				default:
					throw new NotImplementedException();
			}
		}
		
		FileSystemWatcher[] fsWatchers;
		IDisplayInterfaceControl[] fsControls;
		
		public override void UpdateWatchList(IEnumerable<IDisplayInterfaceControl> controls2) {
			IDisplayInterfaceControl[] controls = controls2.ToArray();
			if(this.fsWatchers != null) {
				bool respawn = false;
				if(fsControls.Length == controls.Length) {
					for(int i = 0; i < controls.Length; ++i) {
						if(fsControls[i] != controls[i]) {
							respawn = true;
							break;
						}
					}
				}
				
				if(!respawn) return;
				
				foreach(FileSystemWatcher watcher in this.fsWatchers) {
					watcher.EnableRaisingEvents = false;
					watcher.Dispose();
				}
			}
				
			FileSystemWatcher[] fsWatchers = new FileSystemWatcher[controls.Length];
			for(int i = 0; i < controls.Length; ++i) {
				if(!LoadsUriType(controls[i].CurrentLocation)) { throw new ArgumentException(); }
				FileSystemWatcher fsw = new FileSystemWatcher(controls[i].CurrentLocation.GetScrubbedLocalPath());
				fsw.Changed += OnChanged;
				fsw.Created += OnChanged;
				fsw.Deleted += OnChanged;
				fsw.Renamed += OnRenamed;
				fsw.EnableRaisingEvents = true;
				fsWatchers[i] = fsw;
			}
			
			this.fsWatchers = fsWatchers;
			this.fsControls = controls;
		}
		
		private void OnChanged(object sender, FileSystemEventArgs e) {
			for(int i = 0; i < fsWatchers.Length; ++i) {
				if(fsWatchers[i] == sender) {
					CallUpdated(this, new FileSystemUpdateEventArgs(fsControls[i]));
				}
			}
		}
		
		private void OnRenamed(object sender, RenamedEventArgs e) {
			OnChanged(sender, null);
		}
	}
}
