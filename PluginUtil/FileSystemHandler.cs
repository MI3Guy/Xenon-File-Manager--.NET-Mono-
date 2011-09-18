using System;
using System.IO;
using System.Collections.Generic;

namespace Xenon.PluginUtil {
	public abstract class FileSystemHandler : PluginHandler {
		public abstract bool HandlesUriType(Uri uri);
		public virtual bool LoadsUriType(Uri uri) { return HandlesUriType(uri); }
		public abstract Uri Combine(Uri uri, string addition);
		public abstract Uri ParentDirectory(Uri uri);
		public abstract XeFileInfo[] LoadDirectory(ref Uri uri);
		public abstract bool Exists(Uri uri);
		
		public virtual void LoadFile(Uri path) {
			throw new NotImplementedException();
		}
		
		public virtual void CreateDirectory(Uri path) {
			throw new NotImplementedException();
		}
		
		public virtual Stream OpenRead(Uri path) {
			throw new NotImplementedException();
		}
		
		public virtual Stream OpenWrite(Uri path) {
			throw new NotImplementedException();
		}
		
		public virtual void Copy(Uri src, Uri dest) {
			throw new NotImplementedException();
		}
		
		public virtual void Move(Uri src, Uri dest) {
			throw new NotImplementedException();
		}
		
		public virtual void Delete(Uri uri) {
			throw new NotImplementedException();
		}
		
		public virtual void Recycle(Uri uri) {
			throw new NotImplementedException();
		}
		
		
		public delegate void FileSystemUpdateEventHandler(object sender, FileSystemUpdateEventArgs e);
		public event FileSystemUpdateEventHandler Updated;
		
		protected void CallUpdated(object sender, FileSystemUpdateEventArgs e) {
			if(Updated != null) {
				Updated(sender, e);
			}
		}
		
		public virtual void UpdateWatchList(IEnumerable<IDisplayInterfaceControl> controls) {
			throw new NotImplementedException();
		}
		
		public class FileSystemUpdateEventArgs : EventArgs {
			public FileSystemUpdateEventArgs(IDisplayInterfaceControl control) {
				Control = control;
			}
			public IDisplayInterfaceControl Control { get; private set; }
		}
	}
}

