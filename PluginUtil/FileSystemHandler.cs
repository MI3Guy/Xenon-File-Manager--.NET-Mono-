using System;
using System.IO;
using System.Collections.Generic;

namespace Xenon.PluginUtil {
	public abstract class FileSystemHandler : PluginHandler {
		public abstract bool HandlesUriType(Uri uri);
		public virtual bool LoadsUriType(Uri uri) { return HandlesUriType(uri); }
		public abstract Uri Combine(Uri uri, string addition);
		public abstract Uri ParentDirectory(Uri uri);
		public abstract string[] FileName(Uri uri);
		public abstract XeFileInfo[] LoadDirectory(ref Uri uri);
		public abstract bool Exists(Uri uri);
		
		public virtual string DisplayPath(Uri uri) {
			return uri.PathAndQuery;
		}
		public virtual string ShortPath(Uri uri) {
			return uri.AbsolutePath;
			
		}
		
		
		
		
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
		
		public virtual void CopyAsync(Uri[] src, Uri[] dest, IFileOperationProgress progress) {
			if(src.Length != dest.Length) throw new ArgumentException();
			for(int i = 0; i < src.Length; ++i) {
				progress.UpdateProgress(i + 1, src.Length, (double)i/(double)src.Length, null, null);
				Copy(src[i], dest[i]);
			}
			progress.Finish();
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

