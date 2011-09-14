using System;
using System.IO;

namespace Xenon.PluginUtil {
	public abstract class FileSystemHandler : PluginHandler {
		public abstract bool HandlesUriType(Uri uri);
		public abstract Uri Combine(Uri uri, string addition);
		public abstract Uri ParentDirectory(Uri uri);
		public abstract XeFileInfo[] LoadDirectory(ref Uri uri);
		public abstract bool Exists(Uri uri);
		
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
		
	}
}

