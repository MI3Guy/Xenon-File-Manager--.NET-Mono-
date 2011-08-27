using System;
using System.Diagnostics;
using System.IO;
using Xenon.PluginUtil;
using Gtk;


namespace Xenon.Plugin.FileTypeIconHandlerUnix {
	public class FileTypeIconHandlerUnix : FileTypeIconHandler {
		public override bool DoLoad(PluginUIType ui, PluginOSType os) {
			return ui == PluginUIType.Gtk && os == PluginOSType.Unix;
		}
		
		CacheHash<string, string> fileMimeHash = new CacheHash<string, string>(CommonUtil.IconCacheMaxSize, CommonUtil.IconCacheTrimSize);
		
		public override object FindIcon(Uri path, string ext, string mimetype) {
			if(mimetype == null) {
				if(!path.IsFile) throw new ArgumentException();
				
				if(!fileMimeHash.ContainsKey(path.GetScrubbedLocalPath())) {
					ProcessStartInfo psi = new ProcessStartInfo("xdg-mime", string.Format("query filetype '{0}'", path.GetScrubbedLocalPath()));
					psi.RedirectStandardOutput = true;
					psi.UseShellExecute = false;
					Process p = Process.Start(psi);
					p.WaitForExit();
					StreamReader sr = p.StandardOutput;
					
					mimetype = sr.ReadLine();
					
					fileMimeHash[path.GetScrubbedLocalPath()] = mimetype;
				}
				else {
					mimetype = fileMimeHash[path.GetScrubbedLocalPath()];
				}
			}
			
			IconSet iconset = new IconSet();
			IconSource source = new IconSource();
			source.IconName = mimetype.Replace('/', '-'); //"inode-directory";
			//Console.WriteLine("{0}: {1}", psi.Arguments, text);
			iconset.AddSource(source);
			
			return iconset;
		}
		
		public override object FindIconDir(Uri path, string ext, string mimetype) {
			return FindIcon(null, null, "inode-directory");
		}
	}
}

